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
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id_Photo { get; set; }
        public string Path_Photo { get; set; }
    }


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
        const int PICK_CONTACT_REQUEST = 1221;

        internal static readonly string COUNT_KEY = "count";
        const string permission = Manifest.Permission.WriteExternalStorage;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            //Permission取得
            const string permission = Manifest.Permission.WriteExternalStorage;
            int flag = 0;
            if (CheckSelfPermission(permission) == Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, new[]{Manifest.Permission.WriteExternalStorage, Manifest.Permission.Camera,Manifest.Permission.ReadExternalStorage}, 0);
            }
            
            //初期化
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Console.WriteLine("ああああああああああああ");
            ListItems.Syokika();
            GC.Collect();

            if (CheckSelfPermission(permission) == Permission.Granted)
            {
                Getfromdb();

            }

            // Get our RecyclerView layout:
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            //............................................................
            // Layout Manager Setup:

            // Use the built-in linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);

            // Or use the built-in grid layout manager (two horizontal rows):
            // mLayoutManager = new GridLayoutManager

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
                //Intent intent = new Intent(this, typeof(CameraAppDemo.MainActivity));
                //StartActivity(intent);
            }
            else if (id == Resource.Id.nav_gallery)
            {
                Intent intent = new Intent(this, typeof(App4.TreeMain));
                StartActivity(intent);
            }

            else if (id == Resource.Id.nav_manage)
            {
                Intent intent = new Intent();
                intent.SetAction(Settings.ActionApplicationDetailsSettings);
                intent.SetData(Uri.Parse("package:" + this.PackageName));
                System.Console.WriteLine("package:" + this.PackageName);
                StartActivity(intent);

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private void TakeAPicture(IMenuItem item)
        {
            CreateDirectoryForPictures();
            GC.Collect();
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            string dbPath_Photo = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4_Photo.db");
            var db_Photo = new SQLiteConnection(dbPath_Photo);
            db_Photo.CreateTable<Photo>();

            db_Photo.Insert(new Photo()
            {
                Path_Photo = App._file.Path
            });

            Uri uri = FileProvider.GetUriForFile(this, "aiu", App._file);
            System.Console.WriteLine("GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());

            intent.PutExtra(MediaStore.ExtraOutput, uri);
            this.StartActivityForResult(intent, PICK_CONTACT_REQUEST);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            System.Console.WriteLine(resultCode);
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = FileProvider.GetUriForFile(this, "aiu", App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            // Dispose of the Java side bitmap.
            GC.Collect();
            //Finish();
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

        //public void SortCard()
        //{
        //    string dbPath = System.IO.Path.Combine(
        //                    Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
        //    SQLiteConnection db = new SQLiteConnection(dbPath);
        //    var table_sorted = db.Query<Stock>("SELECT * FROM Items ORDER BY dateTime ASC");

        //    foreach (var s in table_sorted)
        //    {
        //        System.Console.WriteLine("ソート後：" + s.Id + "  " + s.dateTime);
        //    }
        //}

        protected void Getfromdb()
        {
            string dbPath = System.IO.Path.Combine(
                                Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
            SQLiteConnection db = new SQLiteConnection(dbPath);
            var table_sorted = db.Query<Stock>("SELECT * FROM Items ORDER BY dateTime ASC");
            foreach (var a in table_sorted)
            {
                ListItems.IdList.Add(a.Id);
                ListItems.DateList.Add(a.dateTime.Year + "/" + a.dateTime.Month + "/" + a.dateTime.Day);
                ListItems.TimeList.Add(a.dateTime.Hour.ToString() + ":" + a.dateTime.Minute.ToString());
                ListItems.CommentList.Add(a.Comment);
                ListItems.PlanList.Add(a.Plan);
            }
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
        
        public static List<string> PhotoPathList = new List<string>();
        public static List<Bitmap> PhotoBitMapList = new List<Bitmap>();
        public static List<ImageView> PhotoImageViewList = new List<ImageView>();

        public static void Syokika()
        {
            IdList = new List<int>();
            TimeList = new List<string>();
            DateList = new List<string>();
            CommentList = new List<string>();
            PlanList = new List<string>();
            PhotoBitMapList = new List<Bitmap>();}



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
            vh.Text_Card_Date.Text = ListItems.DateList[position];
            vh.Text_Card_Time.Text = ListItems.TimeList[position];
            vh.Text_Card_Plan.Text = ListItems.PlanList[position];
            vh.Text_Card_Comment.Text = ListItems.CommentList[position];
        }
        
        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return ListItems.DateList.Count; }
        }



        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }
    }
}

     

