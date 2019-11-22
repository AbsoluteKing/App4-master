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

namespace TreePager
{
    [Activity(Label = "TreePager")]
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
            

            // Set up the adapter for the ViewPager
            viewPager.Adapter = new TreePagerAdapter(this, treeCatalog);
        }

    }

}