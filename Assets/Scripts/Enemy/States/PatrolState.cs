using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;

    private float distance = 10f;
    
    public override void Enter()
    {
    }

    public override void Perform()
    {
        PatrolCycle();

    }

    public override void Exit()
    {
    }

    public void PatrolCycle()
    {
        
        //implementar lógica de patrulha.
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > 2) 
            {
                if(waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0f;
            }    
        }
        if(enemy.dist < distance)
        {
            enemy.Agent.SetDestination(enemy.player.position);
            if(enemy.dist <= 0.4f)
            {
                enemy.Agent.isStopped = true;
            }
                
        }
    }
}
