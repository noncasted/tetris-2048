using Global.UI.Localizations.Texts;
using UnityEngine;

namespace Global.Publisher.Abstract.Purchases
{
    public interface IProductLink
    {
        Sprite ShopIcon { get; }
        LanguageTextData Description { get; }
        string Id { get; }
        int Price { get; }
        PaymentMethod PaymentMethod { get; }

        void UpdatePrice(int price);
    }
}