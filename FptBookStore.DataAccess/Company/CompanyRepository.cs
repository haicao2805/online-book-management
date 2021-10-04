using FptBookStore.DataAccess.Data;
using FptBookStore.DataAccess.BaseRepository;

namespace FptBookStore.DataAccess.Company
{
    public class CompanyRepository : Repository<Entities.Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.Company;
        }
    }
}
