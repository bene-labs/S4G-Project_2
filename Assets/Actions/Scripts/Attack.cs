using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action
{
    protected enum AttackType { MELEE, RANGE}

    [Header("Attack")]
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    private int manaCost;

    [Header(" ")]

    [SerializeField]
    private int minDamage;
    [SerializeField]
    private int maxDamage;
    [SerializeField]
    private int critDamage;

    [Header(" ")]

    [SerializeField]
    private int range;
    [SerializeField]
    private int hitChance;

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
