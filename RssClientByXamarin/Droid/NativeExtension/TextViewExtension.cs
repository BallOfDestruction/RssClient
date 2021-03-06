﻿using Android.OS;
using Android.Text;
using Android.Widget;
using JetBrains.Annotations;

namespace Droid.NativeExtension
{
    public static class TextViewExtension
    {
        public static void SetTextAsHtml([CanBeNull] this TextView textView, [CanBeNull] string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            
            var spanned = Build.VERSION.SdkInt >= BuildVersionCodes.N
                ? Html.FromHtml(text, FromHtmlOptions.ModeLegacy)
                // TODO Разобраться с сеттером текста
#pragma warning disable 618
                : Html.FromHtml(text);
#pragma warning restore 618
            textView?.SetText(spanned, TextView.BufferType.Spannable);
        }
    }
}
