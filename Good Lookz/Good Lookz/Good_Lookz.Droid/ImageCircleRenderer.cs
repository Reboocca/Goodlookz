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
using Good_Lookz.Droid;
using Good_Lookz.CustomRenderer;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;

//Code: https://github.com/jamesmontemagno/ImageCirclePlugin/tree/master/src
[assembly: ExportRenderer(typeof(CircleImage), typeof(ImageCircleRenderer))]

namespace Good_Lookz.Droid
{
    [Preserve(AllMembers = true)]

    class ImageCircleRenderer : ImageRenderer
    {
        public ImageCircleRenderer() : base()
        {

        }


        public async static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //Only enable hardware accelleration on lollipop
                if ((int)Android.OS.Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            if (e.PropertyName == CircleImage.BorderColorProperty.PropertyName ||
              e.PropertyName == CircleImage.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CircleImage.FillColorProperty.PropertyName)
            {
                this.Invalidate();
            }
        }


        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = (float)Math.Min(Width, Height) / 2f;

                var borderThickness = ((CircleImage)Element).BorderThickness;

                float strokeWidth = 0f;

                if (borderThickness > 0)
                {
                    var logicalDensity = Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;
                    strokeWidth = (float)Math.Ceiling(borderThickness * logicalDensity + .5f);
                }

                radius -= strokeWidth / 2f;

                var path = new Path();
                path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);

                var paint = new Paint
                {
                    AntiAlias = true
                };
                paint.SetStyle(Paint.Style.Fill);
                paint.Color = ((CircleImage)Element).FillColor.ToAndroid();
                canvas.DrawPath(path, paint);
                paint.Dispose();

                var result = base.DrawChild(canvas, child, drawingTime);

                path.Dispose();
                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2f, Height / 2f, radius, Path.Direction.Ccw);


                if (strokeWidth > 0.0f)
                {
                    paint = new Paint
                    {
                        AntiAlias = true,
                        StrokeWidth = strokeWidth
                    };
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = ((CircleImage)Element).BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}