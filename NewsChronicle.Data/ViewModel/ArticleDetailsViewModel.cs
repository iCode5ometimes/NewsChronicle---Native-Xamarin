using NewsChronicle.Data.Model;

namespace NewsChronicle.Data.ViewModel
{
    public class ArticleDetailsViewModel : BaseViewModel
    {
        #region Fields and Properties

        private Article _article;
        public Article Article
        {
            get => _article;
            set => SetProperty(ref _article, value);
        }

        #endregion

        #region Constructor(s)

        public ArticleDetailsViewModel(Article article)
        {
            _article = article;
        }

        public ArticleDetailsViewModel()
        {
        }

        #endregion
    }
}
