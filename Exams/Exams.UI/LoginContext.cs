using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI
{
    public enum UserType
    {
        Student,
        Teacher
    }

    public class LoginContext
    {
        IHashFunction _hashFunction;

        public LoginContext(IHashFunction hashFunction)
        {
            _hashFunction = hashFunction;
        }

        public void LoginAsStudent()
        {
            this.UserType = UserType.Student;
        }

        public void LoginAsTeacher(string password)
        {
            this.UserType = UserType.Teacher;
            this.PasswordHash = _hashFunction.Evaluate(password);
        }

        public UserType UserType { get; private set; }
        public string PasswordHash { get; private set; } 
    }
}
