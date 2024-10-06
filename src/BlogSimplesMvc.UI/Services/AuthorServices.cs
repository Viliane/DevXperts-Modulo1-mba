using BlogSimplesMvc.UI.Data.Repositories.Interfaces;
using BlogSimplesMvc.UI.Models;
using BlogSimplesMvc.UI.Services.Interfaces;

namespace BlogSimplesMvc.UI.Services
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
