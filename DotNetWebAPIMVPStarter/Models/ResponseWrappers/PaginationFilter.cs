using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.ResponseWrappers
{
    public class PaginationFilter
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        public PaginationFilter()
        {
            this.Page = 1;
            this.Limit = 10;
        }

        public PaginationFilter(int Page, int Limit)
        {
            this.Page = Page < 1 ? 1 : Page;
            this.Limit = Limit > 10 ? 10 : Limit;
        }
    }
}
