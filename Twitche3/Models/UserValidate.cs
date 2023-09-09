using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitche3.DataAccess;

namespace Twitche3.Models
{
    public class UserValidate
    {
        public static bool Login(string userName, string password)
        {
            UserDAL usersData = new UserDAL();

            var usersList = usersData.GetAllUsers();

            return usersList.Any(user =>
            user.Username.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
            user.Password == password);
        }

        public static bool Exist(string userName)
        {
            UserDAL usersData = new UserDAL();

            var usersList = usersData.GetAllUsers();

            return usersList.Any(user =>
            user.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

    }
}