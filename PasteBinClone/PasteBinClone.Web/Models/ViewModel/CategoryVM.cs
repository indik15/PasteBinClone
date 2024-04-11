﻿using System.ComponentModel.DataAnnotations;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class CategoryVM
    {
        public int id { get; set; }

        [Required (ErrorMessage = "The Category field name is required.")]
        public string CategoryName { get; set; }
    }
}
