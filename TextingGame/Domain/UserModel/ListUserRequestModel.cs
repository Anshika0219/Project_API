namespace Domain.UserModel
{
    public class ListUserRequestModel
    {
        public string? UserName { get; set; }

        public string EmailId { get; set; } = null!;
        public string? MobileNo { get; set; }
    }
}
