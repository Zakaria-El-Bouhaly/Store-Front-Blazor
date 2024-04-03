using FrontStore.Model;

namespace FrontStore.Services
{
    public interface IAuthService
    {
        Task SignIn(LoginRequest request);
        Task SignUp(RegisterRequest request);
        Task SignOut();
        
    }
}
