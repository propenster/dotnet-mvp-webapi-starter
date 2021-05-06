using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.ResponseWrappers
{
    public class PaginatedResponse<T> : Response<T>
    {
        public int Page { get; set; }
        public int NumberOfPages { get; set; }
        public int PageSize { get; set; }

        public PaginatedResponse(T data, int Page, int NumberOfPages, int PageSize)
        {
            this.Succeeded = true;
            this.Message = string.Empty;
            this.Errors = null;
            this.Data = data;
            this.Page = Page;
            this.NumberOfPages = NumberOfPages;
            this.PageSize = PageSize;
        }
    }
}
