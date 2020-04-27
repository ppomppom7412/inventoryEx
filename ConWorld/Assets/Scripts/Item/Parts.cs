using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : Item {
    public bool IsUse;
    public int IsIntinite;

    protected override void init()
    {
        IsUse = false;
    }

    public override void Activate()
    {
        if (!IsUse)
        {
            ReleaseParts();
        }
        else
        {
            SettingParts();
            IsUse = true;
        }
    }

    //장착
    public void SettingParts()
    {
        Debug.Log("파츠 장착");
    }

    //해제
    public void ReleaseParts()
    {
        if (IsIntinite==0)
        {
            //파츠 파괴
            Debug.Log("파츠 파괴");
            Destroy(this.gameObject);
        }
        else
        {
            //파츠 유지
        }
    }

}
