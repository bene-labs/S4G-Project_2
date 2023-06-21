using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    private List<Vector3> path = new List<Vector3>();

    private void test(Vector3 startPoint, Vector3 endPoint)
    {
        path = new List<Vector3>();

        NavMeshPath navPath = new NavMeshPath();
        NavMesh.CalculatePath(startPoint, endPoint, NavMesh.AllAreas,navPath);

        foreach(Vector3 position in navPath.corners)
        {
            path.Add(position);
        }

        GetComponent<LineRenderer>().positionCount = path.Count;
        GetComponent<LineRenderer>().SetPositions(path.ToArray());
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.transform.tag != "Obstacle")
            {
                test(FindObjectOfType<Unit>().transform.position, hit.point);
            }
        }
    }
}
