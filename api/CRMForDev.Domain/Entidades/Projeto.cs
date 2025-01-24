using CRMForDev.Domain.Utils;
using CRMForDev.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMForDev.Domain.Entidades;

[Table("projeto")]
public class Projeto : EntityControl
{

    [Column("descricao")]
    [StringLength(255)]
    public string Descricao { get; set; } = string.Empty;

    [Column("usuarioid")]
    public string UsuarioId { get; set; } = IdGenerator.NewObjectId();

    [ForeignKey(nameof(UsuarioId))]
    public virtual Usuario UsuarioEntidade { get; set; }

    public Projeto()
    {
        
    }
}
