using System;
namespace NewsChronicle.Data.Interfaces
{
    public interface IConnectivityService
    {
        bool IsConnected { get; }
        event EventHandler<bool> ConnectivityChanged;
    }
}
