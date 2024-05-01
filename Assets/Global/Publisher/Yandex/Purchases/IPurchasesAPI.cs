namespace Global.Publisher.Yandex.Purchases
{
    public interface IPurchasesAPI
    {
        void TryPurchase_Internal(string id);
        void GetProducts_Internal();
    }
}