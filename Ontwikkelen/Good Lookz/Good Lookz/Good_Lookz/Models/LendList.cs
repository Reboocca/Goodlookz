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
    class LendList
    {
        public string lend_id { get; set; }
        public string id { get; set; }
        public string picture { get; set; }
        public string active { get; set; }
        public string username { get; set; }
    }
}
