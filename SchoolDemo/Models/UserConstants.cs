using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SchoolDemo.Models
{
    public class UserConstants
    {
        private readonly IGenericRepository<UserModel> _user;
        private readonly IUnitofWork _unitofWork;
        private readonly StudentContext context;

        public UserConstants(StudentContext studentContext)
        {
            context = studentContext;   
        }
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel () { UserName = "jason_admin", EmailAddress = "jason.admin@email.com", Password = "MyPass_w0rd", GivenName = "Jason", Surname = "Bryant", Role = "Administrator" },
            new UserModel() { UserName = "elyse_seller", EmailAddress = "elyse.seller@email.com", Password = "MyPass_w0rd", GivenName = "Elyse", Surname = "Lambert", Role = "Seller" },
            new UserModel() { UserName = "akhaninamit@gmail.com", EmailAddress = "akhaninamit@gmail.com", Password = "namitakhani214", GivenName = "Elyse", Surname = "Lambert", Role = "Administrator" },

        };
        public List<UserModel> getdata()
        {
            List<UserModel> Usersdemo = new List<UserModel>();
            var itemlist = context.User.ToList();
            foreach (var item in itemlist)
            {
                Usersdemo.Add(new UserModel() { UserName = item.UserName ,Password = item.Password});
            }
            return null;
        }
    }
}
