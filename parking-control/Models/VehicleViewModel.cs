using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace parking_control.Models
{
    public class VehicleAddViewModel
    {
        [Required]
        [Display(Name = "Board")]
        public string Board { get; set; }

        [Required]
        [Display(Name = "InitialDate")]
        public DateTime InitialDate { get; set; }

        public bool DateValid(DateTime time)
        {
            return !(time.Year == 1 && time.Month == 1 && time.Day == 1 && time.Hour == 0 && time.Minute == 0 && time.Second == 0);
        }
    }

    public class VehicleUpdateViewModel
    {
        [Required]
        [Display(Name = "FinalDate")]
        public DateTime FinalDate { get; set; }

        // disabled
        [Required]        
        [Display(Name = "Board")]
        public DateTime Board { get; set; } 

        // disabled
        [Required]
        [Display(Name = "InitialDate")]
        public DateTime InitialDate { get; set; }

        // disabled
        [Required]
        [Display(Name = "HourPrice")]
        public string HourPrice { get; set; }

        // disabled
        [Required]
        [Display(Name = "PriceCharged")]
        public string PriceCharged { get; set; }

        public bool DateValid(DateTime time)
        {
            return !(time.Year == 1 && time.Month == 1 && time.Day == 1 && time.Hour == 0 && time.Minute == 0 && time.Second == 0);
        }
    }

}