using BlogSimpleCore.Models;

namespace BlogSimpleCore.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);        
        void Insert(T entity);
        void Update(T entity);
        void DeleteById(int id);        
        void Save();
    }
}
