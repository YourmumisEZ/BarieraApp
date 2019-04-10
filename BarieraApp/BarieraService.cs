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
            receiver = new SmsReceiver();
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

            var notificationID = "my_channel_01";

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
                var mChannel = new NotificationChannel(notificationID, "my_channel", NotificationImportance.High);
                mChannel.Description = "This is my channel";
                mChannel.EnableLights(true);
                mChannel.LightColor = 2;
                mChannel.EnableVibration(true);
                mChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });
                mChannel.SetShowBadge(false);
                notificationManager.CreateNotificationChannel(mChannel);
            }

            var pendingIntent = PendingIntent.GetActivity(
                    ApplicationContext,
                    0,
                    new Intent(this, typeof(MainActivity)),
                    PendingIntentFlags.UpdateCurrent
                );

            var notification = new Notification.Builder(this, notificationID)
                               .SetContentTitle("BarrierApp")
                               .SetContentText("text")
                               .SetSmallIcon(Resource.Drawable.ic_stat_vpn_key)
                               .SetContentIntent(pendingIntent)
                               .SetPriority((int)NotificationPriority.High)
                               //.SetOngoing(true)
                               // .AddAction(new Notification.Action()
                               // .AddAction()
                                .Build();

            StartForeground(10000, notification);

            return StartCommandResult.Sticky;
        }
    }
}