using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibary
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IDzamówienia")]
        public int IDzamówienia { get; set; }
        [Column("IDklienta")]
        public int IDklienta { get; set; }
        [Column("IDpracownika")]
        public int IDpracownika { get; set; }
        [Column("DataZamówienia")]
        public DateTime DataZamówienia { get; set; }
        [Column("DataWymagana")]
        public DateTime DataWymagania { get; set; }
        [Column("DataWysyłki")]
        public DateTime DataWysylki { get; set; }
        [Column("IDspedytora")]
        public int IDspedytora { get; set;}
      
        [Column("Fracht",TypeName = "decimal(5, 2)")]
        public decimal Fracht { get; set; }

        [Column("NazwaOdbiorcy")]
        public string NazwaOdbiorcy { get; set; }
        [Column("AdresOdbiorcy")]
        public string AdresOdbiorcy { get; set; }
        [Column("MiastoOdbiorcy")]
        public string MiastoOdbiorcy { get; set; }
        [Column("RegionOdbiorcy")]
        public string RegionOdbiorcy { get; set; }
        [Column("KodPocztowyOdbiorcy")]
        public string KodPocztowy { get; set; }
        [Column("KrajOdbiorcy")]
        public string KrajOdbiorcy { get; set; }

    }
}
