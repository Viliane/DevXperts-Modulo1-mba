using BlogSimpleCore.Data.Repositories.Interfaces;
using BlogSimpleCore.Models;
using BlogSimpleCore.Services.Interfaces;

namespace BlogSimpleCore.Services
{
    public class CommentsServices : ICommentServices
    {
        private readonly IRepository<Comments> _repositoryComment;

        public CommentsServices(IRepository<Comments> repositoryComment)
        {
            _repositoryComment = repositoryComment;
        }

        public void Delete(int id)
        {
            _repositoryComment.DeleteById(id);
        }

        public IEnumerable<Comments> GetAll()
        {
            return _repositoryComment.GetAll();
        }

        public Comments GetById(int id)
        {
            return _repositoryComment.GetById(id.ToString());
        }

        public void Insert(Comments post)
        {
            _repositoryComment.Insert(post);
        }

        public void Update(Comments post)
        {
            _repositoryComment.Update(post);
        }
    }
}
