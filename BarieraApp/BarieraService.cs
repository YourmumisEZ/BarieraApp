using Android.App;
using Android.Content;
using Android.OS;

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
            RegisterReceiver(receiver, new IntentFilter(Constants.AndroidSMSReceived));


            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
                var mChannel = new NotificationChannel(Constants.BarrierChannelID, Constants.BarrierChannel, NotificationImportance.High);
                mChannel.EnableLights(true);
                mChannel.LightColor = 2;
                mChannel.EnableVibration(true);
                mChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });
                mChannel.SetShowBadge(false);
                notificationManager.CreateNotificationChannel(mChannel);
            }

            var pendingIntent = PendingIntent.GetActivity(
                    this,
                    0,
                    new Intent(this, typeof(MainActivity)),
                    PendingIntentFlags.UpdateCurrent
                );

            var notification = new Notification.Builder(this, Constants.BarrierChannelID)
                               .SetContentTitle(Constants.BarrierAppName)
                               .SetContentText(Constants.BarrierAppNotificationText)
                               .SetSmallIcon(Resource.Drawable.ic_stat_vpn_key)
                               .SetContentIntent(pendingIntent)
                               .AddAction(StopServiceAction())
                                .Build();

            StartForeground(Constants.NotificationID, notification);

            return StartCommandResult.Sticky;
        }

        private Notification.Action StopServiceAction()
        {
            this.StopForeground(true);
            var stopServiceIntent = new Intent(this, GetType());
            stopServiceIntent.SetAction(Constants.ActionStopService);
            var stopServicePendingIntent = PendingIntent.GetService(this, 0, stopServiceIntent, 0);

            var builder = new Notification.Action.Builder(Android.Resource.Drawable.IcMediaPause,
                                                          Constants.StopService,
                                                          stopServicePendingIntent);
            return builder.Build();
        }
    }
}