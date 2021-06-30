using System.Threading.Tasks;
using NewsChronicle.Data.Interfaces;
using UIKit;

namespace NewsChronicle.Services
{
    public class AlertService : IAlertService
    {
        private TaskCompletionSource<bool> _taskCompletionSource;

        public Task<bool> ShowAlertAsync(string title, string message)
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();

            var okCancelAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            okCancelAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, alert => {
                _taskCompletionSource.SetResult(true);
            }));

            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;

            viewController.PresentViewController(okCancelAlertController, true, null);
            return _taskCompletionSource.Task;
        }
    }
}
