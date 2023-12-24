namespace WebApp.Authorization.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            ReturnUrl = "/home/index";
        }
        public required string UserName { get; set; }
        public required string Password { get; set; }    
        public required string ReturnUrl { get; set; }
    }
}
