using DotNetWebAPIMVPStarter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Blog
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; } //slug of the name
        public List<Post> Posts { get; set; }

        public Category()
        {
            Slug = Utility.Slugify(Name);
        }
    }
}
