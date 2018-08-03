using System.Threading.Tasks;
using Firebase.Auth;
using elloore.Services;

namespace elloore.iOS.Services
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInAsync(email, password);
            return await user.GetIdTokenAsync();
        }

        public async Task<string> SignUpWithEmailPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
            return await user.GetIdTokenAsync();
        }
    }
}
