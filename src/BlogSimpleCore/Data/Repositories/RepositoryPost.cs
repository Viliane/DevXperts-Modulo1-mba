using BlogSimpleCore.Data.Repositories.Interfaces;
using BlogSimpleCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSimpleCore.Data.Repositories
{
    public class RepositoryPost : IRepository<Post>, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public RepositoryPost(ApplicationDbContext context)
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

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.Include(x=> x.Comments).ToList();
        }

        public Post GetById(string id)
        {
            return _context.Posts.Include(x => x.Comments)
                .FirstOrDefault(z => z.Id == int.Parse(id));
        }

        public void Insert(Post entity)
        {
            _context.Posts.Add(entity);
            Save();
        }

        public void Update(Post entity)
        {
            _context.ChangeTracker.Clear();
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void DeleteById(int id)
        {            
            _context.Posts.Remove(_context.Posts.Find(id));
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }          
    }
}
