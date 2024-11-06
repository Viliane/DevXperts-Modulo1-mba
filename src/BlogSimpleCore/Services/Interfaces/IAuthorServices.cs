using BlogSimpleCore.Models;

namespace BlogSimpleCore.Services.Interfaces
{
    public interface IAuthorServices
    {
        void RegisterAuthor(Author author);

        Author GetAuthor(string IdentityId);
    }
}
