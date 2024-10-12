using BlogSimplesAPI.Models;

namespace BlogSimplesAPI.Services.Interfaces
{
    public interface ICommentServices
    {
        void Insert(Comments comments);
        void Update(Comments comments);
        void Delete(int id);
        IEnumerable<Comments> GetAll();
        Comments GetById(int id);
    }
}
