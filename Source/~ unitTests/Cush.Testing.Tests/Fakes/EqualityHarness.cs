using System.Diagnostics.CodeAnalysis;

namespace Cush.Testing.Tests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class EqualityHarness
    {
        private static bool _equalsOperatorReturns;
        private static int _lastHashcode;
        private readonly bool _equalsReturns;
        private readonly bool _hashcodeSame;

        public EqualityHarness(bool equalityMethodReturns, bool equalityOperatorReturns, bool hashcodeSame = true)
        {
            _equalsReturns = equalityMethodReturns;
            _equalsOperatorReturns = equalityOperatorReturns;
            _hashcodeSame = hashcodeSame;
        }

        public override bool Equals(object other)
        {
            return _equalsReturns;
        }

        public static bool operator ==(EqualityHarness a, EqualityHarness b)
        {
            return _equalsOperatorReturns;
        }

        public static bool operator !=(EqualityHarness a, EqualityHarness b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            if (_hashcodeSame) return 0;

            _lastHashcode = GetRandom.Int();
            return NewRandom.Int(_lastHashcode);
        }
    }
}