using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    [Header("Action")]
    [SerializeField] private List<Action> actions;
    private Action selectedAction;
    
    public delegate void OnSelect();

    public OnSelect onSelected;

    // todo: move to seperate class
    [Header("Health")]
    [SerializeField] int maxHp = 10;
    private int currentHp;
   
    // todo: encapsulate UI
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI selectedActionText;
    [SerializeField] private TextMeshProUGUI remainingUsesText;
    
    private NavMeshAgent _navMeshAgent;
    private NavMeshObstacle _navMeshObstacle;
    
    private void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        currentHp = maxHp;

        if (actions.Count == 0)
        {
            Debug.LogError("Assignment Error: Unit '" + gameObject.name + "' has no actions!");
            return;
        }

        selectedAction = actions[0];
        selectedAction.SetUp();
    }

    private void Start()
    {
        
    }

    public void SelectedUpdate()
    {
        // todo: rework
        if (selectedAction.Locked && selectedAction is Move)
        {
            if (_navMeshAgent.remainingDistance <= 0.01f)
            {
                selectedAction.Locked = false;
                remainingUsesText.text = "Uses Left: " + selectedAction.Uses;
            }
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            PerformSelectedAction();
        }

        selectedAction.Preview(this);
    }

    public float Move(Vector3 targetPosition)
    {
        _navMeshAgent.destination = targetPosition;
        return Vector3.Distance(_navMeshAgent.transform.position, targetPosition);
    }

    public void Deselect()
    {
        selectedActionText.enabled = false;
        remainingUsesText.enabled = false;
        _navMeshAgent.enabled = false;
        _navMeshObstacle.enabled = true;
    }

    public void Select()
    {
        selectedActionText.enabled = true;
        remainingUsesText.enabled = true;
        _navMeshAgent.enabled = true;
        _navMeshObstacle.enabled = false;
    }
    
    private void AnimateAction(string actionName)
    {

    }

    private bool PerformSelectedAction()
    {

        if (selectedAction.Perform(this))
        {
            //remainingUsesText.text = "Uses left: " + selectedAction.Uses;
            return true;
        }
        return false;
    }

    public void RestoreActions()
    {
        foreach (var action in actions)
        {
            action.RestoreUse();
        }
        if (selectedAction != null)
            remainingUsesText.text = "Uses left: " + selectedAction.Uses;
    }

    public void TakeDamage(int amount)
    {
        currentHp = Mathf.Clamp(currentHp - amount, 0, maxHp);
        if (currentHp == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Unit '" + name + "' died!");
        Destroy(gameObject);
    }

    public bool HasUsableAction()
    {
        foreach (var action in actions)
        {
            if (action.IsUsable())
                return true;
        }
        return false;
    }
}
