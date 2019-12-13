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
using App4;

namespace TreePager
{
    // TreePage: contains image resource ID and caption for a tree:
    public class TreePage
    {
        // Image ID for this tree image:
        public int imageId;

        // Caption text for this image:
        public string caption;

        // Returns the ID of the image:
        public int ImageID { get { return imageId; } }

        // Returns the caption text for the image:
        public string Caption { get { return caption; } }
    }

    // Tree catalog: holds image resource IDs and caption text:
    public class TreeCatalog
    {
        // Built-in tree catalog (could be replaced with a database)
        static TreePage[] treeBuiltInCatalog = {
        };

        // Array of tree pages that make up the catalog:
        private TreePage[] treePages;
        
        public TreeCatalog() { treePages = treeBuiltInCatalog; }
    }
}