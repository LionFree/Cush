using System.Runtime.Remoting.Messaging;

namespace Cush.Exceptions
{
    public interface IUserFriendlyException
    {
        string UserFacingMessageResourceKey { get; }
    }
}