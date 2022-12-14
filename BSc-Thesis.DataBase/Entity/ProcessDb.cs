using System.ComponentModel.DataAnnotations.Schema;

namespace BSc_Thesis.DataBase.Models;

public partial class ProcessDb
{

    //public long Id { get; set; }

    [Column("Temperatura")]
    public double? Temperatura { get; set; }

    public double? TemperaturaOtoczenia { get; set; }

    public double? Glukoza { get; set; }

    public double? Maltoza { get; set; }

    public double? Maltortioza { get; set; }

    public double? Cukier { get; set; }

    public double? DrozdzeMartwe { get; set; }

    public double? DrozdzeAktywne { get; set; }

    public double? DrozdzeLatecyjne { get; set; }

    public double? Etanol { get; set; }
}
