//using Android.OS;
//using Java.IO;
//using System;
//using Environment = Android.OS.Environment;

//namespace plancreate
//{
//public class Plancreate
//    {
//        public static void createfile()
//        {
//            App4.App.plans_dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments), "plans");
//            if (!App4.App.plans_dir.Exists())
//            {
//                App4.App.plans_dir.Mkdirs();
//            }
//            App4.App.plans = new File(App4.App.plans, String.Format("myPlan_{0}.txt", 19971019));
//        }

//    }
//}