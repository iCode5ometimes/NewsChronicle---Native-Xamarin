using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NewsChronicle.Data.Interfaces;

namespace NewsChronicle.Data.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public bool IsConnectedToInternet => _connectivityService.IsConnected;

        public string AppArticleLanguage => _appLanguageSetting.GetAppArticleLanguage();

        #endregion

        #region Constructor(s) (and Dependencies)

        protected readonly IConnectivityService _connectivityService;
        protected readonly IAppLanguageSetting _appLanguageSetting;

        public BaseViewModel(IConnectivityService connectivityService,
                             IAppLanguageSetting appLanguageSetting)
        {
            _connectivityService = connectivityService ?? throw new ArgumentNullException(nameof(connectivityService));
            _appLanguageSetting = appLanguageSetting ?? throw new ArgumentNullException(nameof(appLanguageSetting));
        }

        public BaseViewModel(IConnectivityService connectivityService)
        {
            _connectivityService = connectivityService ?? throw new ArgumentNullException(nameof(connectivityService)); 
        }

        public BaseViewModel(IAppLanguageSetting appLanguageSetting)
        {
            _appLanguageSetting = appLanguageSetting ?? throw new ArgumentNullException(nameof(appLanguageSetting));
        }

        public BaseViewModel()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Subscribe to internet connection monitor on appearing
        /// </summary>
        public virtual void OnAppearing()
        {
            _connectivityService.ConnectivityChanged += ConnectivityService_ConnectivityChanged;
        }

        /// <summary>
        /// Unsubscribe from internet connection monitor on dissapearing
        /// </summary>
        public virtual void OnDisappearing()
        {
            _connectivityService.ConnectivityChanged -= ConnectivityService_ConnectivityChanged;
        }

        protected virtual void InternalOnConnectivityChanged(bool isConnected)
        {
            OnPropertyChanged(nameof(IsConnectedToInternet));
        }

        private void ConnectivityService_ConnectivityChanged(object sender, bool isConnected)
        {
            InternalOnConnectivityChanged(isConnected);
        }

        public void SetAppArticleLanguage(string language)
        {
            _appLanguageSetting.SetAppArticleLanguage(language);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
