using System;
using NewsChronicle.Data.Interfaces;
using Xamarin.Essentials;

namespace NewsChronicle.Data.Services
{
    internal class ConnectivityService : IConnectivityService
    {
        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public event EventHandler<bool> ConnectivityChanged;

        public ConnectivityService()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, IsConnected);
        }

    }
}
