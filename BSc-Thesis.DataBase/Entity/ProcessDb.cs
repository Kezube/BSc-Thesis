using System.ComponentModel.DataAnnotations.Schema;

namespace BSc_Thesis.DataBase.Models;
public partial class ProcessDb
{

    
    public int Id { get; set; }

    public DateTime Data { get; set; }

    [Column("Temperatura")]
    public double Temperatura { get; set; }

    public double TemperaturaOtoczenia { get; set; }

    public double Glukoza { get; set; }

    public double Maltoza { get; set; }

    public double Maltortioza { get; set; }

    public double Cukier { get; set; }

    public double DrozdzeMartwe { get; set; }

    public double DrozdzeAktuwne { get; set; }

    public double DrozdzeLatacujne { get; set; }

    public double Etanol { get; set; }
}
