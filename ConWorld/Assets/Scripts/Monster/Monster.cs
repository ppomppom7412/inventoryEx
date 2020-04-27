using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject playerOb;

    protected Rigidbody rigid;
    protected State m_currstate;

    public int wanderState;

    public string m_index;
    public string m_name;
    public int m_MaxHP;
    public int m_HP;
    public int m_attack;
    public float m_moveSpeed;
    public float m_attackSpeed;
    public int m_attackRange;
    public int m_resistance;

    ///*---------------------------------------------------*///

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();

        if (rigid == null)
        {
            Debug.Log("Monster Rigidbody 할당 실패");
        }

        playerOb = GameObject.FindGameObjectWithTag("Player");

        if (playerOb == null)
        {
            Debug.Log("Monster playerOb 할당 실패");
        }

        Init();
    }

    //초기화 시키다
    public virtual void Init() { }

    //매 프레임당 불러줄 것.
    public virtual void mUpdate() { }

    void Update()
    {
        mUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BeAttacked(30);
        }
    }

    ///*---------------------------------------------------*///

    //상태가 변하다
    public void ChangeState(State _newState)
    {
        m_currstate.Exit(this);
        m_currstate = _newState;
        m_currstate.Enter(this);
    }

    ///*---------------------------------------------------*///

    //점프조건 체크
    public IEnumerator CheckInJump()
    {
        while (m_HP > 0)
        {
            RaycastHit hit;
            Vector3 rayPoint = transform.position + (Vector3.forward * 1.5f);

            Debug.DrawRay(transform.position, transform.forward, Color.cyan);

            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
            {
                if (hit.collider.gameObject.layer == 11)
                {
                    rigid.velocity = rigid.velocity + Vector3.up * 8.0f;
                    Debug.Log("Jump!");

                    yield return new WaitForSeconds(2.0f);
                }
            }
            yield return 0;
        }
    }

    //거리체크
    public float DistanceForMonsterToPlayer()
    {
        return Vector3.Distance(gameObject.transform.position, playerOb.transform.position);
    }

    //시야각 체크
    public bool CheckInPlayer()
    {
        //while (m_HP>0) {
        RaycastHit hit;
        Vector3 rayDir = playerOb.transform.position - transform.position;

        //거리범위 검사
        if (DistanceForMonsterToPlayer() >= 10.0f)
            return false;

        //강제인식범위 검사
        if (DistanceForMonsterToPlayer() < 3.0f)
            return true;

        if ((Vector3.Angle(rayDir, transform.forward)) > m_attackRange * 3)
            return false;

        Debug.DrawLine(transform.position, transform.position + (transform.forward * m_attackRange * 3), Color.green);

        if (Physics.Raycast(transform.position, rayDir, out hit, 10.0f))
        {
            if (hit.collider.tag == "Player")
            {

                //Debug.Log("Find player!");

                return true;
            }
        }
        return false;
    }

    //공격범위 체크
    public bool CheckInAttack()
    {
        if (CheckInPlayer() && (DistanceForMonsterToPlayer() <= m_attackRange * 3))
            return true;

        return false;
    }

    //공격받다
    public virtual void BeAttacked(int _demage)
    {
        if (m_HP > 0)
        {
            m_HP -= _demage;
            //모션함께
        }
    }

    ///*---------------------------------------------------*///

    //공격하다
    public virtual void Attack() { }

    //특수공격하다
    public virtual void SpecialAttack() { }

    //방황하다
    public virtual void Wander() { }

    //추격하다
    public virtual void Chase() { }

    //대기하다
    public virtual void WaitForAction() { }

    //죽다
    public virtual void Death()
    {
    }

    //드랍아이템
    public virtual void DrapTheItem()
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
