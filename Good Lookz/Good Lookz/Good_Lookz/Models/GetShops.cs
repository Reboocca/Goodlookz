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
    class GetShops
    {
        public string shops_id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string url { get; set; }
        public string active { get; set; }
    }
}
