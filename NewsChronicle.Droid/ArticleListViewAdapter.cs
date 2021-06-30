using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using NewsChronicle.Data.Model;
using Square.Picasso;

namespace NewsChronicle.Droid
{
    public class ArticleListViewAdapter : RecyclerView.Adapter
    {
        #region Fields and Properties

        private List<Article> _articleList;

        public override int ItemCount => _articleList.Count;

        #endregion

        #region Event(s)

        public event EventHandler<Article> ArticleClicked;    //view subscribes to this and creates an intent to navigate to
                                                              //the detail page

        #endregion

        #region Constructor(s)

        public ArticleListViewAdapter(List<Article> data)
        {
            _articleList = data;
        }

        #endregion

        #region RecyclerView.Adapter

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ArticleListViewHolder viewHolder = holder as ArticleListViewHolder;
            viewHolder.ArticleTitle.Text = _articleList[position].Title;
            viewHolder.ArticlePreview.Text = _articleList[position].Description;
            viewHolder.ArticlePublishDate.Text = _articleList[position].PublishedAt;
            viewHolder.ArticleSource.Text = _articleList[position].SourceName;
            if(!string.IsNullOrWhiteSpace(_articleList[position].UrlToArticleImage))
            {
                Picasso.Get().
                        Load(_articleList[position].UrlToArticleImage).
                        Placeholder(Resource.Drawable.placeholder_image_32dp).
                        Error(Resource.Drawable.broken_image_32dp).
                        Into(viewHolder.ArticleImage);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);
            var view = inflater.Inflate(resource: Resource.Layout.article_row, parent, false);

            var holder = new ArticleListViewHolder(view);

            holder.GetArticleClicked += Holder_GetArticleClicked;

            return holder;
        }

        #endregion

        #region Methods

        private void Holder_GetArticleClicked(object sender, int e)
        {
            ArticleClicked?.Invoke(this, _articleList[e]);
        }

        #endregion
    }
}
