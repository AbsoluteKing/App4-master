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



namespace App4
{
    // TreePage: contains image resource ID and caption for a tree:
    public class TreePage : Activity
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
    public class TreeCatalog : Activity
    {

        // Built-in tree catalog (could be replaced with a database)
        public TreePage[] treeBuiltInCatalog = {

        };

        // Array of tree pages that make up the catalog:
        private TreePage[] treePages;

        // Create an instance copy of the built-in tree catalog:
        public TreeCatalog() { treePages = treeBuiltInCatalog; }

        //// Indexer (read only) for accessing a tree page:
        //public TreePage this[int i] { get { return treePages[i]; } }

        //// Returns the number of tree pages in the catalog:
        //public int NumTrees { get { return treePages.Length; } }
    }
}