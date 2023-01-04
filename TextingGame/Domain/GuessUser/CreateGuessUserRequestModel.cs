namespace Domain.GuessUser
{
    public class CreateGuessUserRequestModel
    {
        public int? UserId { get; set; }
        public int ImpersonatedUserId { get; set; }
    }
}
