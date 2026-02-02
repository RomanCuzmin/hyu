using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Models
{
    public class User
    {
        
            //Модель пользователя
            [Key]
            [Column("ID")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Column("Name")]
            public string Name { get; set; }
            [Column("SerName")]
            public string SerName { get; set; }
            [Column("LoginName")]
            public string LoginName { get; set; }
            [Column("Password")]
            public string Password { get; set; }
            [Column("PhoneNumber")]
            public double PhoneNumber { get; set; }
            [Column("PassportSeries")]
            public int PassportSeries { get; set; }
            [Column("PassportNumber")]
            public int PassportNumber { get; set; }
            [Column("RegistrationDate")]
            public DateTime RegistrationDate { get; set; }
        
    }
}
