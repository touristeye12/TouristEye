using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TouristEye.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristEye.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        LoginViewModel viewModel;
		public LoginPage()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = new LoginViewModel(Navigation);
            BindingContext = viewModel;
        }
        private void Login_Button(object sender, EventArgs e)
        {
            bool flag = false;
            var validateemail = EmailEntry.Text;
            var validatepassword = PasswordEntry.Text;
            var EmailPattern = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
            if (validateemail == null || validateemail == "")
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Email Required!";
                flag = true;
            }
            else if (!Regex.IsMatch(validateemail, EmailPattern))
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Please Enter Valid Email Address";
                flag = true;
            }
            else if (validatepassword == null || validatepassword == "")
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Password Required!";
                flag = true;
            }
            if (flag == false)
            {
                viewModel.LoginCmd.Execute(null);
                ErrorMessage.IsVisible = false;
            }
        }

        private void ForGot_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }
        private void Signup_Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }
    }
}