using Common.DataTypes.Runtime.Reactive;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.UI.Design.Runtime.DataBindings
{
    public static class DesignTextBindExtensions
    {
        public static void Construct(
            this DesignTextBind bind,
            IReadOnlyLifetime lifetime,
            IViewableProperty<string> property)
        {
            bind.Construct(lifetime, property, value => value);
        }

        public static void Construct(
            this DesignTextBind bind,
            IReadOnlyLifetime lifetime,
            IViewableProperty<int> property)
        {
            bind.Construct(lifetime, property, value => value.ToString());
        }
    }
}