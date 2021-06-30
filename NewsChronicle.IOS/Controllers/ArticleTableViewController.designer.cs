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
	[Register ("ArticleTableViewController")]
	partial class ArticleTableViewController
	{
		[Outlet]
		UIKit.UIImageView articleCountryImage { get; set; }

		[Outlet]
		UIKit.UISegmentedControl articlePriorityFilter { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (articlePriorityFilter != null) {
				articlePriorityFilter.Dispose ();
				articlePriorityFilter = null;
			}

			if (articleCountryImage != null) {
				articleCountryImage.Dispose ();
				articleCountryImage = null;
			}
		}
	}
}
