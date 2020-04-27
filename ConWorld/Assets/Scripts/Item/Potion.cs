using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion :Item {
    public int hpHeal;
    public int spHeal;
    public string effect;

    protected override void init()
    {
        itemCount = 1;

    }

    public override void Activate()
    {
        Healing(hpHeal, spHeal);
        ConsumeToItem();
    }

    public void Healing(int _hp, int _sp)
    {
        //플레이어의 회복 함수부르기
        //player.SendMessage();
        Debug.Log("포션을 마십니다:" + itemName + "+HP"+_hp+" +SP"+_sp);
    }

    public void ConsumeToItem()
    {
            if (itemCount > 1)
            {
                itemCount--;
            }
            else if (itemCount <= 1)
            {
                    Destroy(gameObject);
            }

    }

    public void SetToeffect()
    {

    }
}
