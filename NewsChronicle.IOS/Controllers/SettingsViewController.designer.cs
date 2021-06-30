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
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		UIKit.UITextField languageOptions { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (languageOptions != null) {
				languageOptions.Dispose ();
				languageOptions = null;
			}
		}
	}
}
