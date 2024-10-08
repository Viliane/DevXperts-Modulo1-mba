using BlogSimplesAPI.Data.Repositories.Interfaces;
using BlogSimplesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSimplesAPI.Data.Repositories
{
    public class RepositoryComment : IRepository<Comments>, IDisposable
    {
        private readonly ApiDbContext _context;
        private bool _disposed = false;

        public RepositoryComment(ApiDbContext context)
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

        public IEnumerable<Comments> GetAll()
        {
            return _context.Comments.ToList();
        }

        public Comments GetById(string id)
        {
            return _context.Comments.FirstOrDefault(x => x.Id == int.Parse(id));
        }

        public void Insert(Comments entity)
        {
            _context.Comments.Add(entity);
            Save();
        }

        public void Update(Comments entity)
        {
            _context.ChangeTracker.Clear();
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void DeleteById(int id)
        {
            _context.Comments.Remove(_context.Comments.Find(id));
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
