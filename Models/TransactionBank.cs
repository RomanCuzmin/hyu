using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Models
{
    public class TransactionBank
    {
            [Key]
            [Column("ID")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Column("BankAccount ")]
            public BankAccount BankAccount { get; set; }
            [Column("Type")]
            public string Type { get; set; }
            [Column("TheAmount")]
            public decimal TheAmount { get; set; }
            [Column("Date")]
            public DateTime Date { get; set; }

        
    }
}
