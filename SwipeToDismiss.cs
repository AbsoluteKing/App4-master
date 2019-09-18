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
        private PhotoAlbumAdapter mAdapter;



        public SwipeToDeleteCallback(PhotoAlbumAdapter adapter) : base(0, ItemTouchHelper.Left | ItemTouchHelper.Right)
        {
            mAdapter = adapter;
        }

        public override void OnSwiped(ViewHolder viewHolder, int direction)
        {
            int position = viewHolder.AdapterPosition;
            mAdapter.DateList.RemoveAt(position);
            mAdapter.NotifyItemRemoved(position);
            mAdapter.NotifyItemRangeChanged(position, mAdapter.DateList.Count-1);
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