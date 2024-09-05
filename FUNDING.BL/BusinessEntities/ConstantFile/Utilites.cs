using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FUNDING.BL.BusinessEntities.Masters;
using System.Globalization;
using System.Text.RegularExpressions;
namespace FUNDING.BL.BusinessEntities.ConstantFile
{
  public  class Utilites
    {

        AuditLogs al = new AuditLogs();
       
        public static String RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "/[^a-zA-Z0-9\\s\\/]/", "", RegexOptions.IgnoreCase);
        }

        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        public static void logfile(String ClassName, String ErrorNumber, String ErrorMessage)
        {
            System.IO.StreamWriter sw = null;
            String Message = "";


            try
            {
                string path = @"D:\Logs\Clogs.txt";
                sw = new System.IO.StreamWriter(path, true);
                Message = Environment.NewLine + "Date: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + Environment.NewLine + "ClassName: " + ClassName + Environment.NewLine + "ErrorNumber: " + ErrorNumber + Environment.NewLine + "ErrorMessage: " + ErrorMessage + Environment.NewLine;
                sw.WriteLine(Message);
            }
            catch (Exception ex)
            {
                try
                {
                    String exception = ex.ToString();
                    sw = new System.IO.StreamWriter("~/Logs/Clogs.txt", true);
                    //sw = new System.IO.StreamWriter(@"D:\SAIT\logs\aarogyasri.txt", true);
                    Message = Environment.NewLine + "Date: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + Environment.NewLine + "ClassName: Debug" + Environment.NewLine + "ErrorMessage:" + ex.ToString() + Environment.NewLine;
                    sw.WriteLine(Message);
                }
                catch (Exception Ex1)
                {
                    Ex1.Message.ToString();
                }

            }
            finally
            {
                sw.Close();
            }


        }
        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        public static string DecodeFrom64(string password)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
    }
}
