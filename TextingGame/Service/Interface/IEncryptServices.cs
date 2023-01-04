namespace Service.Interface
{
    public interface IEncryptServices
    {
        string EncodePasswordToBase64(string password);
        string DecodeFrom64(string encodedData);
    }
}