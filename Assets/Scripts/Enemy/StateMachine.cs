using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public AttackState attackState;

    [SerializeField] private float distance;

    public void Initialise()
    {
        //estado padrão: patrulha.
        patrolState = new PatrolState();
        attackState = new AttackState();
        ChangeState(patrolState);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
         
        
    }

    public void ChangeState(BaseState newState)
    {
        //checar activeState != null
        if(activeState != null)
        {
            //limpar e mudar o activeState.
            activeState.Exit();

        }
        //mudar para novo estado.
        activeState = newState;

        if(activeState != null) 
        {
            //Novo estado.
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
