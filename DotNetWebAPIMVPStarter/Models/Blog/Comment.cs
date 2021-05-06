using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Blog
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; } //foreignKey...
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        
    }
}
