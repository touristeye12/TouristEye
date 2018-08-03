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
	public partial class SignupPage : ContentPage
	{
        SignupPageViewModel signupPageViewModel;
		public SignupPage()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            signupPageViewModel = new SignupPageViewModel(Navigation);
            BindingContext = signupPageViewModel;
        }
        private void SubmitClicked(object sender, EventArgs e)
        {
            bool flag = false;
            var validateName = NameEntry.Text;
            var validateTele = TeleEntry.Text;
            var validateemail = EmailEntry.Text;
            var validatepassword = PasswordEntry.Text;
            var validateRepassword = RepaswordEntry.Text;
            var validateResidensHospital = ResidensHospitalEntry.Text;
            var EmailPattern = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
            if (validateName == null || validateName == "")
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Name Required!";
                flag = true;
            }
            else if (validateTele == null || validateTele == "")
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "TelePhone Required!";
                flag = true;
            }
            else if (validateemail == null || validateemail == "")
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Email Required";
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
                ErrorMessage.Text = "password Required";
                flag = true;
            }
            else if (validateRepassword.Length < 6)
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Password Must Contain 6 Characters";
                flag = true;
            }

            else if (validateRepassword == null || validateRepassword == "")
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "RePassword Required";
                flag = true;
            }

            else if (validateRepassword != validatepassword)
            {
                flag = true;
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Password did not match!";
            }
            else if (validateResidensHospital == null || validateResidensHospital == "")
            {
                ErrorMessage.IsVisible = true;
                ErrorMessage.Text = "Resident Hospital Required!";
                flag = true;
            }
            if (flag == false)
            {
                signupPageViewModel.LoginCmd.Execute(null);
                ErrorMessage.IsVisible = false;
            }
        }
    }
}