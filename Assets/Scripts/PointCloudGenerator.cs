using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using sl;

namespace FYP
{
    public class PointCloudGenerator : MonoBehaviour
    {
        [Range(0f, 1.0f)] // Limited from 0 - 1 
        public float MeshDensity = .10f;
        public PhysicalCameraIntectorAPI phyCameraAPI;
        public Material depthTo3DPointShader;
        public int i = 0;
        public MeshGenerator meshGenerator;

        Camera cam;

        Vector3[] vertices;
        int[] triangles;


        void Start()
        {
            cam = this.GetComponent<Camera>();
        }



        // Update is called once per frame

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position\

            if (vertices != null)
            {

                for (int i = 0; i < vertices.GetLength(0); i = i + 100)
                {

                    Gizmos.color = Color.yellow;
                    Vector3 temp = vertices[i];
                    Gizmos.DrawSphere(temp, .005f);
                }


            }

            if (triangles != null)
            {
                for (int i = 0; i < triangles.GetLength(0); i = i + 3)
                {
                    Gizmos.color = Color.blue;
                    Vector3 temp = vertices[triangles[i]];
                    Vector3 temp2 = vertices[triangles[i + 1]];
                    Vector3 temp3 = vertices[triangles[i + 2]];
                    Gizmos.DrawLine(temp, temp2);
                    Gizmos.DrawLine(temp2, temp3);
                    Gizmos.DrawLine(temp3, temp);
                }
            }

        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {

            // Copy source to destination
            Graphics.Blit(source, destination);

            Texture2D depth = phyCameraAPI.GetDepthTexture();

            if (depth == null || depth.width == 0 || depth.height == 0)
                return;

            CameraParas paras = phyCameraAPI.GetCameraPara();
            int resolutionX = (int)(paras.ResolutionWidth * MeshDensity);
            int resolutionY = (int)(paras.ResolutionHeight * MeshDensity);
            float cx = paras.cx * MeshDensity;
            float cy = paras.cy * MeshDensity;
            float fx = paras.fx * MeshDensity;
            float fy = paras.fy * MeshDensity;

            depthTo3DPointShader.SetFloat("_Camera_cx", cx);
            depthTo3DPointShader.SetFloat("_Camera_cy", cy);
            depthTo3DPointShader.SetFloat("_Camera_fx", fx);
            depthTo3DPointShader.SetFloat("_Camera_fy", fy);
            depthTo3DPointShader.SetInt("_Camera_ResolutionX", resolutionX);
            depthTo3DPointShader.SetInt("_Camera_ResolutionY", resolutionY);


            RenderTexture tempRenderTexture = new RenderTexture(resolutionX, resolutionY, 16, RenderTextureFormat.ARGBHalf);

            Graphics.Blit(depth, tempRenderTexture, depthTo3DPointShader);

            //Cached Active RenderTexture
            RenderTexture cachedActive = RenderTexture.active;
            RenderTexture.active = tempRenderTexture;

            Texture2D tex2d = new Texture2D(resolutionX, resolutionY, TextureFormat.RGBAHalf, false);
            tex2d.ReadPixels(new Rect(0, 0, resolutionX, resolutionY), 0, 0);

           
            // For each Pixel R = X, G = Y, B = Z 
            Color[] allPixelInfo = tex2d.GetPixels();
            vertices = new Vector3[allPixelInfo.Length];
            triangles = new int[(resolutionX -1) * (resolutionY -1) * 2 * 3];
            int triangleCount = 0;
            for (int i = 0; i < allPixelInfo.Length; i++)
            {

                Color pixelInfo = allPixelInfo[i];
                int u = (i % resolutionX);
                int v = (i / resolutionX);
                double x, y, z;

                x = pixelInfo.r;
                y = pixelInfo.g;
                z = pixelInfo.b;

                vertices[i] = new Vector3(Math.Min(Math.Max((float)x, -100.0f), 100.0f), 
                    Math.Min(Math.Max((float)y, -100.0f), 100.0f),
                    Math.Min(Math.Max((float)z, 0.05f), 20.0f));

                if (u < (resolutionX - 1) && v < (resolutionY - 1))
                {

                    triangles[triangleCount] = i;
                    triangleCount++;
                    triangles[triangleCount] = i + 1;
                    triangleCount++;
                    triangles[triangleCount] = i + resolutionX;
                    triangleCount++;
                    triangles[triangleCount] = i + 1;
                    triangleCount++;
                    triangles[triangleCount] = i + resolutionX + 1;
                    triangleCount++;
                    triangles[triangleCount] = i + resolutionX;
                    triangleCount++;
                }

            }


            meshGenerator.GenerateMesh(vertices, triangles);
    
            //Reset Active RenderTexture
            RenderTexture.active = cachedActive;
        }
    }
}
