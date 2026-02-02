using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Models
{
   public class BankAccount
   {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("OwnerPhoneNumber")]
        public float OwnerPhoneNumber { get; set; }
        [Column("AccountNumber")]
        public decimal AccountNumber { get; set; }
        [Column("CorrespondentAccountNumber")]
        public decimal CorrespondentAccountNumber { get; set; }
        [Column("BIC")]
        public int BIC { get; set; }
        [Column("Balance")]
        public decimal Balance { get; set; }
        [Column("BankName")]
        public string BankName { get; set; }
        [Column("BankAdress")]
        public string BankAdress { get; set; }

    }
}
