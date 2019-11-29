using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using SQLite;
using System;
using System.IO;

namespace App4
{
    [Activity(Label = "ENTER YOUR PLANS")]
    public class plan_main : Activity
    {
        TextView timeDisplay;
        TextView Datedisplay;
        int Date_year = new DateTime().Year;
        int Date_month = new DateTime().Month;
        int Date_day = new DateTime().Day;
        int Date_hour = new DateTime().Hour;
        int Date_minute = new DateTime().Minute;
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
                    DoSomeDataAccess(dateTime, editText_Plan.Text, editText_Comment.Text);
                    CreateNotification(dateTime, editText_Plan.Text, editText_Comment.Text);
                    Intent intent = new Intent(this, typeof(App4.MainActivity));
                    SortCard();
                    StartActivity(intent);
                }
            };
        }

        public void DoSomeDataAccess(DateTime SelectedDateTime, String SelectedPlan, String SelectedComment)
        {
            string dbPath = Path.Combine(
                            Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Stock>();

            db.Insert(new Stock()
            {
                dateTime = SelectedDateTime,
                Plan = SelectedPlan,
                Comment = SelectedComment
            });

            var table = db.Table<Stock>();
            foreach (var s in table)
            {
                System.Console.WriteLine(s.Id + " " + s.Plan);
            }
        }

        public void SortCard()
        {
    //        const string permission = Manifest.Permission.WriteExternalStorage;
    //        if (CheckSelfPermission(permission) == Permission.Denied)
    //        {
    //            ActivityCompat.RequestPermissions(this, new[]
    //{
    //            Manifest.Permission.WriteExternalStorage, Manifest.Permission.Camera
    //        }, 0);
    //        }
            string dbPath = Path.Combine(
                            Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
            SQLiteConnection db = new SQLiteConnection(dbPath);
            var table_sorted = db.Query<Stock>("SELECT * FROM Items ORDER BY dateTime ASC");

            foreach (var s in table_sorted)
            {
                System.Console.WriteLine("ソート後：" + s.Id + "  " + s.dateTime);
            }
        }

        private void DateSelectOnClick(object sender, EventArgs eventArgs)
        {
            new DatePickerFragment(delegate (DateTime time)
            {
                Datedisplay.Text = time.ToShortDateString();
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