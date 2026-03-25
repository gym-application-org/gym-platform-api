using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

namespace Core.Mailing.MailKitImplementations;

public class MailKitMailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private DkimSigner? _signer;

    public MailKitMailService(IConfiguration configuration)
    {
        const string configurationSection = "MailSettings";
        _mailSettings =
            configuration.GetSection(configurationSection).Get<MailSettings>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
        _signer = null;
    }

    public void SendMail(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;

        EmailPrepare(mail, out MimeMessage email, out SmtpClient smtp);
        smtp.Send(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }

    public async Task SendEmailAsync(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;

        EmailPrepare(mail, out MimeMessage email, out SmtpClient smtp);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }

    private void EmailPrepare(Mail mail, out MimeMessage email, out SmtpClient smtp)
    {
        email = new MimeMessage();
        email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));
        email.To.AddRange(mail.ToList);

        if (mail.CcList != null && mail.CcList.Any())
            email.Cc.AddRange(mail.CcList);

        if (mail.BccList != null && mail.BccList.Any())
            email.Bcc.AddRange(mail.BccList);

        email.Subject = mail.Subject;

        if (mail.UnsubscribeLink != null)
            email.Headers.Add("List-Unsubscribe", $"<{mail.UnsubscribeLink}>");

        BodyBuilder bodyBuilder = new() { TextBody = mail.TextBody, HtmlBody = mail.HtmlBody };

        if (mail.Attachments != null)
        {
            foreach (MimeEntity? attachment in mail.Attachments)
            {
                if (attachment != null)
                    bodyBuilder.Attachments.Add(attachment);
            }
        }

        email.Body = bodyBuilder.ToMessageBody();
        email.Prepare(EncodingConstraint.SevenBit);

        if (
            !string.IsNullOrWhiteSpace(_mailSettings.DkimPrivateKey)
            && !string.IsNullOrWhiteSpace(_mailSettings.DkimSelector)
            && !string.IsNullOrWhiteSpace(_mailSettings.DomainName)
        )
        {
            _signer = new DkimSigner(ReadPrivateKeyFromPemEncodedString(), _mailSettings.DomainName, _mailSettings.DkimSelector)
            {
                HeaderCanonicalizationAlgorithm = DkimCanonicalizationAlgorithm.Simple,
                BodyCanonicalizationAlgorithm = DkimCanonicalizationAlgorithm.Simple,
                AgentOrUserIdentifier = $"@{_mailSettings.DomainName}",
                QueryMethod = "dns/txt"
            };

            HeaderId[] headers = { HeaderId.From, HeaderId.Subject, HeaderId.To };
            _signer.Sign(email, headers);
        }

        smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

        if (_mailSettings.AuthenticationRequired)
            smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
    }

    private AsymmetricKeyParameter ReadPrivateKeyFromPemEncodedString()
    {
        if (string.IsNullOrWhiteSpace(_mailSettings.DkimPrivateKey))
            throw new InvalidOperationException("DKIM private key is empty.");

        string pemEncodedKey = _mailSettings.DkimPrivateKey.Replace("\\n", "\n").Trim();

        using StringReader stringReader = new(pemEncodedKey);
        PemReader pemReader = new(stringReader);
        object? pemObject = pemReader.ReadObject();

        if (pemObject is AsymmetricCipherKeyPair keyPair)
            return keyPair.Private;

        if (pemObject is AsymmetricKeyParameter keyParameter && keyParameter.IsPrivate)
            return keyParameter;

        throw new InvalidOperationException("Invalid DKIM private key format.");
    }
}
