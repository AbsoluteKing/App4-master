using System;
using System.Collections.Generic;
//using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Text.Format;
using Android.Util;
using Android.Views;
using Android.Widget;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

namespace App4
{


    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
        public static File plans;
        public static File plans_dir;
    }

    //（派生クラス名）：基底クラス名
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        TextView timeDisplay;
        TextView Datedisplay;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //OnCreate→Activityの初期化

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            LinearLayout cardLinear = (LinearLayout)FindViewById(Resource.Id.cardLinear);
            cardLinear.RemoveAllViews();



            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //あとまわし

           // timeDisplay = FindViewById<TextView>(Resource.Id.time_display);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
            //FloatingActionButtonはapp_bar_mainで定義した右下に出てくるボタン　それを変数fabで宣言
            //ボタンの取得　FindViewById<ボタンの種類>(Resource.Id.ボタン変数)

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();
            //レイアウトの状態を取得
            //変数toggleをActionBarDrawerToggleのインスタンスとして生成
            //ここでいうthisはインスタンスのメンバ変数の宣言に使う予約語とは別
            //ActionBarDrawerToggle(HostActivity,DrawerLayoutObject,ToolbarLayoutObject,「ドロワーを開く」操作を示す文字列リソース,「ドロワーを閉じる」操作を示す文字列リソース)
            //メニューアイコンを生成（≡のやつ）
            //toggle.SyncState()によって、画面を横向きにしたとき回転しながらアイコンが遷移するようになる

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            //NavigationViewによって、ナビゲーションドロワーを実装できる。


            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
            }

        }

        public override void OnBackPressed()
        {
            //androidの戻るボタンが押された時の操作を記述

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //Drawerを実装

            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
            //左側のドロワーが開いていた時閉じる
            //そうでないとき、基底のOnbackpressedにアクセス（android端末を1つ前の状態に戻す）

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
            //オプションボタンを追加 manu_main.xmlに記述してある
            //処理を続行するならfalse
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //オプションの項目の動作を記述

            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {

            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                TakeAPicture(item);
            }
            else if (id == Resource.Id.nav_gallery)
            {
                TimeSelectOnClick(item);
            }
            else if (id == Resource.Id.nav_slideshow)
            {
                Intent intent = new Intent(this, typeof(RecyclerViewer.RecyclerMain));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_manage)
            {
                Intent intent = new Intent(this,typeof(App4.plan_main));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_share)
            {
            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }


        private void TakeAPicture(IMenuItem item)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            Uri uri = FileProvider.GetUriForFile(this, "aiu", App._file);

            intent.PutExtra(MediaStore.ExtraOutput, App._file);

            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = FileProvider.GetUriForFile(this, "aiu", App._file);

            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim), "CameraAppDemo");

            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }

        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        void TimeSelectOnClick(IMenuItem item)
        {


            //LinearLayout cardLinear = (LinearLayout)FindViewById(Resource.Id.cardLinear);
            //LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
            //LinearLayout linearLayout = (LinearLayout)inflater.Inflate(Resource.Layout.layout1, null);
            //CardView cardView = (CardView)linearLayout.FindViewById(Resource.Id.cardview);
            //TextView timeDisplay = (TextView)linearLayout.FindViewById(Resource.Id.textBox1);
            //TextView textBox2 = (TextView)linearLayout.FindViewById(Resource.Id.textBox2);

            //TimePickerFragment frag = TimePickerFragment.NewInstance(
            //delegate (DateTime time) {
            //timeDisplay.Text = time.ToShortTimeString();
            // });
            //frag.Show(FragmentManager, TimePickerFragment.TAG);

            //cardLinear.AddView(linearLayout, 0);
        }



    }
}
     

