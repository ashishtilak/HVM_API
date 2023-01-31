
namespace HVM_API.Helper
{
    public class Helper
    {
        public static string Encode(string plainText)
        {
            //encrypt data
            byte[] data = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(data);
        }

        public static string Decode(string base64EncodedData)
        {
            var encodedData = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(encodedData);
        }
    }
}
