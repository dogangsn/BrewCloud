namespace VetSystems.IdentityServer.Models
{
    public class ResetPasswordDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string Confirm { get; set; }
    }
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
