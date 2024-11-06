using BlogSimpleCore.Data.Repositories.Interfaces;
using BlogSimpleCore.Models;
using BlogSimpleCore.Services.Interfaces;

namespace BlogSimpleCore.Services
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IRepository<Author> _repositoryAuthor;

        public AuthorServices(IRepository<Author> repositoryAuthor)
        {
            _repositoryAuthor = repositoryAuthor;
            
        }

        public Author GetAuthor(string IdentityId)
        {
            return _repositoryAuthor.GetById(IdentityId);
        }

        public void RegisterAuthor(Author author)
        {
            _repositoryAuthor.Insert(author);
        }
    }
}
