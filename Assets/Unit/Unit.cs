using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    private Action[] availbleActions;
    [SerializeField] private Action selectedAction;

    public delegate void OnSelect();

    public OnSelect onSelected;

    [SerializeField] int maxHp = 10;
    private int currentHp;

    private NavMeshAgent m_navMeshAgent;
    
    private void Awake()
    {
        currentHp = maxHp;
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move(Vector3 targetPosition)
    {
        m_navMeshAgent.destination = targetPosition;
    }

    void Deselect()
    {

    }

    void AnimateAction(string actionName)
    {

    }

    void PerformSelectedAction()
    {
        selectedAction.Perform();
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

    }

    void Die()
    {
        Debug.Log("Unit '" + name + "' died!");
        Destroy(gameObject);
    }
}
