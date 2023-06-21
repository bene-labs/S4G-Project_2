using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "Actions/Move", order = 1)]
public class Move : Action
{
    [Header("Move")]
    [SerializeField]
    private int range;

    public override void Perform()
    {
        base.Perform();
    }

    public override void Preview()
    {
        base.Preview();
    }
}
