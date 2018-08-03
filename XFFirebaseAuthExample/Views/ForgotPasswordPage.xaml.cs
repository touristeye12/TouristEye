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
	public partial class ForgotPasswordPage : ContentPage
	{
        ForgotPasswordViewModel viewModel;
		public ForgotPasswordPage()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = new ForgotPasswordViewModel(Navigation);
            BindingContext = viewModel;
        }
        protected async void Submit(object s, EventArgs e)
        {
            bool flag = false;
            var validateemail = ValidateEmail.Text;
            var EmailPattern = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
            if (validateemail == null || validateemail == "")
            {
                ValidateLabelEmail.IsVisible = true;
                ValidateLabelEmail.Text = "Email Address Required";
                flag = true;
            }
            else if (!Regex.IsMatch(validateemail, EmailPattern))
            {
                ValidateLabelEmail.IsVisible = true;
                ValidateLabelEmail.Text = "Please Enter Valid Email Address";
                flag = true;
            }
            else if (validateemail != null || validateemail != "")
            {
                ValidateLabelEmail.IsVisible = false;
                flag = false;
            }
            if (flag == false)
            {
                viewModel.navigateCmd.Execute(null);
            }
        }
    }
}