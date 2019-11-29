package md591ac425b7bebc7a65cec0ef28027c2db;


public class TreeCatalog
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("App4.TreeCatalog, App4", TreeCatalog.class, __md_methods);
	}


	public TreeCatalog ()
	{
		super ();
		if (getClass () == TreeCatalog.class)
			mono.android.TypeManager.Activate ("App4.TreeCatalog, App4", "", this, new java.lang.Object[] {  });
	}

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
