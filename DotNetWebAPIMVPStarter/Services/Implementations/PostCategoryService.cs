using DotNetWebAPIMVPStarter.DAL;
using DotNetWebAPIMVPStarter.Models.Blog;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.Implementations
{
    public class PostCategoryService : IPostCategoryService
    {
        private DataContext _context;

        public PostCategoryService(DataContext context)
        {
            _context = context;
            
        }

        public void Add(Category Category)
        {
            _context.Categories.Add(Category);
            _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            Category Category = _context.Categories.Find(Id);
            if (Category == null) return;
            _context.Categories.Remove(Category);
            _context.SaveChanges();
        }

        public Response<IEnumerable<Category>> GetAll()
        {
            IEnumerable<Category> Categories = _context.Categories.Include(x => x.Posts).ToList();
            return new Response<IEnumerable<Category>>(Categories);
        }

        public Response<Category> GetSingle(int Id)
        {
            Category Category = _context.Categories.Where(x => x.Id == Id).Include(x => x.Posts).FirstOrDefault();
            return new Response<Category>(Category);
        }

        public void Update(int Id, Category Category)
        {
            if (Category == null || Id != Category.Id) return; // something's not right...
            _context.Entry(Category).State = EntityState.Modified;
            _context.SaveChanges(); 
        }
    }
}
