using BlogSimplesMvc.UI.Models;

namespace BlogSimplesMvc.UI.Services.Interfaces
{
    public interface ICommentServices
    {
        void Insert(Comments comments);
        void Update(Comments comments);
        void Delete(int id);
        void GetAll();
        Comments GetById(int id);
    }
}
