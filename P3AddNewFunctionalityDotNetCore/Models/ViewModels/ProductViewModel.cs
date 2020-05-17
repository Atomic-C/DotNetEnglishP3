using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }
        //[Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid  number")]
        public string Stock { get; set; }
        //[Required]
        //[Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        public string Price { get; set; }


    }
}
