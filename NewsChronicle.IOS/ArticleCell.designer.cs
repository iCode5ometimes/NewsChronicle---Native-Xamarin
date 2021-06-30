// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace NewsChronicle
{
	[Register ("ArticleCell")]
	partial class ArticleCell
	{
		[Outlet]
		UIKit.UILabel articleContentPreview { get; set; }

		[Outlet]
		UIKit.UILabel articleDate { get; set; }

		[Outlet]
		UIKit.UIImageView articleImage { get; set; }

		[Outlet]
		UIKit.UILabel articleSource { get; set; }

		[Outlet]
		UIKit.UILabel articleTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (articleContentPreview != null) {
				articleContentPreview.Dispose ();
				articleContentPreview = null;
			}

			if (articleImage != null) {
				articleImage.Dispose ();
				articleImage = null;
			}

			if (articleTitle != null) {
				articleTitle.Dispose ();
				articleTitle = null;
			}

			if (articleDate != null) {
				articleDate.Dispose ();
				articleDate = null;
			}

			if (articleSource != null) {
				articleSource.Dispose ();
				articleSource = null;
			}
		}
	}
}
