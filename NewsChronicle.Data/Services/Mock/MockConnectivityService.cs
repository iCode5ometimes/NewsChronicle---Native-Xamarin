using System;
using NewsChronicle.Data.Interfaces;

namespace NewsChronicle.Data.Services.Mock
{
    public class MockConnectivityService : IConnectivityService
    {
        public bool IsConnected => false;

        public event EventHandler<bool> ConnectivityChanged;

        public MockConnectivityService()
        {
        }
    }
}
