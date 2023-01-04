namespace Service.Interface
{
    public interface ISendingSms
    {
        bool SendMessage(double phone, string message);
    }
}
