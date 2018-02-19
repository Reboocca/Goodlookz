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
    class ShopsChosen
    {
        public int users_id { get; set; }
        public int rubric_id { get; set; }
        public int shops_id { get; set; }
    }

    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    class ShopsChosenSaved
    {
        private static int _shops1_id;
        private static int _shops2_id;
        private static int _shops3_id;
 
        public static int shops1_id
        {
            get { return _shops1_id; }
            set { _shops1_id = value; }
        }

        public static int shops2_id
        {
            get { return _shops2_id; }
            set { _shops2_id = value; }
        }

        public static int shops3_id
        {
            get { return _shops3_id; }
            set { _shops3_id = value; }
        }
    }
}
