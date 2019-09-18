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
        public string Date { get; set; }
        public string Time { get; set; }
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
        public static string dbPath_sorted = Path.Combine(
                        Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "App4_sorted.db");
        public static SQLiteConnection db = new SQLiteConnection(dbPath);
        public static SQLiteConnection db_sorted = new SQLiteConnection(dbPath_sorted);

        public static void DoSomeDataAccess(string SelectedDate, String SelectedTime, String SelectedPlan, String SelectedComment)
        {
            Console.WriteLine("Creating database, if it doesn't already exist");

            db.CreateTable<Stock>();

            db.Insert(new Stock()
            {
                Date = SelectedDate,
                Time = SelectedTime,
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
            db_sorted.CreateTable<Stock_sorted>();
            var table = db.Table<Stock>();
            var table_sorted = db.Table<Stock>();

            db.Query<Stock>("SELECT * FROM Items ORDER BY Date DESC");
            table_sorted = db.Table<Stock>();
            foreach (var s in table_sorted)
            {
                Console.WriteLine("！！！！！！！！！：" + s.Id + "　　" + s.Date);
            }

            db.Query<Stock>("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Items'");
            var Table_P = db.Table<Stock>();
            //db.Query<Stock>("DELETE FROM Items");
            //var Table_P = db.Query<Stock>("DELETE FROM SQLITE_SEQUENCE WHERE NAME='Items'");
            foreach (var a in Table_P)
            {
                Console.WriteLine("ソート後：" + a.Id + "　　" + a.Date);
            }
        }
    }

}