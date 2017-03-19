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

namespace BarieraApp
{
    [Service]
    public class BarieraService : Service
    {

        private SmsReceiver _receiver = new SmsReceiver();
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(_receiver);
        }

        public void CallNumber(string phoneNumber)
        {
            var uri = Android.Net.Uri.Parse(phoneNumber);
            var callIntent = new Intent(Intent.ActionCall, uri);
            StartActivity(callIntent);
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _receiver.SetService(this);
            RegisterReceiver(_receiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));

            return StartCommandResult.Sticky;
        }
    }
}