using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;
    MeshCollider mc;

    public bool generate = true;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        mesh = GetComponent<MeshFilter>().mesh;
        mc = gameObject.AddComponent<MeshCollider>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMesh(Vector3[] newVertices, int[] newTriangles) {


        if (count == 10 && generate)
        {
            mesh.vertices = newVertices;
            mesh.triangles = newTriangles;
            mesh.RecalculateTangents();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mc.sharedMesh = null;
            mc.sharedMesh = mesh;
            count = 0;
        }

        count++;
    }
}
