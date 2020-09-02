using Android.Widget;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MusicPlayerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();

            Debug.WriteLine("APP DIR" + Android.OS.Environment.ExternalStorageDirectory.ToString());
            //Debug.WriteLine("APP DIR" + Android.OS.Environment.);
            var path1 = Android.OS.Environment.ExternalStorageDirectory.ToString();
            //var path1 = Android.OS.Environment.GetExternaxlStoragePublicDirectory("mp3");
            //var mp3Files = Directory.EnumerateFiles(path1, "*.mp3", SearchOption.AllDirectories);
            //foreach (string currentFile in mp3Files)
            //{
            //    Debug.WriteLine("Folder: " + currentFile);
            //}
            

        }
        
    }
}