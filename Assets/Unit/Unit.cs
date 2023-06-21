using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    [Header("Action")]
    private Action[] availbleActions;
    [SerializeField] private Action selectedAction;

    public delegate void OnSelect();

    public OnSelect onSelected;

    [Header("Health")]
    [SerializeField] int maxHp = 10;
    private int currentHp;

    private NavMeshAgent m_navMeshAgent;

    [Header("Controls")]
    [SerializeField] private InputAction actionInput;
    
    private void Awake()
    {
        currentHp = maxHp;
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        actionInput.performed += PerformSelectedAction;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 targetPosition)
    {
        m_navMeshAgent.destination = targetPosition;
    }

    void Deselect()
    {

    }

    void AnimateAction(string actionName)
    {

    }

    void PerformSelectedAction(InputAction.CallbackContext callbackContext)
    {
        selectedAction.Perform(this);
    }

    void RestoreActions()
    {
        foreach (var action in availbleActions)
        {
            action.RestoreUse();
        }
    }

    void TakeDamage(int amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Unit '" + name + "' died!");
        Destroy(gameObject);
    }
}
