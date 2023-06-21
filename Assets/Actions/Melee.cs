using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAction", menuName = "Actions/Melee Attack", order = 1)]
public class Melee : Attack
{
    public override void Perform(Unit caster)
    {
        base.Perform(caster);
    }
}
