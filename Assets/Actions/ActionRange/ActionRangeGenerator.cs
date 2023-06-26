using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ActionRangeGenerator : MonoBehaviour
{
    public static ActionRangeGenerator instance;

    [SerializeField]
    private NavMeshSurface rangeTemplate;

    private void Awake()
    {
        instance = this;
    }

    public NavMeshSurface Generate(float radius, Vector3 position)
    {
        NavMeshSurface result = Instantiate(rangeTemplate);

        result.transform.position = position;
        result.transform.localScale = new Vector3(radius *2, 0.01f, radius * 2);

        result.RemoveData();
        result.BuildNavMesh();

        return result;
    }
}
