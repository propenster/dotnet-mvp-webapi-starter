using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Product
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }



    }
}
