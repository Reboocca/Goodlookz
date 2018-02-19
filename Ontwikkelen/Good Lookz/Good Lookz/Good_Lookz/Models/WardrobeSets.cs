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
    public class WardrobeSets
    {
        public string wardrobe_id { get; set; }
        public string users_id { get; set; }
        public string name { get; set; }
        public string head_id { get; set; }
        public string top_id { get; set; }
        public string bottom_id { get; set; }
        public string feet_id { get; set; }
        public string allId { get { return head_id + " - " + top_id + " - " + bottom_id + " - " + feet_id; }}
    }
}
