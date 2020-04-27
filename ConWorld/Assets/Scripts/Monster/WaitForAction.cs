using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAction : State {
    private float timer = 0.0f;
    private bool timeSwitch = false;
    public override void Execute(Monster _mt)
    {
        if (!timeSwitch)
        {
            _mt.WaitForAction();
            timeSwitch = true;
            _mt.StartCoroutine(waitActionTime());
        }

        //일정 거리 안으로 오면 > 추격
        if (_mt.CheckInPlayer())
        {
            timer = 0.0f;
            timeSwitch = false;
            _mt.ChangeState(new Chase());
        }

        //일정 시간 지나면 > 방황하기
        if (timer > 7.0f)
        {
            timer = 0.0f;
            timeSwitch = false;
            _mt.ChangeState(new Wander());
        }

        if (_mt.m_HP <= 0)
        {
            _mt.ChangeState(new Death());
        }

        _mt.WaitForAction();
    }

    public override void Enter(Monster _mt)
    {
        //Debug.Log("대기하기에 들어갑니다.");
    }

    public override void Exit(Monster _mt)
    {
        //Debug.Log("대기하기에 나갑니다.");
    }

    private IEnumerator waitActionTime()
    {
        if (timeSwitch) {
            yield return new WaitForSeconds(8.0f);
            timer = 8.0f;
          }
    }
}
