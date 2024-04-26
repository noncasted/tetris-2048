using VContainer;

namespace Internal.Scopes.Runtime.Containers
{
    public class InstanceInjection
    {
        public InstanceInjection(object target)
        {
            Target = target;
        }

        public readonly object Target;

        public void Inject(IObjectResolver resolver)
        {
            resolver.Inject(Target);
        }
    }
}