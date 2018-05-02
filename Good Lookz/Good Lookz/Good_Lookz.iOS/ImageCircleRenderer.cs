using CoreAnimation;
using CoreGraphics;
using Foundation;
using Good_Lookz.CustomRenderer;
using Good_Lookz.iOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//Code: https://github.com/jamesmontemagno/ImageCirclePlugin/tree/master/src
[assembly: ExportRenderer(typeof(CircleImage), typeof(ImageCircleRenderer))]

namespace Good_Lookz.iOS
{
    [Preserve(AllMembers = true)]
    public class ImageCircleRenderer : ImageRenderer
    {
        public async static void Init()
        {
            var temp = DateTime.Now;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            CreateCircle();
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
              e.PropertyName == CircleImage.BorderColorProperty.PropertyName ||
              e.PropertyName == CircleImage.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CircleImage.FillColorProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            try
            {
                var min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (nfloat)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.BackgroundColor = ((CircleImage)Element).FillColor.ToUIColor();
                Control.ClipsToBounds = true;

                var borderThickness = ((CircleImage)Element).BorderThickness;

                //Remove previously added layers
                var tempLayer = Control.Layer.Sublayers?
                                       .Where(p => p.Name == borderName)
                                       .FirstOrDefault();
                tempLayer?.RemoveFromSuperLayer();

                var externalBorder = new CALayer();
                externalBorder.Name = borderName;
                externalBorder.CornerRadius = Control.Layer.CornerRadius;
                externalBorder.Frame = new CGRect(-.5, -.5, min + 1, min + 1);
                externalBorder.BorderColor = ((CircleImage)Element).BorderColor.ToCGColor();
                externalBorder.BorderWidth = ((CircleImage)Element).BorderThickness;

                Control.Layer.AddSublayer(externalBorder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }

        const string borderName = "borderLayerName";
    }
}