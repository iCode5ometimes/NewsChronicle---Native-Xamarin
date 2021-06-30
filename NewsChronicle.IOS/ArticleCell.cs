// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace NewsChronicle
{
	public partial class ArticleCell : UITableViewCell
	{
		public UIImageView ArticleImage
        {
			get => articleImage;
        }

		public UILabel ArticleTitle
        {
			get => articleTitle;
        }

		public UILabel ArticleContentPreview
        {
			get => articleContentPreview;
        }

		public UILabel ArticlePublishDate
        {
			get => articleDate;
        }

		public UILabel ArticleSourceName
        {
			get => articleSource;
        }

		public ArticleCell (IntPtr handle) : base (handle)
		{
		}
	}
}