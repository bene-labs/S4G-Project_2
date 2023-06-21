using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance { get; private set; }

    public List<Vector3> validPathPoints;
    public List<Vector3> invalidPathPoints;
    public float lastMousePosition;
    private bool pathValid;

    private NavMeshPath navMeshPath;
    private LineRenderer lineRenderer;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float width;

    // variables for testing
    [SerializeField] private float moveRange;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        navMeshPath = new NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void CalculatePath(Vector3 startPoint, Vector3 endPoint, float maxDistance)
    {
        // validpathPoints = navmesh(startpoint, newEndpoint)
        // path creating performance intensive for long range; max range to calculate path?! maybe 10times the move radius?
        // consider case for no valid path?
        // increase performance for small changes--> only calculate small portion of pathpoints?
        

        if (NavMesh.CalculatePath(startPoint, endPoint, NavMesh.AllAreas, navMeshPath) == true)
        {
            float currentDistance = 0f;
            float distanceStep;

            ClearPath();

            validPathPoints.Add(startPoint);

            for (int i = 1; i < navMeshPath.corners.Length; i++)
            {
                distanceStep = Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
                if (currentDistance + distanceStep <= maxDistance)
                {
                    currentDistance += distanceStep;
                    validPathPoints.Add(navMeshPath.corners[i]);
                }
                else
                {
                    currentDistance += distanceStep;
                    distanceStep = currentDistance - maxDistance;
                    validPathPoints.Add((navMeshPath.corners[i] - navMeshPath.corners[i]).normalized * distanceStep);
                    invalidPathPoints.Add((navMeshPath.corners[i] - navMeshPath.corners[i]).normalized * distanceStep);
                    invalidPathPoints.Add(navMeshPath.corners[i]);

                    if(i < navMeshPath.corners.Length)
                    {
                        for (int k = i + 1; k < navMeshPath.corners.Length; k++)
                        {
                            invalidPathPoints.Add(navMeshPath.corners[k]);
                        }
                    }
                    break;
                }
            }
        }
    }
    public bool IsPathValid()
    {
        return pathValid; //if first point in path distance to last point in path > maxdistance
    }

    // make sure displaypath() processed after calculatepath() in frame
    public void DisplayPath() 
    {
        //AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0.4f)
        //    , new Keyframe(arrowStartLength, 0.4f)
        //    , new Keyframe(arrowStartLength + 0.01f, 1f)
        //    , new Keyframe(1, 0f));
        //lineRenderer.widthCurve = curve;
        lineRenderer.widthMultiplier = width;
        lineRenderer.positionCount = validPathPoints.Count;
        lineRenderer.SetPositions(validPathPoints.ToArray());
    }

    //partially clear for performance?
    private void ClearPath() 
    {
        validPathPoints.Clear();
        invalidPathPoints.Clear();
    }

    public void EvaluateMousePosition(float mosuePosition) { }


    public void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, groundLayer))
            {
                CalculatePath(transform.position, hit.point, moveRange);
            }
            DisplayPath();
        }

      
    }
}
