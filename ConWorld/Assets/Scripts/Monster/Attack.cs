using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State {
    private bool attackSwitch = false;

    public override void Execute(Monster _mt)
    {
        //너무 멀면 되돌아간다.
        if (_mt.DistanceForMonsterToPlayer() > 10.0f)
            _mt.ChangeState(new Wander());

        //추격범위 안이면
        if (_mt.CheckInPlayer() && (!_mt.CheckInAttack()))
        {
            _mt.ChangeState(new Chase());
        }

        if (_mt.m_HP <= 0)
        {
            _mt.ChangeState(new Death());
        }

        if (!attackSwitch)
        {
            _mt.Attack();

            _mt.StartCoroutine(CallNextState(_mt));
        }
    }

    public override void Enter(Monster _mt)
    {
        Debug.Log("공격하기에 들어갑니다.");
    }

    public override void Exit(Monster _mt)
    {
        Debug.Log("공격하기에 나갑니다.");
    }

    public void ResetAttackSwitch()
    {
        attackSwitch = false;
    }
    
    //패턴용
    public IEnumerator CallNextState(Monster _mt)
    {
        attackSwitch = true;

        yield return new WaitForSeconds(0.5f);

        ResetAttackSwitch();
    }
}
