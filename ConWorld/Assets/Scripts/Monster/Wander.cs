using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : State {
    private bool randomSwitch = false;
    private float timer = 0.0f;
    private int randomAng = 0;

    public override void Execute(Monster _mt)
    {
        _mt.Wander();

        _mt.transform.eulerAngles = Vector3.Slerp(_mt.transform.eulerAngles, _mt.transform.eulerAngles + Vector3.up * randomAng, Time.deltaTime);

        if (!randomSwitch)
        {
            randomSwitch = true;
            _mt.StartCoroutine(TimerForSetRandomRotate());
        }

        //시야안으로 오면 or 강제인식범위 > 추격하기
        if (_mt.CheckInPlayer())
        {
            _mt.ChangeState(new Chase());
        }

        //일정시간 지나면 > 대기
        if (timer > 5.0f)
        {
            timer = 0.0f;
            _mt.ChangeState(new WaitForAction());
        }

        if (_mt.m_HP <= 0)
        {
            _mt.ChangeState(new Death());
        }
    }

    public override void Enter(Monster _mt)
    {
        //Debug.Log("방황하기에 들어갑니다.");
        _mt.StartCoroutine(_mt.CheckInJump());
    }

    public override void Exit(Monster _mt)
    {
        //Debug.Log("방황하기에 나갑니다.");
        _mt.StopCoroutine(_mt.CheckInJump());
    }

    //랜덤한 시간안에 회전각주기
    IEnumerator TimerForSetRandomRotate()
    {
        if (randomSwitch)
        {
            //랜덤각
            randomAng = Random.Range(-30, 30);
            //랜덤시간
            int randomSec = Random.Range(0, 2);

            yield return new WaitForSeconds(randomSec);

            //Debug.Log("new random value : " + randomAng);
            timer += randomSec;
            randomSwitch = false;
        }
    }
}
