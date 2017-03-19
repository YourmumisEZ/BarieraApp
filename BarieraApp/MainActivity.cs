using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Java.Lang;

namespace BarieraApp
{
    [Activity(Label = "BarieraApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button startButton;
        Button stopButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            startButton = FindViewById<Button>(Resource.Id.button1);
            startButton.Click += Start_Click;
            stopButton = FindViewById<Button>(Resource.Id.button2);
            stopButton.Click += Stop_Click;
            stopButton.Enabled = false;
        }
        void Start_Click(object sender, System.EventArgs e)
        {
            StartService(new Intent(this, typeof(BarieraService)));
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }


        void Stop_Click(object sender, System.EventArgs e)
        {
            StopService(new Intent(this, typeof(BarieraService)));
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }
    }
}

