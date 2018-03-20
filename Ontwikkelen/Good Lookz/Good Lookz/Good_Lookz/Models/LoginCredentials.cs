using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
    /// <summary>
    /// Values dat opgeslagen moet worden in een class.
    /// </summary>
    class LoginCredentials
    {
        private static string _loginId;
        private static string _loginUsername;
        private static string _loginPassword;
        private static string _loginFirstname;
        private static string _loginLastname;
        private static string _loginEmail;
        private static string _loginDate;
        private static string _loginGender;
        private static string _loginOffline;
        private static string _loginActive;

		public static double adjust_lighting = 1;

        public static string loginId
        {
            get { return _loginId; }
            set { _loginId = value; }
        }

        public static string loginUsername
        {
            get { return _loginUsername; }
            set { _loginUsername = value; }
        }

        public static string loginPassword
        {
            get { return _loginPassword; }
            set { _loginPassword = value; }
        }

        public static string loginFirstname
        {
            get { return _loginFirstname; }
            set { _loginFirstname = value; }
        }

        public static string loginLastname
        {
            get { return _loginLastname; }
            set { _loginLastname = value; }
        }

        public static string loginEmail
        {
            get { return _loginEmail; }
            set { _loginEmail = value; }
        }

        public static string loginDate
        {
            get { return _loginDate; }
            set { _loginDate = value; }
        }

        public static string loginGender
        {
            get { return _loginGender; }
            set { _loginGender = value; }
        }

        public static string loginOffline
        {
            get { return _loginOffline; }
            set { _loginOffline = value; }
        }

        public static string loginActive
        {
            get { return _loginActive; }
            set { _loginActive = value; }
        }
    }
}
