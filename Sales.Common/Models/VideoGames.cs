﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.Models
{
    public class VideoGames
    {
        [Key]
        public int ID_VideoGames { get; set; }

        public int ID_VideoGameConsole { get; set; }


        [Required]
        [Display(Name = "Description Game")]
        [StringLength(300, ErrorMessage = "The field {0} can contain maximum {1}")]
        public string Description { get; set; }


        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }


        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public string ImageFullPath 
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return null;
                }

                return $"https://5949-2806-2f0-9001-5230-8c71-1937-aa8e-fc14.ngrok.io/{this.ImagePath.Substring(1)}";
            }
        }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }


        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }


        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }


        [ForeignKey("ID_VideoGameConsole")]
        [Display(Name = "Publish On")]
        public virtual VideoGameConsole VideoGameConsole { get; set; }

    }
}
