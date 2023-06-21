using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    [Header("Action")]
    [SerializeField] private List<Action> availableActions;
    [SerializeField] private Action selectedAction;

    public delegate void OnSelect();

    public OnSelect onSelected;

    [Header("Health")]
    [SerializeField] int maxHp = 10;
    private int currentHp;

    private NavMeshAgent m_navMeshAgent;

    [Header("Controls")]
    [SerializeField] private InputAction actionInput;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI selectedActionText;
    [SerializeField] private TextMeshProUGUI remainingUsesText;
    
    private void Awake()
    {
        availableActions = new List<Action>();
        currentHp = maxHp;
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        selectedAction.SetUp();
        if (!availableActions.Contains(selectedAction))
            availableActions.Add(selectedAction);
    }

    private void Start()
    {
        
    }

    public void SelectedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformSelectedAction();
        }

        selectedAction.Preview(this);
    }

    public float Move(Vector3 targetPosition)
    {
        m_navMeshAgent.destination = targetPosition;
        return Vector3.Distance(m_navMeshAgent.transform.position, targetPosition);
    }

    public void Deselect()
    {
        selectedActionText.enabled = false;
        remainingUsesText.enabled = false;
    }

    public void Select()
    {
        selectedActionText.enabled = true;
        remainingUsesText.enabled = true;
    }
    
    private void AnimateAction(string actionName)
    {

    }

    private bool PerformSelectedAction()
    {

        if (selectedAction.Perform(this))
        {
            remainingUsesText.text = "Uses left: " + selectedAction.Uses;
            return true;
        }
        return false;
    }

    public void RestoreActions()
    {
        foreach (var action in availableActions)
        {
            action.RestoreUse();
        }
        if (selectedAction != null)
            remainingUsesText.text = "Uses left: " + selectedAction.Uses;
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            currentHp = 0;
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
        foreach (var action in availableActions)
        {
            if (action.IsUsable())
                return true;
        }
        return false;
    }
}
