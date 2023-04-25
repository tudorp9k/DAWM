using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class GradesRepository : RepositoryBase<Grade>
    {
        public GradesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Grade> GetGradesByStudentId(int studentId)
        {
            return GetRecords()
                .Where(g => g.StudentId == studentId)
                .ToList();
        }
    }
}
