using System.Text;

namespace AplProt.Labs
{
    public class Laba2 : ILabs
    {
        public const string alphabet = "10";
        public string name { get; } = "TYTYT_2";
        //public ResultStatus Validate() { return ResultStatus.Warning; }
        public ResultStatus Validate_key_code(string text, string code)
        {
            if (code == "a")
            {
                return new ResultStatus(Status.Warning, "Tessst11");
            }
            return new ResultStatus(Status.Success, "Win11");
        }
        public ResultStatus Validate_text_message(string message)
        {
            if (message == "a")
            {
                return new ResultStatus(Status.Warning, "Test12");
            }
            return new ResultStatus(Status.Success, "Win12");
        }
        public string Encrypt(string text, string key_code) { return "string12"; }
        public string Decrypt(string text, string key_code) { return "string22"; }


        public string RandomKey(int range)
        {
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
