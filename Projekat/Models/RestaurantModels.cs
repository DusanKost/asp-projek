using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class RestaurantModels
    {
        #region Basic Props
        public int Id { get; set; }
        [Required]
        [Display(Name="Restaurant Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Restaurant Description")]
        public string Desc { get; set; }

        public string ApplicationUserId { get; set; }

        #endregion

    }
}