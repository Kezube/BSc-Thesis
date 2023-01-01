using System.ComponentModel.DataAnnotations.Schema;

namespace BSc_Thesis.DataBase.Models;

public class ProcessDb
{
    [Column("Id")] public int ID { get; set; }

    [Column("Data")] public DateTime Date { get; set; }

    [Column("Temperatura")] public double Temperature { get; set; }

    [Column("TemperaturaOtoczenia")] public double AmbientTemperature { get; set; }

    [Column("Glukoza")] public double Glucose { get; set; }

    [Column("Maltoza")] public double Maltose { get; set; }

    [Column("Maltortioza")] public double Maltotriosis { get; set; }

    [Column("Cukier")] public double Sugar { get; set; }

    [Column("DrozdzeMartwe")] public double DeadYeast { get; set; }

    [Column("DrozdzeAktywne")] public double ActiveYeast { get; set; }

    [Column("DrozdzeLatencyjne")] public double LatticeYeast { get; set; }

    [Column("Etanol")] public double Ethanol { get; set; }
}