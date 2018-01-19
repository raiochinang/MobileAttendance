using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Media;
using System;
using System.Threading.Tasks;
using Android.Content;
using SQLite;
using System.IO;
using SQLite.Net;
using System.Collections.Generic;

namespace DroidCamApp
{
    [Activity(Label = "DroidCamApp", Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        string sqliteFilename;
        string path;
        string EmployeeIDValue = string.Empty;
        private ListView lv;
        private List<string> data;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            TextView EmployeeID = FindViewById<TextView>(Resource.Id.txtEmployeeID);
            lv = FindViewById<ListView>(Resource.Id.listView);

           
            button.Click += delegate
            {
                EmployeeIDValue = EmployeeID.Text;
                TakePhoto();
            };

            CreateDB();
            DisplayData();
        }

        private void TakePhoto()
        {
            var picker = new MediaPicker(this);
            Intent intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions { 
                Name = "test.jpg",
                Directory="MediaPickerSample"
            });
            StartActivityForResult(intent, 1);
        }

        private void DisplayData()
        {
           

            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            path = Path.Combine(documentsPath, sqliteFilename);
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroidN();
            var param = new SQLiteConnectionString(path, false);
            var connection = new SQLiteConnection(platform, path);
            var data = connection.Table<AttendIn>();


            //ArrayAdapter<AttendIn> adapter = new ArrayAdapter<AttendIn>(this, Android.Resource.Layout.SimpleListItem2, data.ToList());
            //lv.Adapter = adapter;


            //var x = test;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            if(resultCode == Result.Canceled)
            {
                return;
            }

            data.GetMediaFileExtraAsync(this).ContinueWith(t=>
            {
                var r = t.Result.Path;
                try
                {
                    sqliteFilename = "myDb.db3";
                    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
                    path = Path.Combine(documentsPath, sqliteFilename);
                    var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroidN();
                    var param = new SQLiteConnectionString(path, false);
                    var connection = new SQLiteConnection(platform, path);
                    connection.CreateTable<AttendIn>();  
                    AttendIn i = new AttendIn();
                    i.EmployeeID = EmployeeIDValue;
                    i.Petsa = System.DateTime.Now;
                   
                    connection.Insert(i);
                    Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short);
                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show(); 
                }
               

                //Save to sqlLie
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void CreateDB()  
        {  
            var output = "";  
            sqliteFilename = "myDb.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            path = Path.Combine(documentsPath, sqliteFilename);
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroidN();
            var param = new SQLiteConnectionString(path, false);
            output += "Creating Databse if it doesnt exists";
            var connection =  new SQLiteConnection(platform, path);
            output += "\n Database Created....";  
            Toast.MakeText(this, output, ToastLength.Short).Show();
        }  
    }
}

