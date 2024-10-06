using BlogSimplesMvc.UI.Models;

namespace BlogSimplesMvc.UI.Services.Interfaces
{
    public interface IAuthorServices
    {
        void RegisterAuthor(Author author);   
        
        Author GetAuthor(string IdentityId);
    }
}
