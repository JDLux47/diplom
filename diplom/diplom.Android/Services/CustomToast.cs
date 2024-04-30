using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using diplom.Droid.Services;
using diplom.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;
using View = Android.Views.View;

[assembly: Dependency(typeof(CustomToast))]
namespace diplom.Droid.Services
{
    public class CustomToast : ICustomToast
    {
        public void ShowCustomToast(string message, string backgroundColor, string textColor)
        {
            // Получаем LayoutInflater через Context
            LayoutInflater inflater = (LayoutInflater)Application.Context.GetSystemService(Context.LayoutInflaterService);

            View layout = inflater.Inflate(Resource.Layout.ToastLayout, null);

            // Создание объекта GradientDrawable
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.SetShape(ShapeType.Rectangle); // Установка формы прямоугольника
            gradientDrawable.SetCornerRadius(60); // Установка радиуса скругления углов
            gradientDrawable.SetColor(Android.Graphics.Color.ParseColor(backgroundColor)); // Установка цвета фона

            LinearLayout toastLayout = layout.FindViewById<LinearLayout>(Resource.Id.custom_toast_layout);
            TextView toastMessage = layout.FindViewById<TextView>(Resource.Id.custom_toast_message);

            toastLayout.Background = gradientDrawable;

            toastMessage.Text = message;

            string hexTextColor = textColor;
            Android.Graphics.Color textCol = HexToColor(hexTextColor);
            toastMessage.SetTextColor(new Android.Graphics.Color(textCol));

            Toast toast = new Toast(Application.Context);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Duration = ToastLength.Long;
            toast.View = layout;
            toast.Show();
        }

        private Android.Graphics.Color HexToColor(string hexColor)
        {
            if (hexColor.StartsWith("#"))
            {
                hexColor = hexColor.Substring(1);
            }

            int argb = (int)(Convert.ToUInt32(hexColor, 16) + 0xFF000000);
            return new Android.Graphics.Color(argb);
        }
    }
}