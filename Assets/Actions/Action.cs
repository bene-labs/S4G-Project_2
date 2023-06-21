using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Action : ScriptableObject
{
    [Header("Action")]
    [SerializeField] protected string animationName = "!!!!REPLACE!!!!EinsEinsElf!ICHMAINESERNST";
    [SerializeField] protected int maxUses = 0;
    protected int uses = 0;
    public int Uses => uses;
    [SerializeField] protected float maxActionPoints;
    protected float availibleActionPoints;

    public virtual void SetUp()
    {
        uses = maxUses;
        availibleActionPoints = maxActionPoints;
    }

    public virtual void RestoreUse()
    {
        uses = Mathf.Clamp(uses + 1, 0, maxUses);
    }

    public string GetAnimationName()
    {
        return animationName;
    }

    public virtual bool Perform(Unit caster)
    {
        AfterPerform();
        return true;
    }

    public virtual void Preview(Unit caster)
    {

    }

    private void AfterPerform()
    {
        uses--;
    }

    public bool IsUsable()
    {
        return uses > 0 && availibleActionPoints > 0;
    }
}
