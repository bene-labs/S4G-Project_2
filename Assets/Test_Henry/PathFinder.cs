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
    private bool pathValid = false;
    private float pathDistance;

    private NavMeshPath navMeshPath;
    private LineRenderer lineRenderer;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;
    private Gradient colorGradient;

    [SerializeField] private float arrowSize;

    [SerializeField] private float width;
    [SerializeField] private float heightOffset;

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

        //lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        lineRenderer.material.SetColor("_Color", validColor);
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

            pathValid = true;
            ClearPath();

            validPathPoints.Add(navMeshPath.corners[0]);

            for (int i = 1; i < navMeshPath.corners.Length; i++)
            {
                distanceStep = Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
                if (currentDistance + distanceStep <= maxDistance)
                {
                    currentDistance += distanceStep;
                    validPathPoints.Add(navMeshPath.corners[i]);
                    pathDistance = currentDistance;
                }
                else
                {
                    pathValid = false;

                    currentDistance += distanceStep;
                    distanceStep = currentDistance - maxDistance;
                    validPathPoints.Add((navMeshPath.corners[i] - navMeshPath.corners[i-1]).normalized * distanceStep + navMeshPath.corners[i - 1]);
                    invalidPathPoints.Add((navMeshPath.corners[i] - navMeshPath.corners[i-1]).normalized * distanceStep + navMeshPath.corners[i - 1]);
                    invalidPathPoints.Add(navMeshPath.corners[i]);

                    currentDistance += distanceStep;

                    if(i+1 < navMeshPath.corners.Length)
                    {
                        for (int k = i + 1; k < navMeshPath.corners.Length; k++)
                        {
                            currentDistance += Vector3.Distance(navMeshPath.corners[k - 1], navMeshPath.corners[k]);
                            invalidPathPoints.Add(navMeshPath.corners[k]);
                        }
                    }
                    pathDistance = currentDistance;
                    break;
                }
            }
            if (validPathPoints.Count == 0)
                pathValid = false;
        }
    }
    public bool IsPathValid()
    {
        return pathValid; //if first point in path distance to last point in path > maxdistance
    }

    // make sure displaypath() processed after calculatepath() in frame
    public void RenderPath() 
    {
        lineRenderer.enabled = true;
        //AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0.4f)
        //    , new Keyframe(arrowStartLength, 0.4f)
        //    , new Keyframe(arrowStartLength + 0.01f, 1f)
        //    , new Keyframe(1, 0f));
        //lineRenderer.widthCurve = curve;

        //colorGradient = new Gradient();
        //colorGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(validColor, 0.0f), new GradientColorKey(validColor, ), new GradientColorKey(invalidColor, 1f), new GradientColorKey(invalidColor, 1f) },
        //                      new GradientAlphaKey[] { new GradientAlphaKey(1f, 1f), new GradientAlphaKey(1f, 1f) });
        //lineRenderer.colorGradient = colorGradient;

        lineRenderer.widthMultiplier = width;
        lineRenderer.positionCount = validPathPoints.Count;
        for (int i = 0; i < validPathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(validPathPoints[i].x, validPathPoints[i].y + heightOffset, validPathPoints[i].z));
        }
    }

    //partially clear for performance?
    public void ClearPath() 
    {
        lineRenderer.enabled = false;
        validPathPoints.Clear();
        invalidPathPoints.Clear();
    }

    public void EvaluateMousePosition(float mosuePosition) { }


    public void Update()
    {

    }
}
