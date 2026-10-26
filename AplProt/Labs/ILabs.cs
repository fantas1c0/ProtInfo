namespace AplProt.Labs
{
    
    public interface ILabs
    {
        
        public string name { get; }
        public ResultStatus Validate_key_code(string text, string code);
        public ResultStatus Validate_text_message(string message);
        public string Encrypt(string text, string key_code);
        public string Decrypt(string text, string key_code);
        public string RandomKey(int range);
    }
}
