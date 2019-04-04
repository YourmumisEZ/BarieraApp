using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Graphics;

namespace BarieraApp
{
    [Activity(Label = "BarieraApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button startButton;
        private Button stopButton;
        private Button setPhoneNumberButton;
        private EditText phoneNumber;
        private TextView phoneNumberError;
        //private SmsReceiver receiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            startButton = FindViewById<Button>(Resource.Id.button1);
            stopButton = FindViewById<Button>(Resource.Id.button2);
            setPhoneNumberButton = FindViewById<Button>(Resource.Id.button3);
            phoneNumber = (EditText)FindViewById(Resource.Id.phoneNr);
            phoneNumberError = (TextView)FindViewById(Resource.Id.phoneNumberError);
            phoneNumberError.SetTextColor(Color.Red);

            startButton.Click += Start_Click;
            stopButton.Click += Stop_Click;
            setPhoneNumberButton.Click += SetPhoneNumber;

            var isRunning = Operations.CheckIfBarieraServiceIsRunning();
            startButton.Enabled = !isRunning;
            stopButton.Enabled = isRunning;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //receiver.Dispose();
        }

        private void Start_Click(object sender, System.EventArgs e)
        {
            //receiver = new SmsReceiver();
            //RegisterReceiver(receiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
            StartService(new Intent(this, typeof(BarieraService)));
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }


        private void Stop_Click(object sender, System.EventArgs e)
        {
            var isRunning = Operations.CheckIfBarieraServiceIsRunning();
            StopService(new Intent(this, typeof(BarieraService)));
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private void SetPhoneNumber(object sender, System.EventArgs e)
        {
            phoneNumberError.Text = string.Empty;
            if (Operations.IsValidaPhoneNumber(phoneNumber.Text))
            {
                Operations.SetPhoneNumber(phoneNumber.Text);
                SetErrorToFalse();
            }
            else
            {
                SetErrorToTrue();
            }
        }

        private void SetErrorToTrue()
        {
            phoneNumberError.SetTextColor(Color.Red);
            phoneNumberError.Text = "Not a phone number";
        }

        private void SetErrorToFalse()
        {
            phoneNumberError.SetTextColor(Color.Green);
            phoneNumberError.Text = "Number Saved";
        }
    }
}

