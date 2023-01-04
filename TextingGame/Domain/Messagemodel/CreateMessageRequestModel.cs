namespace Domain.Messagemodel
{
    public class CreateMessageRequestModel
    {
        public int? RoomId { get; set; }
        public string Message { get; set; } = null!;
    }
}
