﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ITAPP_CarWorkshopService.Authorization;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class UserManager
    {
        public static Mutex mutex = new Mutex();

        public static User GetUser(int userId)
        {
            if (!CheckIfUserExists(userId))
            {
                throw NoUserOfGivenId(userId);
            }

            mutex.WaitOne();
            var db = new ITAPPCarWorkshopServiceDBEntities();
            User user = db.Users.FirstOrDefault(n => n.User_ID == userId);
            mutex.ReleaseMutex();

            user.User_password = "******";

            return user;
        }

        public static string RegisterUser(User user)
        {
            user.User_email = UserEmailAdjustment(user.User_email);

            if(CheckIfUserExists(user.User_email))
            {
                return "User of given email already exists.";
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();

            mutex.WaitOne();
            db.Users.Add(user);
            db.SaveChanges();
            mutex.ReleaseMutex();

            return "User was registered.";
        }

        public static string Login(User user)
        {
            string email = UserEmailAdjustment(user.User_email);
            string password = user.User_password;

            var db = new ITAPPCarWorkshopServiceDBEntities();

            if (!CheckIfUserExists(user.User_email))
            {
                throw NoUserOfGivenEmail(email);
            }

            mutex.WaitOne();
            User User = db.Users.FirstOrDefault(record => record.User_email.ToLower() == email);
            mutex.ReleaseMutex();

            if (User.User_password.Equals(password))
            {
                return GenerateTokenForUser(email);
            }
            else
            {
                throw WrongPassword(email);
            }
        }

        public static void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            mutex.WaitOne();
            User User;

            var db = new ITAPPCarWorkshopServiceDBEntities();

            User = db.Users.FirstOrDefault(user => user.User_ID == userId);

            if (User == null)
            {
                mutex.ReleaseMutex();
                throw NoUserOfGivenId(userId);
            }

            if (User.User_password.Equals(oldPassword))
            {
                User.User_password = newPassword;
                db.SaveChanges();
            }
            else
            {
                mutex.ReleaseMutex();
                throw WrongPassword(userId);
            }

            mutex.ReleaseMutex();
        }

        private static string GenerateTokenForUser(string email)
        {
            int userId;
            Token token;
            email = UserEmailAdjustment(email);

            if (!CheckIfUserExists(email))
            {
                throw NoUserOfGivenEmail(email);
            }

            userId = GetUserIdByUserEmailPrivate(email);
            token = new Token(userId);
            token.RegisterToken();
            string result = token.TokenString;
            return result;
        }

        public static int GetUserIdByUserEmail(string email)
        {
            int userId;

            mutex.WaitOne();

            userId = GetUserIdByUserEmailPrivate(email);

            mutex.ReleaseMutex();

            return userId;
        }

        private static int GetUserIdByUserEmailPrivate(string email)
        {
            User User;
            email = UserEmailAdjustment(email);
            var db = new ITAPPCarWorkshopServiceDBEntities();

            User = db.Users.FirstOrDefault(user => user.User_email.ToLower() == email);


            if (User == null)
            {
                throw NoUserOfGivenEmail(email);
            }

            return User.User_ID;
        }

        private static bool CheckIfUserExists(int userId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            if (db.Users.Any(user => user.User_ID == userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckIfUserExists(string userEmail)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            userEmail = UserEmailAdjustment(userEmail);

            if (db.Users.Any(user => user.User_email == userEmail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Exception NoUserOfGivenEmail(string email)
        {
            string exceptionMessage;
            exceptionMessage = "User with email: ";
            exceptionMessage += email;
            exceptionMessage += " does not exists.";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static Exception NoUserOfGivenId(int userId)
        {
            string exceptionMessage;
            exceptionMessage = "User with id: ";
            exceptionMessage += userId;
            exceptionMessage += " does not exists.";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static Exception WrongPassword(string email)
        {
            string exceptionMessage;
            exceptionMessage = "Wrong password for user: ";
            exceptionMessage += email;
            exceptionMessage += ".";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static Exception WrongPassword(int id)
        {
            string exceptionMessage;
            exceptionMessage = "Wrong password for user id: ";
            exceptionMessage += id;
            exceptionMessage += ".";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static string UserEmailAdjustment(string oldString)
        {
            string newString = oldString;
            newString = StringAdjustment.RemoveSpaces(newString);
            newString = newString.ToLower();
            return newString;
        }
    }
}