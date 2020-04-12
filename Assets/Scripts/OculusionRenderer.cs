using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using sl;

namespace FYP
{
    public class OculusionRenderer : MonoBehaviour
    {

        public PhysicalCameraIntectorAPI phyCameraAPI;
        public Material oculusionShader;

        Camera cam;
 
        // Start is called before the first frame update
        void Start()
        {
            cam = this.GetComponent<Camera>();

            //Enable Depth Capture
            cam.depthTextureMode = DepthTextureMode.Depth;
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
           
            Texture2D depth = phyCameraAPI.GetDepthTexture();
            Texture2D color = phyCameraAPI.GetColorTexture();

            oculusionShader.SetTexture("_DepthTestTexture", depth);
            oculusionShader.SetTexture("_ExternalCameraTexture", color);

            Graphics.Blit(source, destination, oculusionShader);
        }
    }
}