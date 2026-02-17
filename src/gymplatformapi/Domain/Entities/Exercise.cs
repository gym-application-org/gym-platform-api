using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Exercise : Entity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? MuscleGroup { get; set; }
    public string? Equipment { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }

    public string? VideoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }

    public bool IsActive { get; set; }

    public Exercise() { }

    public Exercise(
        string name,
        string? description,
        string? muscleGroup,
        string? equipment,
        DifficultyLevel difficultyLevel,
        string? videoUrl,
        string? thumbnailUrl,
        bool isActive = true
    )
        : base()
    {
        Name = name;
        Description = description;
        MuscleGroup = muscleGroup;
        Equipment = equipment;
        DifficultyLevel = difficultyLevel;
        VideoUrl = videoUrl;
        ThumbnailUrl = thumbnailUrl;
        IsActive = isActive;
    }
}
