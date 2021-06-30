using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using NewsChronicle.Data.ViewModel;
using NewsChronicle.Data;
using AndroidX.RecyclerView.Widget;
using NewsChronicle.Data.QueryParameters;
using NewsChronicle.Droid.Services;
using NewsChronicle.Data.Interfaces;
using Android.Widget;
using Android.Views;
using Xamarin.Essentials;
using Android.Runtime;
using Google.Android.Material.Navigation;
using NewsChronicle.Data.Constants;
using NewsChronicle.Data.Model;
using Android.Content;
using Newtonsoft.Json;
using AndroidX.SwipeRefreshLayout.Widget;
using NewsChronicle.Data.Enum;

namespace NewsChronicle.Droid
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        #region Fields

        private ArticleListViewModel _viewModel;

        private ImageButton _btnSettings;
        private RecyclerView _recycler;
        private ArticleListViewAdapter _adapter;
        private SearchView _searchView;
        private NavigationView _navigationView;
        private AndroidX.AppCompat.Widget.Toolbar _toolbar;
        private AndroidX.DrawerLayout.Widget.DrawerLayout _drawerLayout;
        private SwipeRefreshLayout _refreshLayout;

        #endregion

        #region AppCompatActivity

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.article_list_main);
            FindAllViews();

            ViewModelLocator.Instance.RegisterSingleton<IAlertService, AlertService>();
            ViewModelLocator.Instance.RegisterSingleton<IDBFileAccessHelper, DBFileAccessHelper>();
            _viewModel = ViewModelLocator.Instance.GetInstanceViewModelInstance<ArticleListViewModel>();

            SetSupportActionBar(_toolbar);

            _navigationView.SetCheckedItem(Resource.Id.all_top_headlines);

            var elem = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            _recycler.SetLayoutManager(elem);
            _adapter = new ArticleListViewAdapter(_viewModel.ArticleList);
            _recycler.SetAdapter(_adapter);
        }

        private void OnArticleClicked(object sender, Article e)
        {
            var intent = new Intent(this, typeof(DetailActivity));
            var serializedArticle = JsonConvert.SerializeObject(e);
            intent.PutExtra(Constants.ArticleSelectedIntentName, serializedArticle);
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();

            _searchView.ClearFocus();

            SubscribeToEvents();

            _viewModel.OnAppearing();

            MyQueryParams newQueryParams;

            if(_viewModel.AppIsFirstTimeLoaded)
            {
                _viewModel.QueryParams = new MyQueryParams
                {
                    Language = _viewModel.AppArticleLanguage
                };
            }
            else
            {
                newQueryParams = MyQueryParams.GetCopy(_viewModel.QueryParams);
                if (newQueryParams.Language != _viewModel.AppArticleLanguage)
                {
                    newQueryParams.Language = _viewModel.AppArticleLanguage;
                    _viewModel.QueryParams = newQueryParams;
                }
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnsubscribeToEvents();
            _viewModel.OnDisappearing();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _recycler.SetAdapter(null);
        }

        #endregion

        #region SearchView

        private void SearchView_TextChanged(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            FilterContentForSearchText(e.NewText);
        }

        private void FilterContentForSearchText(string text)
        {
            var newQueryParams = MyQueryParams.GetCopy(_viewModel.QueryParams);
            newQueryParams.KeywordsOrPhrases = text;
            if (newQueryParams.ApiCallType == ArticleApiCall.AllArticles)
            {
                if (string.IsNullOrWhiteSpace(text)) //when looking for all articles, search word cannot be empty or null
                {
                    newQueryParams.KeywordsOrPhrases = Constants.KeywordWildCardForAllArticlesSearch;
                }
            }
            _viewModel.QueryParams = newQueryParams;
        }

        #endregion

        #region DrawerLayout

        private void SettingsButton_Clicked(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }

        private void DrawerArticleSearchOptionChanged(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var newQueryParams = new MyQueryParams();
            newQueryParams.Language = _viewModel.QueryParams.Language;

            switch (e.MenuItem.ItemId)
            {
                //top headlines
                case Resource.Id.all_top_headlines:
                    {
                        newQueryParams.Category = " ";
                        break;
                    }
                case Resource.Id.health_top_headlines:
                    {
                        newQueryParams.Category = ArticleSearchCategory.Health.ToString().ToLower();
                        break;
                    }
                case Resource.Id.science_top_headlines:
                    {
                        newQueryParams.Category = ArticleSearchCategory.Science.ToString().ToLower();
                        break;
                    }
                case Resource.Id.sports_top_headlines:
                    {
                        newQueryParams.Category = ArticleSearchCategory.Sports.ToString().ToLower();
                        break;
                    }
                case Resource.Id.technology_top_headlines:
                    {
                        newQueryParams.Category = ArticleSearchCategory.Technology.ToString().ToLower();
                        break;
                    }

                //all articles
                case Resource.Id.newest_all_articles:
                    {
                        newQueryParams.SortBy = ArticleSearchSortBy.Newest.ToString().ToLower();
                        break;
                    }
                case Resource.Id.relevancy_all_articles:
                    {
                        newQueryParams.SortBy = ArticleSearchSortBy.Relevancy.ToString().ToLower();
                        break;
                    }
                case Resource.Id.popularity_all_articles:
                    {
                        newQueryParams.SortBy = ArticleSearchSortBy.Popularity.ToString().ToLower();
                        break;
                    }
            }

            //the all articles api call does not accept category as query word
            //it also needs something to look for, so I use the wildcard keyword
            if (string.IsNullOrEmpty(newQueryParams.Category))
            {
                newQueryParams.ApiCallType = ArticleApiCall.AllArticles;
                newQueryParams.KeywordsOrPhrases = Constants.KeywordWildCardForAllArticlesSearch;
            }

            ClearSearchView();

            _viewModel.QueryParams = newQueryParams;
            _drawerLayout.Close();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home) // the burger menu was clicked
            {
                _drawerLayout.Open();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        #endregion

        #region UIMethods

        /// <summary>
        ///when the category has changed, make search bar text empty and
        ///clear search view focus
        /// </summary>
        private void ClearSearchView()
        {
            _searchView.ClearFocus();
            _searchView.SetQuery(string.Empty, false);
        }

        private void FindAllViews()
        {
            _refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.pullToRefresh);
            _searchView = FindViewById<SearchView>(Resource.Id.searchBar);
            _toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.appToolBar);
            _navigationView = FindViewById<NavigationView>(Resource.Id.navigationView);
            _drawerLayout = FindViewById<AndroidX.DrawerLayout.Widget.DrawerLayout>(Resource.Id.drawerLayout);
            _recycler = FindViewById<RecyclerView>(Resource.Id.lstArticles);

            var navigationViewHeader = _navigationView.GetHeaderView(0);
            _btnSettings = navigationViewHeader.FindViewById<ImageButton>(Resource.Id.BtnSettings);
        }

        private void ShowLoadingSpinner(bool isShowing)
        {
            if (isShowing)
            {
                _recycler.Visibility = ViewStates.Gone;
            }
            else
            {
                _recycler.Visibility = ViewStates.Visible;
            }
            if(_viewModel.IsConnectedToInternet)   //no internet then no need for this
            {
                _refreshLayout.Refreshing = isShowing;
            }
        }

        #endregion

        #region Events

        private void SubscribeToEvents()
        {
            _viewModel.PropertyChanged += ViewModel_PropertiesChanged;
            _refreshLayout.Refresh += UserSwipedToRefresh;
            _viewModel.DataSetChanged += ViewModelDataSet_Changed;
            _navigationView.NavigationItemSelected += DrawerArticleSearchOptionChanged;
            _searchView.QueryTextChange += SearchView_TextChanged;
            _btnSettings.Click += SettingsButton_Clicked;
            _adapter.ArticleClicked += OnArticleClicked;
        }

        private void UnsubscribeToEvents()
        {
            _navigationView.NavigationItemSelected -= DrawerArticleSearchOptionChanged;
            _refreshLayout.Refresh -= UserSwipedToRefresh;
            _viewModel.DataSetChanged -= ViewModelDataSet_Changed;
            _searchView.QueryTextChange -= SearchView_TextChanged;
            _viewModel.PropertyChanged -= ViewModel_PropertiesChanged;
            _adapter.ArticleClicked -= OnArticleClicked;
            _btnSettings.Click -= SettingsButton_Clicked;
        }

        private void ViewModelDataSet_Changed(object sender, System.EventArgs e)
        {
            _adapter.NotifyDataSetChanged();
        }

        private void UserSwipedToRefresh(object sender, System.EventArgs e)
        {
            if(_viewModel.IsConnectedToInternet)   //only show loading bar when connected to internet
            {
                _viewModel.ForceRefresh();
            }
            else
            {
                Toast.MakeText(this, "You are in offline mode", ToastLength.Long).Show();
                _refreshLayout.Refreshing = false;
            }
        }

        #endregion

        #region INotifyPropertyChanged

        private void ViewModel_PropertiesChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ArticleListViewModel.IsBusy):
                    {
                        ShowLoadingSpinner(_viewModel.IsBusy);
                        break;
                    }
                case nameof(ArticleListViewModel.IsConnectedToInternet):
                    {
                        if(_viewModel.IsConnectedToInternet)
                        {
                            Toast.MakeText(this, "You are online", ToastLength.Long).Show();
                            ClearSearchView();
                        }
                        else
                        {
                            Toast.MakeText(this, "You are offline", ToastLength.Long).Show();
                        }
                        break;
                    }
            }
        }

        #endregion

        #region Xamarin.Essentials

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion
    }
}
