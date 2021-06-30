using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using NewsChronicle.Data;
using NewsChronicle.Data.Constants;
using NewsChronicle.Data.Model;
using NewsChronicle.Data.ViewModel;
using Newtonsoft.Json;
using Square.Picasso;

namespace NewsChronicle.Droid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : AppCompatActivity
    {
        #region Fields

        private ArticleDetailsViewModel _viewModel;

        private AndroidX.AppCompat.Widget.Toolbar _toolbar;
        private ImageView _articleImage;
        private TextView _articleTitle;
        private TextView _articlePublishDate;
        private TextView _articleContent;

        #endregion

        #region AppCompatActivity

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.article_detail_page);

            _viewModel = ViewModelLocator.Instance.GetInstanceViewModelInstance<ArticleDetailsViewModel>();
            //DeserializeAndAssignIntentArticle();

            _toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.detailPageToolbar);
            _toolbar.Title = _viewModel.Article.SourceName;
            SetSupportActionBar(_toolbar);

            _articleImage = FindViewById<ImageView>(Resource.Id.articleImage);
            _articleTitle = FindViewById<TextView>(Resource.Id.articleTitle);
            _articlePublishDate = FindViewById<TextView>(Resource.Id.articlePublishDate);
            _articleContent = FindViewById<TextView>(Resource.Id.articleContent);

            DeserializeAndAssignIntentArticle();
            AssignSelectedArticleDataToView();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if(item.ItemId == Android.Resource.Id.Home) //back button in this case
            {
                Finish();
            }
            return true;
        }

        #endregion

        #region Methods

        private void DeserializeAndAssignIntentArticle()
        {
            var intentExtra = Intent.GetStringExtra(Constants.ArticleSelectedIntentName);
            var deserializedArticle = JsonConvert.DeserializeObject<Article>(intentExtra);
            _viewModel.Article = deserializedArticle;
        }

        private void AssignSelectedArticleDataToView()
        {
            if(!string.IsNullOrWhiteSpace(_viewModel.Article.UrlToArticleImage))
            {
                Picasso.Get().
                        Load(_viewModel.Article.UrlToArticleImage).
                        Placeholder(Resource.Drawable.placeholder_image_32dp).
                        Error(Resource.Drawable.broken_image_32dp).
                        Into(_articleImage);
            }
            _articleTitle.Text = _viewModel.Article.Title;
            _articlePublishDate.Text = _viewModel.Article.PublishedAt;
            _articleContent.Text = _viewModel.Article.Content;
        }

        #endregion
    }
}
