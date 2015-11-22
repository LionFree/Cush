using System.Runtime.Serialization;

namespace Cush.Testing.Mockable
{
    // The assembly class doesn't define a deserializer, so we need to implement one
    // in order to mock an assembly.
    public abstract class Assembly : System.Reflection.Assembly
    {
        public new abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}
