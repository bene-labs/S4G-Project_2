using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action
{
    [Header("Attack")]
    [SerializeField]
    private int damage;
    [SerializeField]
    private int range;

    protected virtual List<Unit> GetTargets()
    {
        //default target is unit under the mouse cursor

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit) && hit.transform.tag != "Obstacle")
        {
            Unit target = hit.transform.GetComponent<Unit>();
            if(target != null)
            {
                return new List<Unit>() { target };
            }
        }

        return null;
    }
}
