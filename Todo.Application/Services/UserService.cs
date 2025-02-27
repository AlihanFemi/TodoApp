﻿ using Todo.Domain.Models;
using Todo.Domain.Repositories;
using Todo.Domain.Services;

namespace Todo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public void Create(string name, string password)
        {
            var existingUser = _repository.IsExistingName(name);
            if (existingUser)
            {
                throw new InvalidProgramException("User with such name already exists");
            }
            _repository.Create(new User { Name = name, Password = password });
        }
        public void Delete(int id)
        {
            var user = _repository.GetById(id);
            if (user is null)
            {
                throw new InvalidProgramException("User with such id does not exists");
            }
            _repository.Delete(user);
        }
        public User? GetUser(int id)
        {
            var user = _repository.GetById(id);
            if (user is null)
            {
                throw new InvalidProgramException("User with such id does not exists");
            }
            return user;
        }
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _repository.GetAll();
            return users;
        }
        public void Update(int id, string name)
        {
            var user = _repository.GetById(id);
            if (user is null)
            {
                throw new InvalidProgramException("User with such id does not exists");
            }
            if (user.Name == name) return;
            var isExistingUsername = _repository.IsExistingName(name);
            if (isExistingUsername)
            {
                throw new InvalidProgramException("User with such name already exists");
            }
            user.Name = name;
            _repository.Update(user);
        }
    }
}
