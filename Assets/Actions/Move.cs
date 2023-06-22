using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "Actions/Move", order = 1)]
public class Move : Action
{
    [Header("Move")]
    [SerializeField] private int range;
    [SerializeField] LayerMask groundLayer;
    private float maxCharges;
    private float charges;

    public override void SetUp()
    {
        base.SetUp();
        maxCharges = range;
        charges = maxCharges;
        name = "Move";
    }

    public override bool Perform(Unit caster)
    {
        if (isResolving)
            return false;

        if (PathFinder.Instance.IsPathValid())
        {
            charges -= caster.Move(GetTarget());
            isResolving = true;
        } else
            return false;
        // todo: rework
        //base.Perform(caster);
        return true;
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
}
