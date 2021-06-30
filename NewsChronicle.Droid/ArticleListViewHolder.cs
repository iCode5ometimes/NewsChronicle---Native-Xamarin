using System;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using NewsChronicle.Data.Model;

namespace NewsChronicle.Droid
{
    public class ArticleListViewHolder : RecyclerView.ViewHolder
    {
        #region Properties

        //public View Item { get; private set; }  //actual item clicked in the recycler view
        public TextView ArticleTitle { get; private set; }
        public TextView ArticlePreview { get; private set; }
        public TextView ArticlePublishDate { get; private set; }
        public TextView ArticleSource { get; private set; }
        public ImageView ArticleImage { get; private set; }

        private View _view;

        #endregion

        #region Events

        protected Action<Article> SendArticleClicked;
        public event EventHandler<int> GetArticleClicked;

        #endregion

        #region Constructor(s)

        public ArticleListViewHolder(View view) : base(view)
        {
            ArticleTitle = view.FindViewById<TextView>(Resource.Id.articleTitle);
            ArticlePreview = view.FindViewById<TextView>(Resource.Id.articlePreview);
            ArticlePublishDate = view.FindViewById<TextView>(Resource.Id.articleDate);
            ArticleSource = view.FindViewById<TextView>(Resource.Id.articleSource);
            ArticleImage = view.FindViewById<ImageView>(Resource.Id.articleImage);

            _view = view;
            _view.Click += ViewItem_Clicked;
        }

        #endregion

        #region Methods

        private void ViewItem_Clicked(object sender, EventArgs e)
        {
            if (AdapterPosition != RecyclerView.NoPosition)
            {
                GetArticleClicked?.Invoke(this, AdapterPosition);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
            {
                _view.Click -= ViewItem_Clicked;
            }
        }

        #endregion
    }
}
