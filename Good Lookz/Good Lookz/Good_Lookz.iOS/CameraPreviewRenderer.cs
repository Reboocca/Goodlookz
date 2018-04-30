using Good_Lookz.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.iOS;

namespace Good_Lookz.iOS
{
    public class CameraPreviewRenderer : ViewRenderer<CameraPreview, UICameraPreview>
    {
        UICameraPreview uiCameraPreview;

        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                uiCameraPreview = new UICameraPreview(e.NewElement.Camera);
                SetNativeControl(uiCameraPreview);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
                uiCameraPreview.Tapped -= OnCameraPreviewTapped;
            }
            if (e.NewElement != null)
            {
                // Subscribe
                uiCameraPreview.Tapped += OnCameraPreviewTapped;
            }
        }

        void OnCameraPreviewTapped(object sender, EventArgs e)
        {
            if (uiCameraPreview.IsPreviewing)
            {
                uiCameraPreview.CaptureSession.StopRunning();
                uiCameraPreview.IsPreviewing = false;
            }
            else
            {
                uiCameraPreview.CaptureSession.StartRunning();
                uiCameraPreview.IsPreviewing = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.CaptureSession.Dispose();
                Control.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}