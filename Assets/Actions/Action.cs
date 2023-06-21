using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : ScriptableObject
{
    [Header("Action")]
    [SerializeField]
    private string animationName = "!!!!REPLACE!!!!EinsEinsElf!ICHMAINESERNST";
    [SerializeField]
    private int uses = 0;

    private int MaxUses = 0;

    public void SetUp()
    {
        MaxUses = uses;
    }

    public void RestoreUse()
    {
        uses = Mathf.Clamp(uses + 1, 0, MaxUses);
    }

    public string GetAnimationName()
    {
        return animationName;
    }

    public virtual void Perform()
    {

    }

    public virtual void Preview()
    {

    }

    public void AfterPerform()
    {
        uses--;
    }

    private bool HasUsesLeft()
    {
        return uses > 0;
    }
}
