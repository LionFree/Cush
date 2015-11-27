namespace Cush.Windows.Services.Tests
{
    [WindowsService("Test Service")]
    public class TestService : WindowsService
    {
        public override void OnStart(string[] args)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStop()
        {
            throw new System.NotImplementedException();
        }

        public override void OnCustomCommand(int command)
        {
            throw new System.NotImplementedException();
        }
    }
}