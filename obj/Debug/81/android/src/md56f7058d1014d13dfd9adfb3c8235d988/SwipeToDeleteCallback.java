package md56f7058d1014d13dfd9adfb3c8235d988;


public class SwipeToDeleteCallback
	extends android.support.v7.widget.helper.ItemTouchHelper.SimpleCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSwiped:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnSwiped_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onChildDraw:(Landroid/graphics/Canvas;Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$ViewHolder;FFIZ)V:GetOnChildDraw_Landroid_graphics_Canvas_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_ViewHolder_FFIZHandler\n" +
			"n_onMove:(Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$ViewHolder;Landroid/support/v7/widget/RecyclerView$ViewHolder;)Z:GetOnMove_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_ViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"";
		mono.android.Runtime.register ("RecyclerViewer.SwipeToDeleteCallback, App4", SwipeToDeleteCallback.class, __md_methods);
	}


	public SwipeToDeleteCallback (int p0, int p1)
	{
		super (p0, p1);
		if (getClass () == SwipeToDeleteCallback.class)
			mono.android.TypeManager.Activate ("RecyclerViewer.SwipeToDeleteCallback, App4", "System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public void onSwiped (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onSwiped (p0, p1);
	}

	private native void n_onSwiped (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);


	public void onChildDraw (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.ViewHolder p2, float p3, float p4, int p5, boolean p6)
	{
		n_onChildDraw (p0, p1, p2, p3, p4, p5, p6);
	}

	private native void n_onChildDraw (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.ViewHolder p2, float p3, float p4, int p5, boolean p6);


	public boolean onMove (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1, android.support.v7.widget.RecyclerView.ViewHolder p2)
	{
		return n_onMove (p0, p1, p2);
	}

	private native boolean n_onMove (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1, android.support.v7.widget.RecyclerView.ViewHolder p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
