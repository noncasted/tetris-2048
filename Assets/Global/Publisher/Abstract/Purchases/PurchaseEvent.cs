namespace Global.Publisher.Abstract.Purchases
{
    public readonly struct PurchaseEvent
    {
        public PurchaseEvent(IProductLink productLink)
        {
            ProductLink = productLink;
        }

        public readonly IProductLink ProductLink;
    }
}