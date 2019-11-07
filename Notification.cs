using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Net;
using Android.OS;
using Android.Support.V4.App;
using System;
using App4;
using Android.Content.Res;

namespace App4
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
      



            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);
            NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

            int resourceId = Resource.Drawable.Icon;

            Android.Net.Uri uri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            
            var builder = new NotificationCompat.Builder(context, MainActivity.CHANNEL_ID)
                            .SetContentIntent(contentIntent)
                            .SetSmallIcon(Resource.Drawable.Icon)
                            .SetContentTitle("aiueo")
                            .SetContentText("kakikukeko")
                            .SetStyle(style)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                            .SetSound(uri)
                            .SetColor(ActivityCompat.GetColor(context, Resource.Color.colorPrimaryDark));

            var notification = builder.Build();

            manager.Notify(0, notification);
            Console.WriteLine("かきくえこ");

        }

    }
}