namespace AccountExternalModel
{
    public class ApiCredential
    {
        private string _grant_type { get; set; }

        public string grant_type
        {
            get
            {
                if (string.IsNullOrEmpty(_grant_type))
                    return "password";

                return _grant_type;
            }
            set
            {
                _grant_type = value;
            }
        }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
