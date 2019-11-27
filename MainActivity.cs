using System;
using System.Collections.Generic;
using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V4.Util;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using RecyclerViewer;
using SQLite;
using static Android.Graphics.Bitmap;
using ActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

namespace App4
{ 

    public static class App
    {
        public static File _file;
        public static File _dir;
        public static File plans;
        public static File plans_dir;
        public static Bitmap bitmap;
    }

    [Table("Items")]
    public class Stock
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public string Plan { get; set; }
        public string Comment { get; set; }
    }

    [Table("Items")]
    public class Photo
    {
        [PrimaryKey, AutoIncrement, Column("_id1")]
        public int Id_Photo { get; set; }
        public string Path_Photo { get; set; }
    }

    //（派生クラス名）：基底クラス名
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class  MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        // RecyclerView instance that displays the photo album:
        RecyclerView mRecyclerView;

        // Layout manager that lays out each card in the RecyclerView:
        RecyclerView.LayoutManager mLayoutManager;

        // Adapter that accesses the data set (a photo album):
        PhotoAlbumAdapter mAdapter;

        // Photo album that is managed by the adapter:
        PhotoAlbum mPhotoAlbum;
        private ImageView _imageView;

        internal static readonly string COUNT_KEY = "count";
        const string permission = Manifest.Permission.WriteExternalStorage;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            //Permission取得
            const string permission = Manifest.Permission.WriteExternalStorage;
            if (CheckSelfPermission(permission) == Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, new[]{Manifest.Permission.WriteExternalStorage, Manifest.Permission.Camera,Manifest.Permission.ReadExternalStorage}, 0);
            }
            
            //初期化
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            ListItems.Syokika();

            // Get our RecyclerView layout:
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            //............................................................
            // Layout Manager Setup:

            // Use the built-in linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);

            // Or use the built-in grid layout manager (two horizontal rows):
            // mLayoutManager = new GridLayoutManager
            //        (this, 2, GridLayoutManager.Horizontal, false);

            // Plug the layout manager into the RecyclerView:
            mRecyclerView.SetLayoutManager(mLayoutManager);
            //............................................................
            // Adapter Setup:

            // Create an adapter for the RecyclerView, and pass it the
            // data set (the photo album) to manage:
            mAdapter = new PhotoAlbumAdapter(mPhotoAlbum);


            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(mAdapter);

            ItemTouchHelper itemTouchHelper = new
            ItemTouchHelper(new SwipeToDeleteCallback(mAdapter));
            itemTouchHelper.AttachToRecyclerView(mRecyclerView);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //floatingactionButton実装
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += delegate {
                Intent intent = new Intent(this, typeof(App4.plan_main));
                StartActivity(intent);
            };

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnBackPressed()
        {
            //androidの戻るボタンが押された時の操作を記述
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //オプションボタンを追加
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
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

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                TakeAPicture(item);
            }
            else if (id == Resource.Id.nav_gallery)
            {
                Intent intent = new Intent(this, typeof(TreePager.TreeMain));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_slideshow)
            {
            }
            else if (id == Resource.Id.nav_manage)
            {
                Intent intent = new Intent(this,typeof(App4.plan_main));
                StartActivity(intent);

            }
            else if (id == Resource.Id.nav_share)
            {
                var alarmIntent = new Intent(this, typeof(AlarmReceiver));
                alarmIntent.PutExtra("title", "Hello");
                alarmIntent.PutExtra("message", "World!");

                var pending = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

                var alarmManager = GetSystemService(AlarmService).JavaCast<AlarmManager>();
                alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pending);
                Console.WriteLine("あいえう");
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
            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            CreateDirectoryForPictures();
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            string Name = String.Format("myPhoto_{0}.jpg" , Guid.NewGuid());
            string dbPath= System.IO.Path.Combine(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim).ToString(), "App4_Photo.db");
            string photoPath = System.IO.Path.Combine(App._dir.Path, Name);
            SQLiteConnection db = new SQLiteConnection(dbPath);

            db.CreateTable<Photo>();

            db.Insert(new Photo()
            {
                Path_Photo = photoPath
            });

            App._file = new File(App._dir, Name);
            System.Console.WriteLine("ファイル："+App._file);

            Uri uri = FileProvider.GetUriForFile(this, "aiu", App._file);
            

            intent.PutExtra(MediaStore.ExtraOutput, App._file);
            //Bitmap bitmap = (Bitmap)Data.getdata();
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AtomicFile atomicFile = new AtomicFile(App._file);


            // Make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = FileProvider.GetUriForFile(this, "aiu", App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private Android.Graphics.Bitmap NGetBitmap(Android.Net.Uri uriImage)
        {
            Android.Graphics.Bitmap mBitmap = null;
            mBitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, uriImage);
            return mBitmap;
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

    }

    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        //public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }
        public CardView Text_Card { get; private set; }
        public TextView Text_Card_Plan { get; private set; }
        public TextView Text_Card_Comment { get; private set; }
        public TextView Text_Card_Date { get; private set; }
        public TextView Text_Card_Time { get; private set; }

        // Get references to the views defined in the CardView layout.
        public PhotoViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            Text_Card = itemView.FindViewById<CardView>(Resource.Id.Text_Card);
            Text_Card_Plan = itemView.FindViewById<TextView>(Resource.Id.Text_Card_Plan);
            Text_Card_Comment = itemView.FindViewById<TextView>(Resource.Id.Text_Card_Comment);
            Text_Card_Date = (TextView)itemView.FindViewById(Resource.Id.Text_Card_Date);
            Text_Card_Time = itemView.FindViewById<TextView>(Resource.Id.Text_Card_Time);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class ListItems
    {
        public static List<int> IdList = new List<int>();
        public static List<string> DateList = new List<string>();
        public static List<string> TimeList = new List<string>();
        public static List<string> CommentList = new List<string>();
        public static List<string> PlanList = new List<string>();
        
        public static List<File> PhotoFileList = new List<File>();
        public static List<Bitmap> PhotoBitMapList = new List<Bitmap>();
        public static List<ImageView> PhotoImageViewList = new List<ImageView>();

        public static void Syokika()
        {
            IdList = new List<int>();
            TimeList = new List<string>();
            DateList = new List<string>();
            CommentList = new List<string>();
            PlanList = new List<string>();
        }

    }


    //----------------------------------------------------------------------
    // ADAPTER

    // Adapter to connect the data set (photo album) to the RecyclerView: 
    public class PhotoAlbumAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        // Underlying data set (a photo album):
        public PhotoAlbum mPhotoAlbum;

        // Load the adapter with the data set (photo album) at construction time:
        public PhotoAlbumAdapter(PhotoAlbum photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
            //Getfromdb();
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.layout1, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder vh = holder as PhotoViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the photo album:
            //vh.Image.SetImageResource (mPhotoAlbum[position].PhotoID);
            //vh.Caption.Text = mPhotoAlbum[position].Caption;

            vh.Text_Card_Date.Text = ListItems.DateList[position];
            vh.Text_Card_Time.Text = ListItems.TimeList[position];
            vh.Text_Card_Plan.Text = ListItems.PlanList[position];
            vh.Text_Card_Comment.Text = ListItems.CommentList[position];
        }



        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return ListItems.DateList.Count; }
            //get { return mPhotoAlbum.NumPhotos; }
        }

        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }
    }
}

     

