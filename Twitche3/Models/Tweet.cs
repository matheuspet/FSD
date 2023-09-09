using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitche3.Models
{
    public class Tweet
    {
        [Required]
        public String Id { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public int Retweets { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public string DeletedOn { get; set; }

        [Required]
        public string Deleted { get; set; }


    }
}