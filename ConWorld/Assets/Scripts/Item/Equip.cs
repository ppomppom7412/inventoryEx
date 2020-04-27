using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip :Item {
    public float attack;
    public float attackspeed;
    public float range;
    public int effect;
    public int usesp;

    protected override void init()
    {
    }

    public override void Activate()
    {
        if (!isActivate)
        {
            ChangeEquip();

            isActivate = true;
            //아이템 사용후 곡 소비확인해주자.
        }
    }

    public override void UnActivate()
    {
        if (isActivate)
        {
            //ChangeEquip();

            isActivate = false;
            //아이템 사용후 곡 소비확인해주자.
        }
    }

    public void ResetEquip()
    {
        //능력치증가 함수 부르기
        //player.SendMessage();

        Debug.Log("장비를 해제합니다:" + itemName);
    }

    public void ChangeEquip()
    {
        //능력치증가 함수 부르기
        //player.SendMessage();

        Debug.Log("장비를 장착합니다:" + itemName);
    }

    public void SetToeffect()
    {

    }
}
