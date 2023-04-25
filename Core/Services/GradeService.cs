using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GradeService
    {
        private readonly UnitOfWork unitOfWork;

        public GradeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public GradeAddDto AddGrade(GradeAddDto gradeAddDto)
        {
            var grade = new Grade
            {
                Value = gradeAddDto.Value,
                Course = gradeAddDto.Course,
                DateCreated = gradeAddDto.DateCreated,
                StudentId = gradeAddDto.StudentId
            };
            unitOfWork.Grades.Insert(grade);

            unitOfWork.SaveChanges();

            return grade.ToGradeAddDto();
        }
    }
}
