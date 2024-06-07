using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;


namespace Developers.Recursos
{
    public class Utilidade
    {

        public static string EncriptarContrasena(string Password)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(Password));

                foreach (byte b in result) 
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();  
        }

    }
}
