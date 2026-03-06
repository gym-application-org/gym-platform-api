using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MailServices.EmailTemplates;

public class MailTemplateResult
{
    public string Subject { get; set; }
    public string TextBody { get; set; }
    public string HtmlBody { get; set; }

    public MailTemplateResult()
    {
        Subject = string.Empty;
        TextBody = string.Empty;
        HtmlBody = string.Empty;
    }

    public MailTemplateResult(string subject, string textBody, string htmlBody)
    {
        Subject = subject;
        TextBody = textBody;
        HtmlBody = htmlBody;
    }
}
