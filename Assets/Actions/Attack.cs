using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action
{
    [Header("Attack")]
    [SerializeField]
    private int damage;
    [SerializeField]
    private int healing;
    [SerializeField]
    private int range;

    public virtual List<float> GetTargets()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit) && hit.transform.tag != "Obstacle")
        {
            return null ; //new unit list
        }
        return null; //new empty unit list
    }
}
