using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CyanSlasher : Enemy {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        enemyState = EnemyState.Idle;
    }

    void Update()
    {
        //If the enemy isn't currently carrying out a task, select a new action
        if (!isPerformingAction)
        {
            //Select next action based on the state the enemy is exiting
            switch (enemyState) {
                case EnemyState.Idle:
                    //TODO: stuff
                    break;
                case EnemyState.Move:
                    //TODO: stuff
                    break;
                case EnemyState.Attack:
                    //TODO: stuff
                    break;
            }
        }
    }

    // ****************************************************
    // Logic for entering new states
    // ****************************************************
    private void EnterIdleState()
    {
        enemyState = EnemyState.Idle;
        isPerformingAction = true;
    }

    private void EnterMoveState()
    {
        enemyState = EnemyState.Move;
        isPerformingAction = true;
    }

    private void EnterAttackState()
    {
        enemyState = EnemyState.Attack;
        isPerformingAction = true;
    }

    //*** *** *** Behaviors *** *** ***

        //Wait for a period, then check for next behavior
        IEnumerator Wait(float seconds)
        {
            EnterIdleState();
            yield return new WaitForSeconds(seconds);
            isPerformingAction = false;
        }

        //Move to a new location from current location, then check for next behavior
        IEnumerator TimedMovement(Vector2 destination, float duration)
        {
            EnterMoveState();
            Vector2 start = transform.position;
            yield return new WaitForSeconds(duration);
            isPerformingAction = false;
        }

    //*** *** *** End Behaviors *** *** ***
}
