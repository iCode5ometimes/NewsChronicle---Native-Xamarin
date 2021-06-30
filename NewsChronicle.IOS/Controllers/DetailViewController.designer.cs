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
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		UIKit.UILabel articleContent { get; set; }

		[Outlet]
		UIKit.UIImageView articleImage { get; set; }

		[Outlet]
		UIKit.UILabel articlePublishDate { get; set; }

		[Outlet]
		UIKit.UILabel articleTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (articleImage != null) {
				articleImage.Dispose ();
				articleImage = null;
			}

			if (articleTitle != null) {
				articleTitle.Dispose ();
				articleTitle = null;
			}

			if (articlePublishDate != null) {
				articlePublishDate.Dispose ();
				articlePublishDate = null;
			}

			if (articleContent != null) {
				articleContent.Dispose ();
				articleContent = null;
			}
		}
	}
}
