using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;

namespace NewsChronicle.Droid.Activities
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/app_icon64dp", Theme ="@style/AppTheme.Splash", MainLauncher =true, NoHistory =true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        #region AppCompatActivity

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ShowMainActivity();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        public override void OnBackPressed()
        {
            //leaving this blank so pressing the back button while in the splash
            //screen does not close the application
        }

        #endregion

        #region Methods

        private void ShowMainActivity()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        #endregion
    }
}
