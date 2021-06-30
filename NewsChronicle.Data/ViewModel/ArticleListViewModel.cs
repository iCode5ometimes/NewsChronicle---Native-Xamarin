using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NewsChronicle.Data.Exceptions;
using NewsChronicle.Data.Interfaces;
using NewsChronicle.Data.Model;
using NewsChronicle.Data.QueryParameters;
using Xamarin.Essentials;

namespace NewsChronicle.Data.ViewModel
{
    public class ArticleListViewModel : BaseViewModel
    {
        #region Fields and Properties

        private List<Article> _articleList = new List<Article>();
        public List<Article> ArticleList
        {
            get => _articleList;
            set => SetProperty(ref _articleList, value);
        }

        private Article _article;
        public Article SelectedArticle
        {
            get => _article;
            set => SetProperty(ref _article, value);
        }

        private MyQueryParams _queryParams;
        public MyQueryParams QueryParams
        {
            get => _queryParams;
            set => SetProperty(ref _queryParams, value, onChanged: OnSearchQueryChanged);
        }

        /// <summary>
        /// True at first start up so the "No results found" label wont show when there are no articles loaded yet
        /// </summary>
        private bool _appIsFirstTimeLoaded = true;
        public bool AppIsFirstTimeLoaded
        {
            get => _appIsFirstTimeLoaded;
            set => SetProperty(ref _appIsFirstTimeLoaded, value);
        }

        private CancellationTokenSource _cts = null;

        #endregion

        #region Events

        public event EventHandler DataSetChanged;

        #endregion

        #region Constructor(s) (and Dependencies)

        private readonly IArticleService        _articleService;
        private readonly IAlertService          _alertService;

        public ArticleListViewModel(IConnectivityService connectivityService,
                                    IArticleService articleService,
                                    IAlertService alertService,
                                    IAppLanguageSetting appLanguageSetting) : base(connectivityService, appLanguageSetting)
        {
            _articleService = articleService      ?? throw new ArgumentNullException(nameof(articleService));
            _alertService   = alertService        ?? throw new ArgumentNullException(nameof(alertService));
        }

        #endregion

        #region Methods

        private async void OnSearchQueryChanged()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            try
            {
                if (!AppIsFirstTimeLoaded)   //do not wait 1 s before triggering the search at start-up
                {
                    if(IsConnectedToInternet)   //no need to wait for search tiping to pause when offline
                    {
                        await Task.Delay(1000, _cts.Token);
                    }
                }

                UpdateUIForLoading();
                var articleList = await _articleService.GetArticlesByQueryParamsAsync(QueryParams, IsConnectedToInternet, _cts.Token);
                ClearAddAndNotify(articleList);
            }
            catch (UnableToGetResponseException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _alertService.ShowAlertAsync("An error occurred.", ex.Message);
                });
            }
            catch (TaskCanceledException) { }
            finally
            {
                AppIsFirstTimeLoaded = false;
                IsBusy = false;
            }
        }

        /// <summary>
        /// This method clears all the previous records showed, adds the news ones and notifies
        /// the view about the change made.
        /// </summary>
        /// <param name="articleList"></param>
        private void ClearAddAndNotify(List<Article> articleList)
        {
            ArticleList.Clear();
            ArticleList.AddRange(articleList);
            DataSetChanged?.Invoke(this, null);
        }

        private void UpdateUIForLoading()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsBusy = true;
                ArticleList.Clear();
                DataSetChanged?.Invoke(this, null);
            });
        }


        /// <summary>
        /// view must call this if it wantd to force a refresh when for example the user swipes to refresh
        /// </summary>
        public void ForceRefresh()
        {
            OnSearchQueryChanged();
        }

        #endregion

        #region BaseViewModel

        protected override void InternalOnConnectivityChanged(bool isConnected)
        {
            base.InternalOnConnectivityChanged(isConnected);
            if(isConnected)
            {
                OnSearchQueryChanged();
            }
        }

        #endregion
    }
}
