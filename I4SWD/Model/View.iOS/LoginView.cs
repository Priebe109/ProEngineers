using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presenter;
using Client.iOS;

namespace View.iOS
{
    public class LoginView
    {
        private LoginViewController Controller { get; set; }

        public LoginView()
        {
            Controller = new LoginViewController {Client = new Client.iOS.Client()};
        }
    }
}
