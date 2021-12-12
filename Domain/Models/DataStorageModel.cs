namespace Domain.Models
{
    public class DataStorageModel
    {
        public string Data { get; set; }
        public string Nonce { get; set; }

        public DataStorageModel()
        { }

        public DataStorageModel(string data, string nonce)
        {
            Data = data;
            Nonce = nonce;
        }
    }
}