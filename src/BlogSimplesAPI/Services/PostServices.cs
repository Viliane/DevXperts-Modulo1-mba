using BlogSimplesAPI.Data.Repositories.Interfaces;
using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;

namespace BlogSimplesAPI.Services
{
    public class PostServices : IPostServices
    {
        private readonly IRepository<Post> _repositoryPost;
        private readonly IRepository<Author> _repositoryAuthor;

        public PostServices(IRepository<Post> repositoryPost, IRepository<Author> repositoryAuthor)
        {
            _repositoryPost = repositoryPost;
            _repositoryAuthor = repositoryAuthor;
        }

        public void Delete(int id)
        {
            _repositoryPost.DeleteById(id);
        }

        public IEnumerable<Post> GetAll()
        {
            var posts = _repositoryPost.GetAll();

            foreach (var post in posts) {
                post.Author = _repositoryAuthor.GetById(post.AuthorId);
            }

            return posts;
        }

        public Post GetById(int id)
        {
            return _repositoryPost.GetById(id.ToString());
        }

        public void Insert(Post post)
        {
            _repositoryPost.Insert(post);
        }

        public void Update(Post post)
        {
            _repositoryPost.Update(post);
        }
    }
}
