using DotNetWebAPIMVPStarter.Models.Blog;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.Interfaces
{
    public interface IPostService
    {
        PaginatedResponse<List<Post>> GetAllPosts(PaginationFilter Filter);
        Response<object> GetPostById(int PostId);
        Response<object> GetPostBySlug(string Slug);
        //Response<object> GetPostsByCategory(int CategoryId);
        PaginatedResponse<List<Post>> GetPostsByAuthor(int AuthorId, PaginationFilter Filter); //or UserId
        PaginatedResponse<List<Post>> GetPostsByDatePublished(DateTime DatePublished, PaginationFilter Filter);
        PaginatedResponse<List<Post>> GetUnpublishedPostsByAuthor(int AuthorId, PaginationFilter Filter);
        PaginatedResponse<List<Post>> GetPublishedPostsByAuthor(int AuthorId, PaginationFilter Filter);
        PaginatedResponse<List<Post>> GetArchivedPostsByAuthor(int AuthorId, PaginationFilter Filter); //status is archived....

        void AddPost(Post Post); // I am going to create a Resource for this using AutoMapper... via a CreatePostModel
        void UpdatePost(int PostId, Post Post);
        void DeletePost(int PostId);



    }
}
