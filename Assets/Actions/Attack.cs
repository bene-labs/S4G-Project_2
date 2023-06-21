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
}
