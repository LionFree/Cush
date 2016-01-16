using System;
using System.ServiceModel.Channels;

namespace Cush.Windows.Services
{
    public class WindowsServiceDescription
    {
        
        public object ServiceObject;
        public Type ServiceType;
        public Endpoint[] Endpoints;
    }

    public class Endpoint
    {
        public object ServiceObject;
        public Type ServiceType;

        public Binding Binding;
        public string Path;
        public Type Contract;
        public int Port = -1;
    }
}