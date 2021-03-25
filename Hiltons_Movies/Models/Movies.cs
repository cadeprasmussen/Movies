using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiltons_Movies.Models
{
    public class Movies
    {
        [Required, Key]
        public int MovieId { get; set; }

        //Making sure Category, Title, Year, Director, Rating are entered into the form before validating the form and submitting
        [Required]
        public string Category { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Rating { get; set; }
        public bool Edited { get; set; }
        public string LentTo { get; set; }

        //Setting the max number of characters for the note to 25 characters
        [MaxLength(25)]
        public string Notes { get; set; }

    }
}
