using Android.App;
using Android.Content;
using Android.Telephony;


namespace BarieraApp
{
    [BroadcastReceiver(Label = "SMS Receiver")]
    [IntentFilter(new[] { Constants.AndroidSMSReceived })]
    public class SmsReceiver : BroadcastReceiver
    {
        private MainService service;

        public SmsReceiver()
        {
            service = new MainService();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != Constants.AndroidSMSReceived)
            {
                return;
            }
            if (intent.Extras == null)
            {
                return;
            }
            if (intent.HasExtra("pdus"))
            {
                var smsArray = (Java.Lang.Object[])intent.Extras.Get("pdus");
                foreach (var item in smsArray)
                {
                    var sms = SmsMessage.CreateFromPdu((byte[])item);
                    if (sms.DisplayMessageBody.ToLower().Contains(Constants.Keyword))
                    {
                        if (!string.IsNullOrEmpty(MainService.CurrentPhoneNumber))
                        {
                            CallNumber(context, string.Format("tel: {0}", MainService.CurrentPhoneNumber));
                        }
                    }
                }
            }
        }

        private void CallNumber(Context context, string phoneNumber)
        {
            context.StartActivity(new Intent(Intent.ActionCall, Android.Net.Uri.Parse(phoneNumber)));
        }
    }
}