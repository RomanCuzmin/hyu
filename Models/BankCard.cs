using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Models
{
    public class BankCard
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("AccountNumber")]
        public decimal AccountNumber { get; set; }
        [Column("BankCardNumber")]
        public decimal BankCardNumber { get; set; }
        [Column("CVICode")]
        public int CVICode { get; set; }
        [Column("PinCode")]
        public int PinCode { get; set; }
        [Column("ValidUntil")]
        public DateTime ValidUntil { get; set; }
    }
}
