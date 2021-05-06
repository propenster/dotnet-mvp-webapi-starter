using DotNetWebAPIMVPStarter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Blog
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public List<string> Tags { get; set; }
        public string Body { get; set; }
        public string FeatureImage { get; set; } //base64String or link to some aws bucket...
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int AuthorId { get; set; } // this is the Auth User that Created this Blog Post
        [ForeignKey("AuthorId")]
        public User Author { get; set; }
        public List<Comment> Comments { get; set; }

        public Post()
        {
            //SlugHelper Helper = new SlugHelper();
            Slug = Utility.Slugify(Title);
        }

    }

    public enum Status
    {
        Unpublished,
        Published,
        Archived
    }
}
