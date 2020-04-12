using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera gameCamera;
    public LayerMask raycastMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {


            //StartCoroutine(GetRequest("http://172.27.218.57:4000/api/synchronise?coord=1"));


        }
    }


    //IEnumerator GetRequest(string uri)
    //{


    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //    {
    //        // Request and wait for the desired page.
    //        yield return webRequest.SendWebRequest();
   

    //        string[] pages = uri.Split('/');
    //        int page = pages.Length - 1;

    //        if (webRequest.isNetworkError)
    //        {
    //            Debug.Log(pages[page] + ": Error: " + webRequest.error);
    //        }
    //        else
    //        {
    //            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit info;
    //            if (Physics.Raycast(ray, out info, 100, raycastMask))
    //            {
    //                Vector3 instantPoint = info.point + (Vector3.up * 0.2f);
    //                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //                cube.transform.position = instantPoint;
    //                cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    //                cube.AddComponent<Rigidbody>();

    //            }

    //        }
    //    }
    //}
}
