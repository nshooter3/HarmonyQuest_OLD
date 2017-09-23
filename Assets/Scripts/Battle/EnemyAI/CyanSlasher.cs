using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanSlasher : Enemy {

    //int idleTickLimit = 4;
    //int moveTickLimit = 4;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        enemyState = EnemyState.Idle;
        MyUpdateTick = IdleTick;
        StartTick();
    }
	
	// Update is called once per frame
	void Update () {
        switch (enemyState) {
            case EnemyState.Idle:
                IdleUpdate();
                break;
            case EnemyState.Move:
                MoveUpdate();
                break;
            case EnemyState.Attack:
                AttackUpdate();
                break;
        }
	}
    
    // ****************************************************
    // Logic that fires from the update loop, based on the enemy's current state
    // ****************************************************
    protected override void IdleUpdate()
    {

    }

    protected override void MoveUpdate()
    {
        //transform.position -= new Vector3(0,Time.deltaTime,0);
    }

    protected override void AttackUpdate()
    {

    }

    // ****************************************************
    // Logic that fires on a tick based update, based on what has been passed into the Tick delegate
    // ****************************************************
    private void IdleTick()
    {
        //print("idle tick");
        tick++;
        //if (tick >= idleTickLimit)
        //{
        //    EnterMoveState();
        //}
    }

    private void MoveTick()
    {
        //print("move tick");
        tick++;
        //if (tick >= idleTickLimit)
        //{
        //    EnterIdleState();
        //}
    }

    private void AttackTick()
    {
        tick++;
    }

    // ****************************************************
    // Logic for entering new states
    // ****************************************************
    private void EnterIdleState()
    {
        //print("Enter idle state");
        enemyState = EnemyState.Idle;
        MyUpdateTick = IdleTick;
        tick = 0;
    }

    private void EnterMoveState()
    {
        //print("Enter move state");
        enemyState = EnemyState.Move;
        MyUpdateTick = MoveTick;
        tick = 0;
    }

    private void EnterAttackState()
    {
        enemyState = EnemyState.Attack;
        MyUpdateTick = AttackTick;
        tick = 0;
    }
}
