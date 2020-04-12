using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FYP
{
    public struct CameraParas
    {

        public float fx;
        public float fy;
        public float cx;
        public float cy;
        public int ResolutionWidth;
        public int ResolutionHeight;
    }

    public interface PhysicalCameraIntectorAPI
    {

        // DepthTexture should have depth in Meters stored in R
        Texture2D GetDepthTexture();
        Texture2D GetColorTexture();
        CameraParas GetCameraPara();
    }
}
