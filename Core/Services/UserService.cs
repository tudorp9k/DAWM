using Core.Dtos;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GradeDto = DataLayer.Dtos.GradeDto;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AuthenticationService authenticationService;

        public UserService(UnitOfWork unitOfWork, AuthenticationService authenticationService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        public List<GradeDto> GetGradesForStudent(int id)
        {
            var grades = unitOfWork.Grades.GetGradesByStudentId(id).ToList();

            return grades.ToGradeDtos();
        }

        public List<GradeDto> GetGradesForAllStudents()
        {
            var grades = unitOfWork.Grades.GetAll();

            return grades.ToGradeDtos();
        }

        public bool RegisterUser(UserAuthDto userRegisterDto)
        {
            if (userRegisterDto == null)
            {
                return false;
            }

            var username = userRegisterDto.Username;
            var password = userRegisterDto.Password;

            bool usernameTaken = unitOfWork.Users.GetByUsername(username) != null;

            if (usernameTaken)
            {
                return false;
            }

            var passwordHash = authenticationService.HashPassword(password);

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                RoleId = userRegisterDto.RoleId
            };

            unitOfWork.Users.Insert(user);

            unitOfWork.SaveChanges();

            return true;
        }

        public string ValidateUser(LoginDto userAuthDto)
        {
            var username = userAuthDto.Username;
            var password = userAuthDto.Password;

            var user = unitOfWork.Users.GetByUsername(username);

            if (user == null)
            {
                return null;
            }

            var authenticated = authenticationService.VerifyPassword(password, user.PasswordHash);

            if (!authenticated)
            {
                return null;
            }

            return authenticationService.GetToken(user);
        }
    }
}
