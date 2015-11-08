using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.CustomPrinciples
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, new UserRole[] {})
        {
        }
    }
}