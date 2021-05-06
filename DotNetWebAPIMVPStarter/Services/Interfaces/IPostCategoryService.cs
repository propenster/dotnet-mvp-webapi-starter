using DotNetWebAPIMVPStarter.Models.Blog;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.Interfaces
{
    public interface IPostCategoryService
    {
        Response<IEnumerable<Category>> GetAll();
        Response<Category> GetSingle(int Id);
        void Add(Category Category);
        void Update(int Id, Category Category);
        void Delete(int Id);
    }
}
