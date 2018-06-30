using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class MenuModels
    {
        #region Basic Props
        public int Id { get; set; }
        [Required]
        [Display(Name = "The name of the menu")]
        public string MenuName { get; set; }
        [Required]
        [Display(Name = "Description of a menu")]
        public string Desc { get; set; }

        #endregion

        #region ORM Props

        public int RestaurantModels_id { get; set; }
        public string ApplicationUserId { get; set; }

        #endregion
    }
}