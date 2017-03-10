using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace parking_control.Models
{
    public class AddValidityDateViewModel
    {
        [Required]
        [Display(Name = "InitialDate")]
        public DateTime InitialDate { get; set; }

        [Required]
        [Display(Name = "FinalDate")]
        public DateTime FinalDate { get; set; }

        [Required]
        [Display(Name = "HourPrice")]
        public double HourPrice { get; set; }        

        public bool DateValid(DateTime time)
        {
            return !(time.Year == 1 && time.Month == 1 && time.Day == 1 && time.Hour == 0 && time.Minute == 0 && time.Second == 0);
        }
    }
}