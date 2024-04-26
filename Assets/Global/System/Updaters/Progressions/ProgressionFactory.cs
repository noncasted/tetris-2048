using Global.System.Updaters.Abstract;

namespace Global.System.Updaters.Progressions
{
    public class ProgressionFactory : IProgressionFactory
    {
        public ProgressionFactory(IUpdater updater)
        {
            _updater = updater;
        }

        private readonly IUpdater _updater;
        
        public IProgressionHandle CreateHandle()
        {
            var handle = new ProgressionHandle(_updater);

            return handle;
        }
    }
}