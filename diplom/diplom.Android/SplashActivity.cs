﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diplom.Droid
{
    [Activity(Label = "Budget&Tasks", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(ApplicationContext, typeof(MainActivity)));
            // Create your application here
        }
    }
}