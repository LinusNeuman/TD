using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

using Android.Media;

using Android.Gms;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Games;
using Android.Content;
using Android.Runtime;

namespace TD
{
    [Activity(Label = "TD"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleTask // Or SingleInstance - Originally
        , ScreenOrientation = ScreenOrientation.SensorLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 
        : Microsoft.Xna.Framework.AndroidGameActivity
        , GoogleApiClient.IConnectionCallbacks
        , GoogleApiClient.IOnConnectionFailedListener
        , AudioManager.IOnAudioFocusChangeListener
    {

        const int ConnectionFailureResolutionRequest = 9000;

        GoogleApiClient client;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if(CheckGooglePlayServices())
            {
                client = CreateApiClient();
            }

            var g = new Game1(this);
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (client != null)
            {
                client.Connect();
            }
        }

        protected override void OnStop()
        {
            if (client != null)
            {
                client.Disconnect();
            }
            base.OnStop();
        }

        public void OnConnected (Bundle connectionHint)
        {
            System.Console.WriteLine("Google API is Connected.");
        }

        public void OnConnectionSuspended (int cause)
        {
            System.Console.WriteLine("Google API is Suspended.");
        }

        public void OnConnectionFailed (ConnectionResult result)
        {
            System.Console.WriteLine("Google API failed.");
            if(result.HasResolution)
            {
                try
                {
                    result.StartResolutionForResult(this, ConnectionFailureResolutionRequest);
                }
                catch (IntentSender.SendIntentException ex)
                {
                    System.Console.WriteLine("Google API Failed: " + ex.LocalizedMessage);
                }
            }
            else
            {
                // show error or something
            }
        }

        public void OnAudioFocusChange (AudioFocus af)
        {

        }

        //class ErrorDialogFragment : Android.Support.V4.App.DialogFragment
        //{
        //    public new Dialog Dialog
        //    {
        //        get;
        //        set;
        //    }

        //    public override Dialog OnCreateDialog(Bundle savedInstanceState)
        //    {
        //        return Dialog;
        //    }
        //}

        GoogleApiClient CreateApiClient()
        {
            return new GoogleApiClient.Builder(this)
                .AddApi(GamesClass.API)
                .AddScope(GamesClass.ScopeGames)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .SetGravityForPopups((int)(GravityFlags.Bottom | GravityFlags.CenterHorizontal))
                .Build();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if(requestCode == ConnectionFailureResolutionRequest)
            {
                if(resultCode == Result.Ok && CheckGooglePlayServices())
                {
                    if(client == null)
                    {
                        client = CreateApiClient();
                        client.Connect();
                    }
                }
                else
                {
                    //Finish();
                }
            }
            else
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }

        bool CheckGooglePlayServices()
        {
            var result = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
            if(result == ConnectionResult.Success)
            {
                System.Console.WriteLine("Connection Success.");
                return true;
            }
            var dialog = GooglePlayServicesUtil.GetErrorDialog(result, this, ConnectionFailureResolutionRequest);
            if(dialog != null)
            {
                System.Console.WriteLine("Connection Failed.");
                //var errorDialog = new ErrorDialogFragment { Dialog = dialog };
                //errorDialog.Show(getFragmentManager(), "Google Services Updates");
                return false;
            }

            //Finish();
            return false;
        }
    }
}

