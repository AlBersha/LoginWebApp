namespace Domain.Models
{
    public class PhoneModel
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Nonce { get; set; }

        public PhoneModel(string username, DataStorageModel phone)
        {
            UserName = username;
            Phone = phone.Data;
            Nonce = phone.Nonce;
        }

        public PhoneModel()
        { }
    }
}