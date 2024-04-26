using Internal.Scopes.Abstract.Lifetimes;

namespace Global.System.Updaters.Abstract
{
    public interface IUpdater
    {
        void Add(IUpdatable updatable);
        void Add(IReadOnlyLifetime lifetime, IUpdatable updatable);
        void Remove(IUpdatable updatable);

        void Add(IPreUpdatable updatable);
        void Add(IReadOnlyLifetime lifetime, IPreUpdatable updatable);
        void Remove(IPreUpdatable updatable);

        void Add(IPreFixedUpdatable updatable);
        void Add(IReadOnlyLifetime lifetime, IPreFixedUpdatable updatable);
        void Remove(IPreFixedUpdatable updatable);

        void Add(IFixedUpdatable updatable);
        void Add(IReadOnlyLifetime lifetime, IFixedUpdatable updatable);
        void Remove(IFixedUpdatable updatable);

        void Add(IPostFixedUpdatable updatable);
        void Add(IReadOnlyLifetime lifetime, IPostFixedUpdatable updatable);
        void Remove(IPostFixedUpdatable updatable);
        
        void Add(IGizmosUpdatable updatable);
        void Add(IReadOnlyLifetime lifetime, IGizmosUpdatable updatable);
        void Remove(IGizmosUpdatable updatable);
    }
}