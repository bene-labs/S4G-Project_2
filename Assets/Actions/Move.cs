using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem.HID;

[CreateAssetMenu(fileName = "MoveAction", menuName = "Actions/Move", order = 1)]
public class Move : Action
{
    [Header("Move")]
    [SerializeField]
    private int range;

    public override void Perform(Unit caster)
    {
        caster.Move(GetTarget());

        base.Perform(caster);
    }

    public override void Preview(Unit caster)
    {
        if (GetTarget() == new Vector3())
        {
            PathFinder.Instance.ClearPath();
            return;
        }

        PathFinder.Instance.CalculatePath(caster.transform.position, GetTarget(), range);
        PathFinder.Instance.RenderPath();
        base.Preview(caster);
    }

    private Vector3 GetTarget()
    {
        //get point that's under the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.transform.tag != "Obstacle")
        {
            return hit.point;
        }

        return new Vector3();
    }
}
