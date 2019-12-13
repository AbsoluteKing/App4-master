using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using App4;
using Android.Support.V7.Widget;
using SQLite;
using Android.Graphics;
using TreePager;

namespace App4
{
    [Activity(Label = "ENTER YOUR PLANS")]
    public class TreeMain: Activity
    {
        // Tree catalog that is managed by the adapter:
        TreeCatalog treeCatalog;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            // Locate the ViewPager:
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            

            LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
            // Instantiate the tree catalog:
            treeCatalog = new TreeCatalog();
            
            string dbPath_Photo = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4_Photo.db");
            
            //var db_Photo = new SQLiteConnection(dbPath_Photo);



            using (var db_Photo = new SQLiteConnection(dbPath_Photo))
            {
                db_Photo.CreateTable<Photo>();
                var table_Photo = db_Photo.Table<Photo>();

                foreach(var s in table_Photo) {
                    ListItems.PhotoPathList.Add(s.Path_Photo);
                }

            } // ここでFileStreamオブジェクトのDisposeメソッドが呼び出される
            

            // Set up the adapter for the ViewPager\
            viewPager.Adapter=new TreePagerAdapter(this,treeCatalog);
        }

        public override void OnBackPressed()
        {
            TreePagerAdapter.bmp = null;
            ListItems.Syokika();
            
            Console.WriteLine("ああああああああああああああああああああああああああああああ");
            Finish();
        }
    }

}