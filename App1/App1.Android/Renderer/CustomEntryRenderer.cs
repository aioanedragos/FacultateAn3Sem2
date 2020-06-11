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
using App1.CustomRenderer;
using App1.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace App1.Droid.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { 

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);

            if (Control != null) {

                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}