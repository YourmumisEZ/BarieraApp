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
        private SmsReceiver receiver;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            receiver= new SmsReceiver();
            base.OnCreate();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            receiver.Dispose();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            RegisterReceiver(receiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
            return StartCommandResult.Sticky;
        }
    }
}