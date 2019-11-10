using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using System;

namespace App4
{
    [Activity(Label = "ENTER YOUR PLANS")]
    public class plan_main : Activity
    {
        TextView timeDisplay;
        TextView Datedisplay;
        DateTime DateTime_Date;
        DateTime DateTime_Time;
        int Date_year = new DateTime().Year;
        int Date_month = new DateTime().Month;
        int Date_day = new DateTime().Day;
        int Date_hour = new DateTime().Hour;
        int Date_minute = new DateTime().Minute;
        static readonly int NOTIFICATION_ID = 1000;
        public static readonly string CHANNEL_ID = "location_notification";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.plan_main);

            Datedisplay = FindViewById<TextView>(Resource.Id.Plan_Text_Date);
            timeDisplay = FindViewById<TextView>(Resource.Id.Plan_Text_Time);
            Button button_Date = FindViewById<Button>(Resource.Id.Plan_Button_Date);
            Button button_Time = FindViewById<Button>(Resource.Id.Plan_Button_Time);
            Button button_Enter = FindViewById<Button>(Resource.Id.Plan_Button_Enter);
            EditText editText_Plan = FindViewById<EditText>(Resource.Id.Plan_Edit_Plan);
            EditText editText_Comment = FindViewById<EditText>(Resource.Id.Plan_Edit_Comment);

            button_Date.Click += DateSelectOnClick;
            button_Time.Click += TimeSelectOnClick;

            button_Enter.Click += delegate {
                DateTime dateTime = new DateTime(Date_year, Date_month, Date_day, Date_hour, Date_minute,0);
                DateTime dateDefault = new DateTime();

                if (dateDefault.CompareTo(dateTime) == 0)
                {
                    Toast.MakeText(this,"日時を入力してください", ToastLength.Long).Show();
                }
                else
                {
                    System.Console.WriteLine("あいうえお："+dateTime);
                    System.Console.WriteLine("かきくえこ"+dateDefault);
                    SQlite_main.DoSomeDataAccess(dateTime, editText_Plan.Text, editText_Comment.Text);
                    CreateNotification(dateTime, editText_Plan.Text, editText_Comment.Text);
                    Intent intent = new Intent(this, typeof(App4.MainActivity));
                    StartActivity(intent);
                }
            };

            string a = editText_Plan.Text;
        }

        private void DateSelectOnClick(object sender, EventArgs eventArgs)
        {
            new DatePickerFragment(delegate (DateTime time)
            {
                Datedisplay.Text = time.ToShortDateString();
                //DateTime_Date = time;
                System.Console.WriteLine(time.Month.ToString());
                Date_year = time.Year;
                Date_month = time.Month;
                Date_day = time.Day;
            })

            .Show(FragmentManager, DatePickerFragment.TAG);
        }


        void TimeSelectOnClick(object sender, EventArgs eventArgs)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(
            delegate (DateTime time)
            {
                timeDisplay.Text = time.ToShortTimeString();
                Date_hour = time.Hour;
                Date_minute = time.Minute;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);

        }

        void CreateNotification(DateTime dateTime,String EditText_Plan, String EditText_Comment)
        {

            CreateNotificationChannel();
            var alarmIntent = new Intent(this, typeof(AlarmReceiver));
            alarmIntent.PutExtra("title","予定が近づいています："+ EditText_Plan);
            alarmIntent.PutExtra("message","　"+ EditText_Comment);

            var pending = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            var alarmManager = GetSystemService(AlarmService).JavaCast<AlarmManager>();
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() /*+ GetDateTimeinMillis(dateTime)*/, pending);
            System.Console.WriteLine("あいえう");
        }
        protected long GetDateTimeinMillis(DateTime SelectedDateTime)
        {
            DateTime currentDate = DateTime.Now;
            System.Console.WriteLine(currentDate);
            TimeSpan TimeLag = SelectedDateTime - currentDate;
            double a = TimeLag.TotalMilliseconds;
            return (long)a;
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

    }
}