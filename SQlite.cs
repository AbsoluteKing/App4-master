using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

//namespace App4
//{
    //[Table("Items")]
    //public class Stock
    //{
    //    [PrimaryKey, AutoIncrement, Column("_id")]
    //    public int Id { get; set; }
    //    //public string Date { get; set; }
    //    //public string Time { get; set; }
    //    public DateTime dateTime { get; set; }
    //    public string Plan { get; set; }
    //    public string Comment { get; set; }
    //}

    //[Table("Items_sorted")]
    //public class Stock_sorted
    //{
    //    [PrimaryKey, AutoIncrement, Column("_id")]
    //    public int Id { get; set; }
    //    public int Date { get; set; }
    //    public string Time { get; set; }
    //    public string Plan { get; set; }
    //    public string Comment { get; set; }
    //}

    //public class SQlite_main
    //{

           //public string dbPath = Path.Combine(
           //     Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
           // SQLiteConnection db = new SQLiteConnection(dbPath);


        //public static TableQuery<Stock> table_sorted;

    //    public static void DoSomeDataAccess(DateTime SelectedDateTime, String SelectedPlan, String SelectedComment)
    //    {
    //        Console.WriteLine("Creating database, if it doesn't already exist");
    //        if (CheckSelfPermission(permission) == Permission.Denied)
    //        {
    //            ActivityCompat.RequestPermissions(this, new[]
    //{
    //            Manifest.Permission.WriteExternalStorage, Manifest.Permission.Camera
    //        }, 0);
    //        }
    

    //        string dbPath = Path.Combine(
    //            Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
    //    SQLiteConnection db = new SQLiteConnection(dbPath);

    //    db.CreateTable<Stock>();

    //        db.Insert(new Stock()
    //        {
    //            dateTime = SelectedDateTime,
    //            Plan = SelectedPlan,
    //            Comment = SelectedComment
    //        });

    //        var table = db.Table<Stock>();
    //        foreach (var s in table)
    //        {
    //            Console.WriteLine(s.Id + " " + s.Plan);
    //        }
    //    }

     //   public void DeleteCard(int position)
     //   {
     //       string dbPath = Path.Combine(
     //Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
     //       SQLiteConnection db = new SQLiteConnection(dbPath);
     //       db.CreateTable<Stock>();
     //       Console.WriteLine("" + db.Delete<Stock>(position + 1));
     //   }

//        public static void SortCard()
//        {
//            //db.Query<Stock>("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Items'");
//            //int i = 0;
//            const string permission = Manifest.Permission.WriteExternalStorage;
//            if (CheckSelfPermission(permission) == Permission.Denied)
//            {
//                ActivityCompat.RequestPermissions(this, new[]
//    {
//                Manifest.Permission.WriteExternalStorage, Manifest.Permission.Camera
//            }, 0);
//            }
//            string dbPath = Path.Combine(
//Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
//            SQLiteConnection db = new SQLiteConnection(dbPath);
//            var table_sorted = db.Query<Stock>("SELECT * FROM Items ORDER BY dateTime ASC");


//            foreach (var s in table_sorted)
//            {
//                Console.WriteLine("ソート後：" + s.Id + "  " + s.dateTime);
//            }
//        }
//    }

//}