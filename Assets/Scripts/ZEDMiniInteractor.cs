using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sl;

namespace FYP
{
    public class ZEDMiniInteractor : MonoBehaviour, PhysicalCameraIntectorAPI
    {

        [SerializeField]
        private ZEDRenderingPlane ZedRenderer;

        [SerializeField]
        private ZEDManager zedManager;


        public CameraParas GetCameraPara()
        {
            ZEDCamera zedCam = zedManager.zedCamera;
            CalibrationParameters para = zedCam.GetCalibrationParameters();
            CameraParas fypPara = new CameraParas();

            fypPara.cx = para.leftCam.cx;
            fypPara.cy = para.leftCam.cy;
            fypPara.fx = para.leftCam.fx;
            fypPara.fy = para.leftCam.fy;
            fypPara.ResolutionWidth = (int)para.leftCam.resolution.width;
            fypPara.ResolutionHeight = (int)para.leftCam.resolution.height;
            return fypPara;
        }

        public Texture2D GetColorTexture()
        {
            return ZedRenderer.TextureEye;
        }

        public Texture2D GetDepthTexture()
        {
            return ZedRenderer.TextureDepth;
        }
    }
}
