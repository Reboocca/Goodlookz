using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
    /// <summary>
    /// JSON response word opgevangen en in een list met variables gestopt.
    /// </summary>
    public class LoginAccount
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public string gender { get; set; }
        public string offline { get; set; }
        public string active { get; set; }
        public bool blocked { get; set; }
    }
}
