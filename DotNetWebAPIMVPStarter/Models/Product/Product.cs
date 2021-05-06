using DotNetWebAPIMVPStarter.Utils;
using Newtonsoft.Json;
using Slugify;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.Product
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("product_id")]
        [Column("ProductId", TypeName = "nvarchar(50)")]
        public string ProductId { get; set; } //a unique string id that will be denerated for the product....
        [JsonProperty("product_name")]
        [Column("ProductName", TypeName = "nvarchar(150)")]
        public string ProductName { get; set; }
        [JsonProperty("slug")]
        [Column("Slug", TypeName = "nvarchar(100)")]
        public string Slug { get; set; } //Generate a slug out of the name...
        [JsonProperty("product_price")]
        [Column("ProductPrice", TypeName = "decimal(7,5)")]
        public decimal ProductPrice { get; set; }
        [JsonProperty("product_real_price")]
        [Column("ProductRealPrice", TypeName = "decimal(7,5)")]
        public decimal ProductRealPrice { get; set; }
        [JsonProperty("date_created")]
        [Column("DateCreated", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("date_last_updated")]
        [Column("DateLastUpdated", TypeName = "datetime")]
        public DateTime? DateLastUpdated { get; set; }
        public List<Review> Reviews { get; set; }


        public Product()
        {
            //SlugHelper Helper = new SlugHelper();
            Slug = Utility.Slugify(ProductName);
        }


    }
}
