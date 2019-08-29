using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderApplication.Models
{
    public class Orders
    {
        [Key]
        [Required]
        public string OrderId { get; set; }

        public string DrinkId { get; set; }

        public string DrinkTitle { get; set; }

        public int DrinkQuantity { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name = "Name For Order")]
        public string NameForOrder { get; set; }


        [Display(Name = "Special Request(s)")]
        public string UserRequest { get; set; } 

        [Required]
        public DateTime OrderTime { get; set; }

        public DateTime? CloseTime { get; set; }

        [Required]
        public Boolean OpenStatus { get; set; }

        public string AssignedTo { get; set; }

        [Required]
        [Display(Name = "Total Order Cost")]
        public double TotalCost { get; set; }
    }
}
