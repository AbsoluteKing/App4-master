using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using System;
using static Android.Support.V7.Widget.RecyclerView;
using App4;
using SQLite;
using System.IO;

namespace App4 {

    public class SwipeToDeleteCallback : ItemTouchHelper.SimpleCallback
    {

        private App4.PhotoAlbumAdapter mAdapter;



        public SwipeToDeleteCallback(PhotoAlbumAdapter adapter) : base(0, ItemTouchHelper.Left | ItemTouchHelper.Right)
        {
            mAdapter = adapter;
        }

        public override void OnSwiped(ViewHolder viewHolder, int direction)
        {
            int position = viewHolder.AdapterPosition;
            ListItems.DateList.RemoveAt(position);
            ListItems.TimeList.RemoveAt(position);
            ListItems.CommentList.RemoveAt(position);
            ListItems.PlanList.RemoveAt(position);

                            string dbPath = Path.Combine(
                                Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
        SQLiteConnection db = new SQLiteConnection(dbPath);

        mAdapter.NotifyItemRemoved(position);
            int SwipedId= ListItems.IdList[position];

            db.Query<Stock>("DELETE FROM Items WHERE _id=?",SwipedId);

            ListItems.Syokika();
            Getfromdb();

            Console.WriteLine("あいうえお："+SwipedId+"あああああ："+position);


        }

        public void Getfromdb()
        {

            string dbPath = Path.Combine(
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

        public override void OnChildDraw(Android.Graphics.Canvas c, RecyclerView recyclerView, ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
        }

        public override bool OnMove(RecyclerView recyclerView, ViewHolder viewHolder, ViewHolder target)
        {
            throw new NotImplementedException();
        }
    }

}