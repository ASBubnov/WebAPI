using DAL.Interfaces;
using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User CreateUser(string firstName, string middleName, string lastName,string email);
        User FindUser(int userId);
        User Delete(int userID);
        User Reestablish(int userId);
        void HardDelete(int userID);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User CreateUser(string firstName, string middleName, string lastName, string email)
        {
            var user = new User()
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                FullName = $"{lastName} {firstName} {middleName}",
                Email = email,
                isBlocked = false

            };            
            return _repository.Create(user);
        }

        public User FindUser(int userId)
        {
            return _repository.FindByID(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            var user = new User();
            Func<User, bool> selector = user => !user.isBlocked;
            return _repository.Find(selector);
        }

        public User Delete(int userId)
        {
            var user = _repository.FindByID(userId);
            if(user != null)
            {   
                user.isBlocked = true;
                return _repository.Update(user);
            }
            else
            {
                return null;
            }
        }

        public User Reestablish(int userId)
        {
            var user = _repository.FindByID(userId);
            if (user != null)
            {
                user.isBlocked = false;
                return _repository.Update(user);
            }
            else
            {
                return null;
            }
        }

        public void HardDelete(int userId)
        {
            var user = _repository.FindByID(userId);
            if (user != null)
            {
                _repository.Delete(userId);
            }            
        }
    }
}
