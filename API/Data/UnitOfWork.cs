using AutoMapper;

namespace API.Data
{
    public class UnitOfWork
    {
        
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        // Implement an interface and setup a repository like this
        // public IUserClientRepository UserClientRepository => new UserClientRepository(_context);

        public async Task<bool> Complete()
        {
            try { return await _context.SaveChangesAsync() > 0; }
            catch { return false; }
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}