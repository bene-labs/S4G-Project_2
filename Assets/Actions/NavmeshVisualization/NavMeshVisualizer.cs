using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


//source https://www.youtube.com/watch?v=zxmz-LV6E6g
public class NavMeshVisualizer : MonoBehaviour
{
    public static NavMeshVisualizer instance;

    [SerializeField]
    private Material visualizationMaterial;
    [SerializeField]
    private Vector3 meshOffset = new Vector3(0,0.05f,0);

    private GameObject visualization;

    private void Awake()
    {
        instance = this;
    }

    private void GenerateVisualizationObject()
    {
        if(visualization == null)
        {
            visualization = new GameObject("Navmesh Visualization");
            visualization.AddComponent<MeshRenderer>();
            visualization.AddComponent<MeshFilter>();

            NavMeshModifier mod = visualization.AddComponent<NavMeshModifier>();
            mod.ignoreFromBuild = true;

            visualization.transform.SetParent(transform);
        }
    }

    private void CalculateVisualization()
    {
        MeshRenderer renderer = visualization.GetComponent<MeshRenderer>();
        MeshFilter filter = visualization.GetComponent<MeshFilter>();

        Mesh navmesh = new Mesh();
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        navmesh.SetVertices(triangulation.vertices);
        navmesh.SetIndices(triangulation.indices, MeshTopology.Triangles, 0);

        renderer.sharedMaterial = visualizationMaterial;
        filter.mesh = navmesh;

    }

    public void RenderNavMesh()
    {
        GenerateVisualizationObject();
        CalculateVisualization();
        visualization.transform.position = meshOffset;

        visualization.SetActive(true);
    }

    public void ClearNavMesh()
    {
        if(visualization != null)
        {
            visualization.SetActive(false);
        }
    }
}
