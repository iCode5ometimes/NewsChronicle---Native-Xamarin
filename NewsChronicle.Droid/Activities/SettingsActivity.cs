using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using NewsChronicle.Data;
using NewsChronicle.Data.ViewModel;

namespace NewsChronicle.Droid
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : AppCompatActivity , AdapterView.IOnItemSelectedListener
    {
        #region Fields

        private AppSettingsViewModel _viewModel;

        private AndroidX.AppCompat.Widget.Toolbar _toolbar;

        private Spinner _languageSpinner;
        private string[] _languagesAvailable;

        private bool _isFirstLaunched = true;

        #endregion

        #region AppCompatActivity

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.article_app_setting_page);

            _viewModel = ViewModelLocator.Instance.GetInstanceViewModelInstance<AppSettingsViewModel>();

            _toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.detailPageToolbar);
            _toolbar.Title = "Article language";
            SetSupportActionBar(_toolbar);

            _languageSpinner = FindViewById<Spinner>(Resource.Id.languageSpinner);
            _languagesAvailable = _viewModel.OptionDict.Keys.ToArray();
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, _languagesAvailable);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _languageSpinner.Adapter = adapter;
            _languageSpinner.SetSelection(GetCurrentAppArticleLanguageIndex());
            _languageSpinner.OnItemSelectedListener = this;

        }

        #endregion

        #region AdapterView.IOnItemSelectedListener

        public void OnItemSelected(AdapterView parent, View view, int position, long id)
        {
            if(!_isFirstLaunched)
            {
                _viewModel.SetAppArticleLanguage(_viewModel.OptionDict[_languagesAvailable[position]]);
            }
            _isFirstLaunched = false;
        }

        public void OnNothingSelected(AdapterView parent)
        {
            //TODO Auto-generated method stub
        }

        #endregion

        #region DrawerLayout

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home) //back button in this case
            {
                Finish();
            }
            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the index of the current app article language from the adapter of the spinner
        /// and returns it so that the spinner default selected value corresponds with the app
        /// article language setting
        /// </summary>
        /// <returns></returns>
        private int GetCurrentAppArticleLanguageIndex()
        {
            var currLangIdentifier = _viewModel.AppArticleLanguage;
            var currLanguage = _viewModel.OptionDict.FirstOrDefault(value => value.Value == currLangIdentifier).Key;
            return Array.FindIndex(_languagesAvailable, index => index == currLanguage);
        }

        #endregion
    }
}
