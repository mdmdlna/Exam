using System.Collections.Generic;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class DepartmentRepository : Repository<department>
    {
        public department GetModelById(int id)
        {
            return Db.Context.From<department>().Where(q => q.Id == id).ToFirst();
        }


        public List<department> GetAllByDeptId(int deptId)
        {
            return Db.Context.From<department>().Where(q => q.Id == deptId).ToList();
        }
    }
}
