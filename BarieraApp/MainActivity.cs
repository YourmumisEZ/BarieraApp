using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Graphics;
using Android.Content.PM;
using Android.Runtime;
using Android;
using System.Linq;

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
        private MainService service;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (grantResults.All(x => (int)x == 0))
            {
                StartService();
            }
        }

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

            service = new MainService();

            startButton.Enabled = !service.CheckIfBarieraServiceIsRunning();
            stopButton.Enabled = service.CheckIfBarieraServiceIsRunning();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void Start_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(MainService.CurrentPhoneNumber))
            {
                CreateSetNumberAlert();
                return;
            }
            var neededPermissions = service.GetNeededPermissions(this.ApplicationContext);

            if (neededPermissions.Length > 0)
            {
                RequestPermissions(neededPermissions, MainService.PermissionCode++);
            }
            else
            {
                StartService();
            }
        }

        private void CreateSetNumberAlert()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(Constants.NotAPhoneNumberTitle);
            alert.SetMessage(Constants.NotAPhoneNumberTitle);
            alert.SetPositiveButton(Constants.NotAPhoneNumberDialogPositiveOption, (senderAlert, args) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void Stop_Click(object sender, System.EventArgs e)
        {
            var isRunning = service.CheckIfBarieraServiceIsRunning();
            StopService(new Intent(this, typeof(BarieraService)));
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private void StartService()
        {
            StartService(new Intent(this, typeof(BarieraService)).SetAction(Constants.ActionStartService));
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void SetPhoneNumber(object sender, System.EventArgs e)
        {
            phoneNumberError.Text = string.Empty;
            if (service.IsValidaPhoneNumber(phoneNumber.Text))
            {
                service.SetPhoneNumber(phoneNumber.Text);
                Toast.MakeText(this, string.Format(Constants.PhoneNumberSetMessage,phoneNumber.Text), ToastLength.Long).Show();
            }
            else
            {
                CreateSetNumberAlert();
            }
        }
    }
}

