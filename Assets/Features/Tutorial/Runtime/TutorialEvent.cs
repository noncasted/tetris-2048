namespace Features.Tutorial.Runtime
{
    public class TutorialEnableEvent
    {
        public TutorialEnableEvent(TutorialSwitchKey key)
        {
            Key = key;
        }
        
        public TutorialSwitchKey Key { get; }
    }
    
    public class TutorialDisableEvent
    {
        public TutorialDisableEvent(TutorialSwitchKey key)
        {
            Key = key;
        }
        
        public TutorialSwitchKey Key { get; }
    }
}