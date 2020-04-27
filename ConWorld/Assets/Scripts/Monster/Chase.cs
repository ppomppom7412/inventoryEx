using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State {

    public override void Execute(Monster _mt)
    {
        _mt.Chase();

        //거리가 멀면 > 방황하기
        if (_mt.DistanceForMonsterToPlayer() > 10.0f)
        {
            _mt.ChangeState(new Wander());
        }

        //일정 거리 안으로 오면 > 공격하기
        if (_mt.CheckInAttack())
        {
            _mt.ChangeState(new Attack());
        }

        if (_mt.m_HP <= 0)
        {
            _mt.ChangeState(new Death());
        }
    }

    public override void Enter(Monster _mt)
    {
        Debug.Log("추격하기에 들어갑니다.");
        _mt.StartCoroutine(_mt.CheckInJump());
    }

    public override void Exit(Monster _mt)
    {
        Debug.Log("추격하기에 나갑니다.");
        _mt.StopCoroutine(_mt.CheckInJump());
    }

}
