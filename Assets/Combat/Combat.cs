using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public static Combat Instance;
    
    public int turn = 0;

    private List<Party> parties;
    private Party activeParty;
    private int activePartyIndex = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI turnText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        parties = new List<Party>();
        foreach (var party in GetComponentsInChildren<Party>())
        {
            if (party.gameObject.activeInHierarchy && party.enabled)
                parties.Add(party);
        }
        StartNextTurn();
    }

    private void Update()
    {
        if (activeParty != null)
        {
            activeParty.ActiveUpdate();
            if (!activeParty.HasUsableUnits())
                EndTurn();
        }
    }

    private void StartNextTurn()
    {
        if (turn > 0 && parties.Count > 1)
        {
            activePartyIndex++;
            if (activePartyIndex >= parties.Count)
                activePartyIndex = 0;
        }
        activeParty = parties[activePartyIndex];
        activeParty.SetActive();
        turn++;
        turnText.text = "Turn " + turn;
    }

    public void EndTurn()
    {
        activeParty.SetInactive();
        StartNextTurn();
    }

    public void Win()
    {
        
    }

    public void Lose()
    {
        
    }
    
}
