using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.Models
{
    public class VideoGameConsole
    {
        [Key]
        public int ID_VideoGameConsole { get; set; }

        [Required]
        [Display(Name = "Description Console")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximum {1}")]
        public string Description { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }
    }
}
