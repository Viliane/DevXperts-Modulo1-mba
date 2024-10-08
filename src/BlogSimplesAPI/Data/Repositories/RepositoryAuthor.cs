using BlogSimplesAPI.Data.Repositories.Interfaces;
using BlogSimplesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSimplesAPI.Data.Repositories
{
    public class RepositoryAuthor : IRepository<Author>, IDisposable
    {
        private readonly ApiDbContext _context;
        private bool _disposed = false;

        public RepositoryAuthor(ApiDbContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.Include(x=> x.Posts)
                .ThenInclude(y => y.Comments).ToList();
        }

        public Author GetById(string id)
        {
            return _context.Authors.Include(x => x.Posts)
                .ThenInclude(y => y.Comments).FirstOrDefault(z=> z.Id == id);
        }

        public void Insert(Author entity)
        {
            _context.Authors.Add(entity);
            Save();
        }

        public void Update(Author entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void DeleteById(int id)
        {
            _context.Authors.Remove(_context.Authors.Find(id));
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }          
    }
}
