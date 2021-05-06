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
    
    public class PostService : IPostService
    {
        private DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public void AddPost(Post Post)
        {
            _context.Posts.Add(Post);
            _context.SaveChanges();
        }

        public void DeletePost(int PostId)
        {
            Post Post = _context.Posts.Find(PostId);
            if (Post == null) return;
            _context.Posts.Remove(Post);
            _context.SaveChanges();

        }

        public PaginatedResponse<List<Post>> GetAllPosts(PaginationFilter Filter)
        {
            int TotalNumberOfPosts = _context.Posts.Count();
            int PageSize = 10;
            int NumberOfPages = (int)Math.Floor(Convert.ToDecimal(TotalNumberOfPosts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);

            List<Post> PostList = _context.Posts
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
                .ToList();
            return new PaginatedResponse<List<Post>>(PostList, Paginator.Page, NumberOfPages, PageSize);
        }

        public PaginatedResponse<List<Post>> GetArchivedPostsByAuthor(int AuthorId, PaginationFilter Filter)
        {
            int TotalNumberOfPosts = _context.Posts.Count();
            int PageSize = 10;
            int NumberOfPages = (int)Math.Floor(Convert.ToDecimal(TotalNumberOfPosts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);

            List<Post> PostList = _context.Posts
                .Where(x => x.AuthorId == AuthorId && x.Status == Status.Archived)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
                .ToList();
            return new PaginatedResponse<List<Post>>(PostList, Paginator.Page, NumberOfPages, PageSize);
        }

        public Response<object> GetPostById(int PostId)
        {
            Post Post = _context.Posts
                .Where(x => x.Id == PostId)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .FirstOrDefault();
            return new Response<object>(Post);
        }

        public Response<object> GetPostBySlug(string Slug)
        {
            //because slug will not be unique, if it ever happens that two posts have exactly same Title and of course same slug,
            //Then let's make our fetching planned towards obtaining a List of objects from the DB
            IEnumerable<Post> PostList = _context.Posts
                .Where(x => x.Slug == Slug)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .ToList();
            return new Response<object>(PostList);
        }

        public PaginatedResponse<List<Post>> GetPostsByAuthor(int AuthorId, PaginationFilter Filter)
        {
            int TotalNumberOfPosts = _context.Posts.Count();
            int PageSize = 10;
            int NumberOfPages = (int)Math.Floor(Convert.ToDecimal(TotalNumberOfPosts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);

            List<Post> PostList = _context.Posts
                .Where(x => x.AuthorId == AuthorId)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
                .ToList();
            return new PaginatedResponse<List<Post>>(PostList, Paginator.Page, NumberOfPages, PageSize);
        }

        //public Response<object> GetPostsByCategory(int CategoryId)
        //{
        //    Category Category
        //    IEnumerable<Post> PostList = _context.Posts
        //        .Where(x => x.Categor)
        //}

        public PaginatedResponse<List<Post>> GetPostsByDatePublished(DateTime DatePublished, PaginationFilter Filter)
        {
            int TotalNumberOfPosts = _context.Posts.Count();
            int PageSize = 10;
            int NumberOfPages = (int)Math.Floor(Convert.ToDecimal(TotalNumberOfPosts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);

            List<Post> PostList = _context.Posts
                .Where(x => x.DateCreated == DatePublished && x.Status == Status.Published)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
                .ToList();

            return new PaginatedResponse<List<Post>>(PostList, Paginator.Page, NumberOfPages, PageSize);
        }

        public PaginatedResponse<List<Post>> GetPublishedPostsByAuthor(int AuthorId, PaginationFilter Filter)
        {
            int TotalNumberOfPosts = _context.Posts.Count();
            int PageSize = 10;
            int NumberOfPages = (int)Math.Floor(Convert.ToDecimal(TotalNumberOfPosts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);

            List<Post> PostList = _context.Posts
                .Where(x => x.AuthorId == AuthorId && x.Status == Status.Published)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
                .ToList();

            return new PaginatedResponse<List<Post>>(PostList, Paginator.Page, NumberOfPages, PageSize);
        }

        public PaginatedResponse<List<Post>> GetUnpublishedPostsByAuthor(int AuthorId, PaginationFilter Filter)
        {
            int TotalNumberOfPosts = _context.Posts.Count();
            int PageSize = 10;
            int NumberOfPages = (int)Math.Floor(Convert.ToDecimal(TotalNumberOfPosts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);

            List<Post> PostList = _context.Posts
               .Where(x => x.AuthorId == AuthorId && x.Status == Status.Unpublished)
               .Include(x => x.Comments)
               .ThenInclude(x => x.Author)
               .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
               .ToList();

            return new PaginatedResponse<List<Post>>(PostList, Paginator.Page, NumberOfPages, PageSize);
        }

        public void UpdatePost(int PostId, Post Post)
        {
            Post PostToBeUpdated = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
            if (Post == null || PostId != Post.Id) return; // something's not right...

            _context.Entry(Post).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
