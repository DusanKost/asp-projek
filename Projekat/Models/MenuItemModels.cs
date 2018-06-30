using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class MenuItemModels
    {
        #region Basic Props
        public int Id { get; set; }
        [Required]
        [Display(Name = "Menu item name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Desc { get; set; }
        [Required]
        [Display(Name="Price")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        #endregion

        #region ORM Props
        public int RestaurantModels_id { get; set; }
        public string ApplicationUserId { get; set; }

        public int MenuModels_id { get; set; }
        #endregion
    }
}