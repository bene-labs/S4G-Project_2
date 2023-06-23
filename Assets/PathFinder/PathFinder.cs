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
    private float validPathPercentage;
    private float totalDistance;

    private NavMeshPath navMeshPath;
    private LineRenderer lineRenderer;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float heightOffset;

    [Header("Path Display")]
    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;
    private Gradient colorGradient;

    private Material material;

    [SerializeField] private float arrowSize;

    [SerializeField] private float lineWidth;
    [SerializeField] private float segmentDensity;
    // range : [0,1]
    [SerializeField] private float segmentThickness;
    [SerializeField] private float speed;


    private void Awake()
    {
        colorGradient = new Gradient();
        navMeshPath = new NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();

        //colorGradient.mode = GradientMode.Fixed;
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Singleton pathfinder is already initialised!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        lineRenderer.material.SetFloat("_segmentThickness", segmentThickness);
        lineRenderer.material.SetFloat("_speed", speed);


    }

    public void CalculatePath(Vector3 startPoint, Vector3 endPoint, float maxDistance)
    {
        // validpathPoints = navmesh(startpoint, newEndpoint)
        // path creating performance intensive for long range; max range to calculate path?! maybe 10times the move radius?
        // consider case for no valid path?
        // increase performance for small changes--> only calculate small portion of pathpoints?


        if (NavMesh.CalculatePath(startPoint, endPoint, NavMesh.AllAreas, navMeshPath) == false)
            return;

        totalDistance = 0f;
        float distanceStep = 0f;

        ClearPath();

        validPathPoints.Add(navMeshPath.corners[0]);
        validPathPercentage = 1f;
        lineRenderer.material.SetColor("_color", validColor);

        for (int i = 1; i < navMeshPath.corners.Length; i++)
        {
            distanceStep = Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
            if (totalDistance + distanceStep <= maxDistance)
            {
                totalDistance += distanceStep;
                validPathPoints.Add(navMeshPath.corners[i]);
            }
            else
            {
                distanceStep = maxDistance - totalDistance;
                    
                validPathPoints.Add((navMeshPath.corners[i] - navMeshPath.corners[i-1]).normalized * distanceStep + navMeshPath.corners[i - 1]);
                invalidPathPoints.Add((navMeshPath.corners[i] - navMeshPath.corners[i-1]).normalized * distanceStep + navMeshPath.corners[i - 1]);
                invalidPathPoints.Add(navMeshPath.corners[i]);

                totalDistance += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);

                if(i+1 < navMeshPath.corners.Length)
                {
                    for (int j = i + 1; j < navMeshPath.corners.Length; j++)
                    {
                        totalDistance += Vector3.Distance(navMeshPath.corners[j - 1], navMeshPath.corners[j]);
                        invalidPathPoints.Add(navMeshPath.corners[j]);
                    }
                }
                validPathPercentage = maxDistance/totalDistance;
                lineRenderer.material.SetColor("_color", invalidColor);
                break;
            }
        }
    }
    public bool IsPathValid()
    {
        return validPathPoints.Count > 0 && invalidPathPoints.Count == 0; //if first point in path distance to last point in path > maxdistance
    }

    public void RenderPath() 
    {
        lineRenderer.enabled = true;
        lineRenderer.widthMultiplier = lineWidth;

        lineRenderer.material.SetFloat("_segmentDensity", segmentDensity * totalDistance * -1f);

        //AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0.4f)
        //    , new Keyframe(arrowStartLength, 0.4f)
        //    , new Keyframe(arrowStartLength + 0.01f, 1f)
        //    , new Keyframe(1, 0f));
        //lineRenderer.widthCurve = curve;

        if (IsPathValid())
        {
            lineRenderer.positionCount = validPathPoints.Count;
            for (int i = 0; i < validPathPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(validPathPoints[i].x, validPathPoints[i].y + heightOffset, validPathPoints[i].z));
            }
            colorGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(validColor, 0.0f), new GradientColorKey(validColor, 1f)},
                                  new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
        }
        else if(validPathPoints.Count != 0)
        {
            lineRenderer.positionCount = validPathPoints.Count + invalidPathPoints.Count - 1;
            for (int i = 0; i < validPathPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(validPathPoints[i].x, validPathPoints[i].y + heightOffset, validPathPoints[i].z));
            }
            for (int i = validPathPoints.Count; i < validPathPoints.Count + invalidPathPoints.Count - 1; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(invalidPathPoints[i-validPathPoints.Count+1].x, invalidPathPoints[i-validPathPoints.Count+1].y + heightOffset, 
                                                        invalidPathPoints[i-validPathPoints.Count+1].z));
            }
            colorGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(validColor, 0.0f), new GradientColorKey(validColor, validPathPercentage), 
                                                           new GradientColorKey(invalidColor, validPathPercentage + 0.02f), new GradientColorKey(invalidColor, 1f) },
                                  new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, validPathPercentage), 
                                                           new GradientAlphaKey(1f, validPathPercentage + 0.02f), new GradientAlphaKey(1f, 1f) });
        }
        lineRenderer.colorGradient = colorGradient;
    }

    // todo: improve performance by partial clear
    public void ClearPath() 
    {
        lineRenderer.enabled = false;
        validPathPoints.Clear();
        invalidPathPoints.Clear();
    }

    public void EvaluateMousePosition(float mosuePosition) { }

    private void SetShader(float pathLength)
    {
    }


    public void Update()
    {

    }
}
