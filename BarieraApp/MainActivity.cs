using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace BarieraApp
{
    [Activity(Label = "BarieraApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button startButton;
        Button stopButton;
        Button setPhoneNumberButton;
        EditText phoneNumber;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            startButton = FindViewById<Button>(Resource.Id.button1);
            startButton.Click += Start_Click;
            stopButton = FindViewById<Button>(Resource.Id.button2);
            stopButton.Click += Stop_Click;
            setPhoneNumberButton = FindViewById<Button>(Resource.Id.button3);
            setPhoneNumberButton.Click += SetPhoneNumber;
            phoneNumber = (EditText)FindViewById(Resource.Id.phoneNr);

            var isRunning = Operations.CheckIfBarieraServiceIsRunning();
            startButton.Enabled = !isRunning;
            stopButton.Enabled = isRunning;
        }
        void Start_Click(object sender, System.EventArgs e)
        {
            StartService(new Intent(this, typeof(BarieraService)));
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }


        void Stop_Click(object sender, System.EventArgs e)
        {
            var isRunning = Operations.CheckIfBarieraServiceIsRunning();
            StopService(new Intent(this, typeof(BarieraService)));
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        void SetPhoneNumber(object sender, System.EventArgs e)
        {
            Operations.SetPhoneNumber(phoneNumber.Text);
        }
    }
}

