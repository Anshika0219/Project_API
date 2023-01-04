namespace Domain.UserModel
{
    public class CreateUserRequestmodel
    {
        public string? Name { get; set; }

        public string EmailId { get; set; } = null!;

        public string? Password { get; set; }

        public string? MobileNo { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
