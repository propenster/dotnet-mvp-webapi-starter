using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Blog.RequestDtos
{
    /// <summary>
    /// This calss is an ApiResource / Dto for the Post Model we'll skip some fields that are not meant to be entered by the user like slug
    /// </summary>
    public class CreatePostRequest
    {
        //[Key]
        //public int Id { get; set; }
        public string Title { get; set; }
        //public string Slug { get; set; }
        public List<string> Tags { get; set; }
        public string Body { get; set; }
        public string FeatureImage { get; set; } //base64String or link to some aws bucket...
        public Status Status { get; set; }
        //public DateTime DateCreated { get; set; }
        //public DateTime? DateUpdated { get; set; }
        //public int AuthorId { get; set; } // this is the Auth User that Created this Blog Post
        //[ForeignKey("AuthorId")]
        //public User Author { get; set; }
        //public List<Comment> Comments { get; set; }
    }
}
