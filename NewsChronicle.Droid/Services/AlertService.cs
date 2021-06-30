using System.Threading.Tasks;
using Android.App;
using Android.Content;
using NewsChronicle.Data.Interfaces;

namespace NewsChronicle.Droid.Services
{
    public class AlertService : IAlertService
    {
        private TaskCompletionSource<bool> _taskCompletionSource;

        public AlertService()
        {

        }

        public Task<bool> ShowAlertAsync(string title, string message)
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();

            AlertDialog.Builder dialog = new AlertDialog.Builder(Xamarin.Essentials.Platform.CurrentActivity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("Ok", (c, ev) =>
            {
                _taskCompletionSource.SetResult(true);
            });

            alert.Show();

            return _taskCompletionSource.Task;
        }
    }
}
