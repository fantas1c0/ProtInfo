using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;

namespace AplProt.Labs
{
    public class Laba1 : ILabs
    {
        private const string alphabet = "01";
        private const int m = 5;
        public string name { get; } = "LABA1";
        //public ResultStatus Validate() { return ResultStatus.Success; }
        public ResultStatus Validate_key_code(string text, string code) {

            if (code.Length <= 0)
                return new ResultStatus(Status.Failed, $"Length key code = {code.Length}! Is not Valid.");

            if (code.Length >= text.Length / m)
                return new ResultStatus(Status.Warning, "The line can be made shorter"); //Строку можно сделать покароче

            if (code.Length <= text.Length / m)
                return new ResultStatus(Status.Warning, $"The line can be made a bit longer");

            if (!Regex.IsMatch(code, alphabet))
                //throw new Exception("Key code is not valid from alphabet!");
                return new ResultStatus(Status.Failed, "Key code is not valid from alphabet!");

            return new ResultStatus(Status.Success, "");
        }
        public ResultStatus Validate_text_message(string message) {
            if (message.Length <= 0)
                return new ResultStatus(Status.Failed, $"Length key code = {message.Length}! Is not Valid.");

            if (!Regex.IsMatch(message, alphabet))
                //throw new Exception("Key code is not valid from alphabet!");
                return new ResultStatus(Status.Failed, "Message is not valid from alphabet!");

            return new ResultStatus(Status.Success, "");
        }
        public string Encrypt(string text, string key_code)
        {
            if (string.IsNullOrEmpty(key_code)) return text;

            var sb = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                char t = text[i];
                char k = key_code[i % key_code.Length];
                sb.Append(t == k ? '0' : '1'); // XOR над битами
            }
            return sb.ToString();
        }
        public string Decrypt(string text, string key_code) => Encrypt(text, key_code);

        public string RandomKey(int range) {
            StringBuilder result = new StringBuilder(range);
            Random random = Random.Shared;

            for (int i = 0; i < range; ++i) 
            {
                int index = random.Next(alphabet.Length);
                result.Append(alphabet[index]);
            }
            
            return result.ToString();
        }
    }
}

