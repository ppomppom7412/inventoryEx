using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : State {

    public override void Execute(Monster _mt)
    {
        _mt.Death();
    }

    public override void Enter(Monster _mt)
    {
        Debug.Log("죽어버립니다.");
        _mt.StopAllCoroutines();

        _mt.DrapTheItem();
    }

    public override void Exit(Monster _mt)
    {
        Debug.Log("죽음에서 헤어나옵니다.");
    }
}
