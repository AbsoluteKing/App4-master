using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Java.Lang;
using App4;
using SQLite;
using Android.Support.V7.Widget;

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
            string dbPath = System.IO.Path.Combine(
                            Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");

            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Stock>();
            var table = db.Table<Stock>();

            LayoutInflater inflater = LayoutInflater.From(context);
            LinearLayout linearLayout1 = (LinearLayout)inflater.Inflate(Resource.Layout.layout1, null);
            var imageView = new ImageView(context);

            //switch (position) {
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
            //        LinearLayout linearLayout = (LinearLayout)inflater.Inflate(App4.Resource.Layout.camera,null);
            //        ((ViewPager)container).AddView(linearLayout);
            //        return linearLayout;

            //    case 3:
            //        foreach (var a in table)
            //        {
            //            LinearLayout linearLayout1 = (LinearLayout)inflater.Inflate(Resource.Layout.layout1, null);
            //            CardView Text_Card = linearLayout1.FindViewById<CardView>(Resource.Id.Text_Card);
            //            TextView Text_Card_Plan = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Plan);
            //            TextView Text_Card_Comment = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Comment);
            //            TextView Text_Card_Date = (TextView)linearLayout1.FindViewById(Resource.Id.Text_Card_Date);
            //            TextView Text_Card_Time = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Time);

            //            Text_Card_Date.Text = a.Date;
            //            Text_Card_Time.Text = a.Time;
            //            Text_Card_Comment.Text = a.Comment;
            //            Text_Card_Plan.Text = a.Plan;
            //        }

            //        return 0;
            //            default:
            //        imageView.SetImageResource(treeCatalog[0].imageId);
            //        var viewPager1 = container.JavaCast<ViewPager>();
            //        viewPager1.AddView(imageView);
            //        return imageView;

            switch (position) {
                case 0:
                foreach (var a in table)
                {      

                    //CardView Text_Card = linearLayout1.FindViewById<CardView>(Resource.Id.Text_Card);
                    //TextView Text_Card_Plan = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Plan);
                    //TextView Text_Card_Comment = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Comment);
                    //TextView Text_Card_Date = (TextView)linearLayout1.FindViewById(Resource.Id.Text_Card_Date);
                    //TextView Text_Card_Time = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Time);

                    //Text_Card_Date.Text = a.Date;
                    //Text_Card_Time.Text = a.Time;
                    //Text_Card_Comment.Text = a.Comment;
                    //Text_Card_Plan.Text = a.Plan;

                    var viewPager = container.JavaCast<ViewPager>();
                    viewPager.AddView(linearLayout1);

                }
                    return linearLayout1;

                default:
                    foreach (var a in table)
                    {

                        //CardView Text_Card = linearLayout1.FindViewById<CardView>(Resource.Id.Text_Card);
                        //TextView Text_Card_Plan = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Plan);
                        //TextView Text_Card_Comment = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Comment);
                        //TextView Text_Card_Date = (TextView)linearLayout1.FindViewById(Resource.Id.Text_Card_Date);
                        //TextView Text_Card_Time = linearLayout1.FindViewById<TextView>(Resource.Id.Text_Card_Time);

                        //Text_Card_Date.Text = a.Date;
                        //Text_Card_Time.Text = a.Time;
                        //Text_Card_Comment.Text = a.Comment;
                        //Text_Card_Plan.Text = a.Plan;

                        var viewPager = container.JavaCast<ViewPager>();
                        viewPager.AddView(linearLayout1);

                    }
                    return linearLayout1;



            }

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

        // Display a caption for each Tree page in the PagerTitleStrip:
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(treeCatalog[position].caption);
        }
    }
}