
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net;

namespace DroidCamApp
{
    [Activity(Label = "Login", MainLauncher = true, Icon = "@mipmap/icon")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);
            CreateDB();
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            EditText username = FindViewById<EditText>(Resource.Id.txtUsername);
            EditText password = FindViewById<EditText>(Resource.Id.txtPassword);

            btnLogin.Click += delegate {
                if(username.Text == "ADMIN" && password.Text == "CONFIRM")
                {
                    StartActivity(typeof(AdminActivity));
                }
                else if (username.Text == "USER" && password.Text == "USER")
                { 
                    StartActivity(typeof(MainActivity));
                }
                else
                {
                    password.Text = string.Empty;
                    username.Text = string.Empty;
                    Toast.MakeText(this, "Invalid Username/Password.", ToastLength.Short);
                }

            };

        }

        public void CreateDB()  
        {  
            string sqliteFilename = "myDb.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            string path = Path.Combine(documentsPath, sqliteFilename);
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroidN();
            var param = new SQLiteConnectionString(path, false);
            var connection =  new SQLiteConnection(platform, path);

        }  
    }
}
