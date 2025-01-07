using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_3_Backend.src.data;
using Catedra_3_Backend.src.dtos.post;
using Catedra_3_Backend.src.interfaces;
using Catedra_3_Backend.src.models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Catedra_3_Backend.src.repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _dataContext;
        private readonly Cloudinary _cloudinary;

        public PostRepository(DataContext dataContext, Cloudinary cloudinary)
        {
            _dataContext = dataContext;
            _cloudinary = cloudinary;
        }
        public async Task<Post> CreatePost(CreatePostDTO createPost)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Id == createPost.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (createPost.Image == null)
            {
                throw new Exception("Image is required");
            }
            if (createPost.Image.ContentType != "image/png" && createPost.Image.ContentType != "image/jpg")
            {
                throw new Exception("Image must be a PNG or JPG");
            } 
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(createPost.Image.FileName, createPost.Image.OpenReadStream()),
                Folder = "catedra3"
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            if (uploadResult.Error != null)
            {
                throw new Exception("Error uploading image" + uploadResult.Error.Message);
            }
            var post = new Post
            {
                Title = createPost.Title,
                PostDate = createPost.PostDate,
                Url = uploadResult.SecureUrl.AbsoluteUri,
                UserId = createPost.UserId
            };
            await _dataContext.Posts.AddAsync(post);
            await _dataContext.SaveChangesAsync();
            return post;
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            return Task.FromResult(_dataContext.Posts.AsEnumerable());
        }
    }
}