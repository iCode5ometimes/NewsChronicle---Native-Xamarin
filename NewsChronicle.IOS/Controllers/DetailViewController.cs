// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Threading.Tasks;
using NewsChronicle.Data;
using NewsChronicle.Data.Model;
using NewsChronicle.Data.ViewModel;
using UIKit;

namespace NewsChronicle
{
	public partial class DetailViewController : UIViewController
	{
        #region Fields and Properties

        public Article SelectedArticle
        {
            get => _viewModel.Article;
            set
            {
                _viewModel.Article = value;
            }
        }

        private readonly ArticleDetailsViewModel _viewModel;

        #endregion

        #region Constructor(s)

        public DetailViewController (IntPtr handle) : base(handle)
        {
            _viewModel = ViewModelLocator.Instance.GetInstanceViewModelInstance<ArticleDetailsViewModel>();
        }

        #endregion

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _viewModel.Article.SourceName;
            await OnUpdateDetails();
        }

        #region UIMethods

        private async Task OnUpdateDetails()
        {
            var image = await IOSUtilities.FromUrl(_viewModel.Article.UrlToArticleImage);
            articleImage.Image = image;
            articleTitle.Text = _viewModel.Article.Title;
            articlePublishDate.Text = _viewModel.Article.PublishedAt;
            articleContent.Text = _viewModel.Article.Content;
        }

        #endregion
    }
}
