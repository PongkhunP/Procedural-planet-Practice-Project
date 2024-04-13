using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeMesh : MonoBehaviour
{
    Mesh mesh;

    MeshFilter meshFilter;

    void OnValidate()
    {
        ConstructMesh();
    }

    void ConstructMesh()
    { 
        if(meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        meshFilter = GetComponent<MeshFilter>();


        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        }

        meshFilter.sharedMesh = mesh;
        if(mesh == null)
        {
            mesh = new Mesh();
            meshFilter.sharedMesh = mesh;
        }

        Vector3[] vertices = {new Vector3(-1,1,0), new Vector3(1,1,0) , new Vector3(1,-1,0) , new Vector3(-1,-1,0)};
        int[] triangles = {0 , 2 , 3 , 0 , 2, 1}; 

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
