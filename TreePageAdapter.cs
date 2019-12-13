using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using App4;
using SQLite;
using Android.Graphics;

namespace TreePager
{
    class TreePagerAdapter : PagerAdapter
    {
        Context context;
        // Underlying data (tree catalog):
        public TreeCatalog treeCatalog;
        public static Bitmap bmp;

        // Load the adapter with the tree catalog at construction time:
        public TreePagerAdapter(Context context, TreeCatalog treeCatalog)
        {
            this.context = context;
            this.treeCatalog = treeCatalog;
        }

        // Return the number of trees in the catalog:
        public override int Count
        {
            get { return ListItems.PhotoPathList.Count; }
        }

        // Create the tree page for the given position:
        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            // Instantiate the ImageView and give it an image:



            var imageView = new ImageView(context);
            string dbPath_Photo = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4_Photo.db");
            var db_Photo = new SQLiteConnection(dbPath_Photo);

            //スライドごとに撮った画像を表示
            var A = db_Photo.Query<Photo>("SELECT * FROM Items WHERE _id = ?", position);
            foreach(var s in A)
            {
                bmp = BitmapFactory.DecodeFile(s.Path_Photo);
                imageView.SetImageBitmap(bmp);
                bmp = null;
                GC.Collect();
            }

            // Add the image to the ViewPager:
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.AddView(imageView);
            
            return imageView;
        }

        // Remove a tree page from the given position.
        public override void DestroyItem(View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as View);
        }
        // Determine whether a page View is associated with the specific key object
        // returned from InstantiateItem (in this case, they are one in the same):
        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == obj;
        }
    }
}