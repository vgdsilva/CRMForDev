using CRMForDev.Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMForDev.Domain.ValueObjects;

public abstract class EntityControl
{
    [Key]
    [Column("id")]
    public string Id { get; set; } = IdGenerator.NewObjectId();

    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    [Column("updatedat")]
    public DateTime UpdatedAt { get; set; }
}
