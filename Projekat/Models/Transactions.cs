using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        [Column(TypeName = "text")]
        public string Cart { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
    }
}