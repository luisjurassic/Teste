namespace Bludata
{
    public class Helper
    {
        public static string RemoverCaracteres(string v)
        {
            if (string.IsNullOrWhiteSpace(v))
                return null;

            return v.Replace("(", "")
             .Replace(")", "")
             .Replace(".", "")
             .Replace("-", "")
             .Replace("/", "");
        }
    }  
}