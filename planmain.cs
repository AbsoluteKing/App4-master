using Android.App;
using Android.OS;
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
            button_Enter.Click += delegate { SQlite_main.DoSomeDataAccess(Datedisplay.Text, timeDisplay.Text, editText_Plan.Text, editText_Comment.Text); };
            string a = editText_Plan.Text;
        }

        private void DateSelectOnClick(object sender, EventArgs eventArgs)
        {
            new DatePickerFragment(delegate (DateTime time)
            {
                Datedisplay.Text = time.ToShortDateString();
            })

            .Show(FragmentManager, DatePickerFragment.TAG);
        }


        void TimeSelectOnClick(object sender, EventArgs eventArgs)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(
            delegate (DateTime time)
            {
                timeDisplay.Text = time.ToShortTimeString();
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);

        }
    }
}