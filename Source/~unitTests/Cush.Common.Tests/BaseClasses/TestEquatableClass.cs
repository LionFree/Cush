namespace Cush.Common.Tests
{
    internal class TestEquatableClass : EquatableBase
    {
        private readonly int _code;
        private readonly bool _equal;

        public TestEquatableClass() : this(true, 12345)
        {
        }

        public TestEquatableClass(bool equal, int code)
        {
            _equal = equal;
            _code = code;
        }

        public override bool Equals(object obj)
        {
            return _equal;
        }

        public override int GetHashCode()
        {
            return _code;
        }
    }
}