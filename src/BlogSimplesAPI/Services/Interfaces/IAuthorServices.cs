using BlogSimplesAPI.Models;

namespace BlogSimplesAPI.Services.Interfaces
{
    public interface IAuthorServices
    {
        void RegisterAuthor(Author author);   
        
        Author GetAuthor(string IdentityId);
    }
}
