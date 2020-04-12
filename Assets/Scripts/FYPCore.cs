using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYP;



public class FYPCore : MonoBehaviour
{
    [SerializeField]
    public Camera mainCamera;

    [SerializeField]
    public Material oculusionShader;

    [SerializeField]
    public Material depthTo3DPointShader;
    public MeshGenerator meshGenerator;

    PhysicalCameraIntectorAPI phyCameraAPI;
    OculusionRenderer oculusionRenderer;
    PointCloudGenerator pointCloudGenerator;


    void Start()
    {
        phyCameraAPI = gameObject.GetComponent<PhysicalCameraIntectorAPI>();

        oculusionRenderer = mainCamera.gameObject.AddComponent<OculusionRenderer>();
        pointCloudGenerator = mainCamera.gameObject.AddComponent<PointCloudGenerator>();

        SetUpOculusionRenderer();
        SetUpPointCloudGenerator();
    }

    void SetUpOculusionRenderer() {

        oculusionRenderer.phyCameraAPI = phyCameraAPI;
        oculusionRenderer.oculusionShader = oculusionShader;

    }

    void SetUpPointCloudGenerator()
    {

        pointCloudGenerator.phyCameraAPI = phyCameraAPI;
        pointCloudGenerator.depthTo3DPointShader = depthTo3DPointShader;
        pointCloudGenerator.meshGenerator = meshGenerator;

    }

}
