﻿using DigitalBookStoreManagement.Expection;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;

namespace DigitalBookStoreManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;

        public UserService(IUserRepo repo)
        {
            this.repo = repo;
        }
        public List<User> GetUserInfo()
        {
            return repo.GetUserInfo();
        }
        public User GetUserInfo(int id)
        {
            User info = repo.GetUserInfo(id);
            if (info == null)
            {
                throw new UserNotFoundExpection($"No User found with id {id}");
            }
            return info;
        }
        //Insert user 
        public int AddUser(User userInfo)
        {
            try
            {
                if (repo.GetUserInfo(userInfo.UserID) != null)
                {
                    throw new UserAlreadyExistsExpection($"The user already exists");
                }
                return repo.AddUser(userInfo);
            }
            catch (UserAlreadyExistsExpection ex)
            {
                // Handle the specific exception
                Console.WriteLine(ex.Message);
                // You can return a specific value or rethrow the exception as needed
                throw;
            }
        }

        //Delete User
        public int RemoveUser(int id)
        {
            if (repo.GetUserInfo(id) == null)
            {
                throw new UserNotFoundExpection($"No user exists with id {id}");
            }
            return repo.RemoveUser(id);
        }

        //Update user 
        public int UpdateUser(int id, User userInfo)
        {
            if (repo.GetUserInfo(id) == null)
            {
                throw new UserNotFoundExpection($"User do not exists with this userid {id}");
            }
            return repo.UpdateUser(id, userInfo);
        }

    }
}
