using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using System;
using static Android.Support.V7.Widget.RecyclerView;
using App4;
using SQLite;
using System.IO;

namespace RecyclerViewer {

    public class SwipeToDeleteCallback : ItemTouchHelper.SimpleCallback
    {
                public static string dbPath = Path.Combine(
                                Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
        public static SQLiteConnection db = new SQLiteConnection(dbPath);
        private PhotoAlbumAdapter mAdapter;



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


            mAdapter.NotifyItemRemoved(position);
            int SwipedId= ListItems.IdList[position];

            SQlite_main.db.Query<Stock>("DELETE FROM Items WHERE _id=?",SwipedId);

            ListItems.Syokika();
            PhotoAlbumAdapter.Getfromdb();

            Console.WriteLine("あいうえお："+SwipedId+"あああああ："+position);


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