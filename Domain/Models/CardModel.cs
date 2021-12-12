namespace Domain.Models
{
    public class CardModel
    {
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public string Nonce { get; set; }

        public CardModel(string username, DataStorageModel model)
        {
            UserName = username;
            CardNumber = model.Data;
            Nonce = model.Nonce;
        }

        public CardModel()
        { }
    }
}