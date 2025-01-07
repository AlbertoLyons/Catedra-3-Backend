using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_3_Backend.src.dtos.post;
using Catedra_3_Backend.src.models;

namespace Catedra_3_Backend.src.interfaces
{
    public interface IPostRepository
    {
        public Task<Post> CreatePost(CreatePostDTO createPost);
        public Task<IEnumerable<Post>> GetPosts();
    }
}