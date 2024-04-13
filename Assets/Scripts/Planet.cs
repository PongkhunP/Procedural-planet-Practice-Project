using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;
    [SerializeField, HideInInspector]

    ShapeGenerator shapeGenerator;
    public ShapeSettings shapeSettings;
    public ColorSetting colourSetting;
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    void OnValidate()
    {
        GeneratePlanet();
    }

    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);
        if(meshFilters == null)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = {Vector3.up,Vector3.down,Vector3.left,Vector3.right,Vector3.forward,Vector3.back};

        for(int i = 0; i < 6; i++)
        {
            if(meshFilters[i] == null)
            {                
                GameObject meshObj = new GameObject("mesh"); // create new GameObject name mesh 
                meshObj.transform.parent = transform; // set the parent of the meshObj to be this obj or in other word set meshObj to be child

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(shapeGenerator , meshFilters[i].sharedMesh ,resolution,directions[i]);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdate()
    {
        Initialize();
        GenerateMesh();
    }
    public void OnColourSettingsUpdate()
    {
        Initialize();
        GenerateColours();
    }

    void GenerateMesh()
    {
        foreach(TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    void GenerateColours()
    {
        foreach(MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSetting.planetColor;
        }
    }
}
