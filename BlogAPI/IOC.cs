using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices;

namespace BlogAPI
{
    public class IOC
    {
        public static AdminService adminService = new AdminService();
        public static UserService userService = new UserService();
    }
}
