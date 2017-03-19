using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Telephony;
using Android.Provider;

namespace BarieraApp
{
    [BroadcastReceiver(Label = "SMS Receiver")]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SmsReceiver : BroadcastReceiver
    {
        public bool openBariera;
        BarieraService service = null;
        public static readonly string IntentAction = "android.provider.Telephony.SMS_RECEIVED";
        public static int Test = 0;
        public override void OnReceive(Context context, Intent intent)
        {
            openBariera = false;
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
                    if (service!=null && sms.DisplayMessageBody.ToLower().Contains("bariera"))
                    {
                        service.CallNumber(string.Format("tel: {0}","0733767442"));
                    }
                }
            }
        }

        public void SetService(BarieraService service)
        {
            this.service = service;
        }
    }
}