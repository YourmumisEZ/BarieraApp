using Android.App;
using Android.Content;
using Android.Telephony;


namespace BarieraApp
{
    [BroadcastReceiver(Label = "SMS Receiver")]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SmsReceiver : BroadcastReceiver
    {
        public static readonly string IntentAction = "android.provider.Telephony.SMS_RECEIVED";

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != IntentAction)
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
                    if (sms.DisplayMessageBody.ToLower().Contains("bariera"))
                    {
                        if (!string.IsNullOrEmpty(Operations.CurrentPhoneNumber))
                        {
                            CallNumber(context, string.Format("tel: {0}", Operations.CurrentPhoneNumber));
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