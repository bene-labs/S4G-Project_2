using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ExperimentalMoveAction", menuName = "Actions/Experimental Move", order = 1)]
public class ExperimentalMove : Move
{
    [SerializeField]
    private NavMeshSurface rangeIndicator;

    public override void SetUp()
    {
        rangeIndicator = null;
        base.SetUp();
    }

    public override bool Perform(Unit caster)
    {
        caster.Move(GetTarget());
        return true;
    }

    public override void Preview(Unit caster)
    {
        //for testing purposes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeSelect();
            return;
        }

        //condition only there to emulate that the Select function would only be called once, before preview
        if(rangeIndicator == null)
        {
            Select(caster);
        }

        if (GetTarget() == new Vector3())
        {
            PathFinder.Instance.ClearPath();
            return;
        }

        PathFinder.Instance.CalculatePath(caster.transform.position, GetTarget(), Mathf.Infinity);
        PathFinder.Instance.RenderPath();

        base.Preview(caster);
    }

    private Vector3 GetTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity, groundLayer))
        {
            return hit.point;
        }

        return new Vector3();
    }

    public override void RestoreUse()
    {
        uses = maxUses;
    }

    private void Select(Unit caster) //ideally overwritten in action class
    {
        rangeIndicator = ActionRangeGenerator.instance.Generate(range, caster.transform.position + new Vector3(0, -caster.transform.position.y, 0));
        NavMeshVisualizer.instance.RenderNavMesh();
    }

    private void DeSelect() //ideally overwritten in action class
    {
        uses = 0;
        Destroy(rangeIndicator.gameObject);
        NavMeshVisualizer.instance.ClearNavMesh();
    }
}
