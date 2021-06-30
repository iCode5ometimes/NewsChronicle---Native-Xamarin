using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace NewsChronicle
{
    public static class IOSUtilities
    {
        public static Task<UIImage> FromUrl(string uri)
        {
            ObjCRuntime.Class.ThrowOnInitFailure = false;
            return Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(uri))
                {
                    using (var url = new NSUrl(uri))
                    using (var data = NSData.FromUrl(url))
                        if (data != null)
                            return UIImage.LoadFromData(data);
                }
                return UIImage.FromBundle("BrokenImageIcon");
            });
        }
    }
}
