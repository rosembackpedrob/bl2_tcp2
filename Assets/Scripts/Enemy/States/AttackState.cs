using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    
    public override void Enter()
    {
    }

    public override void Perform()
    {
        Follow();
    }

    public override void Exit()
    {
        enemy.stateMachine.patrolState.Perform();
    }

    public void Follow()
    {
        //implementar lógica de seguir
        enemy.Agent.SetDestination(enemy.player.position);
    }
}
