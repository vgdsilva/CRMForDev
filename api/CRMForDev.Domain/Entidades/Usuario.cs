using CRMForDev.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMForDev.Domain.Entidades;

[Table("usuario")]
public class Usuario : EntityControl
{
    [Column("login")]
    [StringLength(320)]
    public string Login { get; set; }

    [Column("nome")]
    [StringLength(80)]
    public string Nome { get; set; }

    [Column("senha")]
    [StringLength(30)]
    public string Senha { get; set; } = "";
}
