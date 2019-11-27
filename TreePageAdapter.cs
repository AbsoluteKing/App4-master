using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using App4;
using SQLite;
using System.IO;
using System.Linq;
using Android.Graphics;

namespace TreePager
{
    class TreePagerAdapter : PagerAdapter
    {
        Context context;

        // Underlying data (tree catalog):
        public TreeCatalog treeCatalog;

        // Load the adapter with the tree catalog at construction time:
        public TreePagerAdapter(Context context, TreeCatalog treeCatalog)
        {
            this.context = context;
            this.treeCatalog = treeCatalog;
        }

        // Return the number of trees in the catalog:
        public override int Count
        {
            get { return 3; }
        }

        // Create the tree page for the given position:
        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            string dbPath = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
            string dbPath_Photo= System.IO.Path.Combine( Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4_Photo.db");

            var db = new SQLiteConnection(dbPath);
            var db_Photo= new SQLiteConnection(dbPath_Photo);

            db.CreateTable<Stock>();
            var table = db.Table<Stock>();

            db_Photo.CreateTable<Photo>();
            var table_Photo = db_Photo.Table<Photo>();

            LayoutInflater inflater = LayoutInflater.From(context);
            LinearLayout linearLayout1 = (LinearLayout)inflater.Inflate(Resource.Layout.layout1, null);
            var imageView = new ImageView(context);

            //for (int i = 0; i == ListItems.PhotoFileList.Count(); i++)
            //{
            //    ListItems.PhotoBitMapList[i] = BitmapFactory.DecodeFile(ListItems.PhotoFileList[i].Path);
            //}
            int i= 0;
            foreach(var s in table_Photo)
            {
                ListItems.PhotoBitMapList[i] = BitmapFactory.DecodeFile(s.Path_Photo);
                    i++;
            }

            imageView.SetImageBitmap(ListItems.PhotoBitMapList[position]);
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.AddView(imageView);
            return imageView;
            //switch (position)
            //{
            //    case 0:
            //        imageView.SetImageResource(treeCatalog[0].imageId);
            //        var viewPager = container.JavaCast<ViewPager>();
            //        viewPager.AddView(imageView);
            //        return imageView;

            //    case 1:
            //        imageView.SetImageResource(treeCatalog[1].imageId);
            //        viewPager = container.JavaCast<ViewPager>();
            //        viewPager.AddView(imageView);
            //        return imageView;

            //    case 2:
            //        viewPager = container.JavaCast<ViewPager>();
            //        LinearLayout linearLayout = (LinearLayout)inflater.Inflate(App4.Resource.Layout.camera, null);
            //        ((ViewPager)container).AddView(linearLayout);
            //        return linearLayout;

            //    default:
            //        imageView.SetImageResource(treeCatalog[0].imageId);
            //        var viewPager1 = container.JavaCast<ViewPager>();
            //        viewPager1.AddView(imageView);
            //        return imageView;


            //}
        }

        // Remove a tree page from the given position.
        public override void DestroyItem(View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as View);
        }

        //public void getPhotofromdb()
        //{
        //    string dbPath = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4_Photo.db");
        //    SQLiteConnection db1 = new SQLiteConnection(dbPath);
        //    db1.CreateTable<Photo>();
        //    var table = db1.Table<Photo>();
        //    int i = 0;
        //    foreach (var s in table)
        //    {
        //        ListItems.PhotoFileList[i] = s.Path_Photo;
        //        i++;
        //    }
        //}

        // Determine whether a page View is associated with the specific key object
        // returned from InstantiateItem (in this case, they are one in the same):
        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == obj;
        }

        // Display a caption for each Tree page in the PagerTitleStrip:
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(treeCatalog[position].caption);
        }
    }
}