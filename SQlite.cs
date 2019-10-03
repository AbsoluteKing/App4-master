using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace App4
{
    [Table("Items")]
    public class Stock
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        //public string Date { get; set; }
        //public string Time { get; set; }
        public DateTime dateTime { get; set; }
        public string Plan { get; set; }
        public string Comment { get; set; }
    }

    [Table("Items_sorted")]
    public class Stock_sorted
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int Date { get; set; }
        public string Time { get; set; }
        public string Plan { get; set; }
        public string Comment { get; set; }
    }

    public class SQlite_main
    {
        public static string dbPath = Path.Combine(
                                Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4no.db");
        public static SQLiteConnection db = new SQLiteConnection(dbPath);
        //public static TableQuery<Stock> table_sorted;

        public static void DoSomeDataAccess(DateTime SelectedDateTime, String SelectedPlan, String SelectedComment)
        {
            Console.WriteLine("Creating database, if it doesn't already exist");

            db.CreateTable<Stock>();

            db.Insert(new Stock()
            {
                dateTime = SelectedDateTime,
                Plan = SelectedPlan,
                Comment = SelectedComment
            });

            var table = db.Table<Stock>();
            foreach (var s in table)
            {
                Console.WriteLine(s.Id + " " + s.Plan);
            }
        }

        public static void DeleteCard(int position)
        {
            db.CreateTable<Stock>();
            Console.WriteLine("" + db.Delete<Stock>(position + 1));
        }

        public static void SortCard()
        {
            //db.Query<Stock>("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Items'");
            //int i = 0;


            var table_sorted = db.Query<Stock>("SELECT * FROM Items ORDER BY dateTime ASC");


            foreach (var s in table_sorted)
            {
                Console.WriteLine("ソート後："+s.Id+"  "+s.dateTime);
            }
        }
    }

}