using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeAction", menuName = "Actions/Range Attack", order = 1)]
public class Range : Attack
{
    [Header("Range")]
    [SerializeField]
    private GameObject projectile;

    public override bool Perform(Unit caster)
    {
        return base.Perform(caster);
    }
}
