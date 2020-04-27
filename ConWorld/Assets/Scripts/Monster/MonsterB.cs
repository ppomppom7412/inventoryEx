using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterB : Monster {
    public Animation anim;

    public MonsterB() //(MonsterDB _monserDB) : base(_monserDB)
    { }

    //초기화 시키다
    public override void Init()
    {
        anim = GetComponent<Animation>();

        m_HP = m_MaxHP;
        m_currstate = new WaitForAction();
    }

    //매 프레임당 불러줄 것.
    public override void mUpdate()
    {
        m_currstate.Execute(gameObject.GetComponent<Monster>());
    }

    //공격받다
    public override void BeAttacked(int _demage)
    {
        if (m_HP > 0)
        {
            m_HP -= _demage;
            Debug.Log(m_name + " HP = " + m_HP);
            //모션함께
            anim.Play("R_hurt");
        }
        else
        {
            m_HP = 0;
        }
    }

    //공격하다
    public override void Attack()
    {
        Debug.Log(m_name + "가 공격합니다.");
        anim.Play("Attack");
    }

    //특수공격하다
    public override void SpecialAttack()
    {
        Debug.Log(m_name + "가 특수공격합니다.");
        anim.Play("Fenzied attack");
    }

    //방황하다
    public override void Wander()
    {
        //Debug.Log(m_name + "가 방황합니다.");

        transform.position = Vector3.Lerp(transform.position, transform.position +(transform.forward * m_moveSpeed), Time.deltaTime);
        //transform.Translate(transform.forward * m_moveSpeed);

        //축고정
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        anim.Play("Walk");
    }

    //추격하다
    public override void Chase()
    {
        //Debug.Log(m_name + "가 추격합니다.");

        //단순 추격
        if (Vector3.Distance(transform.position, playerOb.transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, playerOb.transform.position + (transform.forward * m_moveSpeed), Time.deltaTime );
        }

        transform.LookAt(playerOb.transform);

        //축고정
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,0);
        anim.Play("Run");

    }

    //대기하다
    public override void WaitForAction()
    {
        StopCoroutine(CheckInJump());
        anim.Play("Idle");
    }

    //죽다
    public override void Death()
    {
        //애니메이션이 생길시 지워주자.

        anim.Play("Die");

        Destroy(gameObject, 1.0f);

    }

    public override void DrapTheItem()
    {
        //데이터 덩어리
        DrapItem setdrap = MonsterManager.Instance.FindToDrapItem(m_index);

        if (setdrap.prefab != null)
        {

            GameObject ob = Instantiate(setdrap.prefab,
                            gameObject.transform.position + (Vector3.up * 0.5f),
                            gameObject.transform.rotation);

            DrapItem mydrapitem = ob.AddComponent<DrapItem>();
            mydrapitem.Setting(setdrap);

            if (mydrapitem != null)
            {
                mydrapitem.StartCoroutine(mydrapitem.DeleteTimer());
                mydrapitem.StartCoroutine(mydrapitem.DrapItemUPAndDown());
            }

            setdrap.prefab = null;
        }
    }

}
