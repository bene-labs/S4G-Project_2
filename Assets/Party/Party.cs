using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Party : MonoBehaviour
{
    private List<Unit> units = new List<Unit>();
    private Unit selectedUnit;

    [SerializeField] private TextMeshProUGUI activeText;
    
    private void Awake()
    {
        foreach (var unit in transform.GetComponentsInChildren<Unit>())
        {
            units.Add(unit);
        }
    }

    public void RestoreAllActions()
    {
        foreach (var unit in units)
        {
            unit.RestoreActions();
        }
    }

    public void SetActive()
    {
        activeText.text = "Your turn";
        RestoreAllActions();
        
        // todo: remove
        units[0].Select();
        selectedUnit = units[0];
    }

    public void SetInactive()
    {
        activeText.text = "Not your turn";
        selectedUnit.Deselect();
        selectedUnit = null;
    }
    
    public void ActiveUpdate()
    {
        if (selectedUnit != null)
            selectedUnit.SelectedUpdate();
    }
    
    public bool HasUsableUnits()
    {
        foreach (var unit in units)
        {
            if (unit.HasUsableAction())
                return true;
        }
        return false;
    }
}
