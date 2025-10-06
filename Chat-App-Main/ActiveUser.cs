using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public static class ActiveUser
    {
        public static int UserId { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static byte[] ProfileImage { get; set; }
        //public static List<int> GroupIDs { get; set; } = new List<int>(); // Grup ID'leri burada tutulacak
    }

}
