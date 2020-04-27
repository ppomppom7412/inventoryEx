using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrapItem : MonoBehaviour {
    public string drapitemID;
    public string drapMonsterID;
    public float drapProb;
    public GameObject prefab;
    public int order;
    public string presentQ;
    public string preogressQ;

    void Start()
    {
        //StartCoroutine(DeleteTimer());
    }

    public bool ConfirmTheDrapMonster(string _monsterID)
    {
        if (_monsterID == drapMonsterID)
            return true;

        else
            return false;

    }

    public bool CheckPresent()
    {
        if (presentQ == "")
            return true;

        //_qID = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().CheckQ
        //if ( == presentQ)
        //    return true;

        return false;
    }

    public bool ProbabilityDrap()
    {
        int random = Random.Range(1, 100);

        if (random <= drapProb*100)
        {
            return true;
        }

        else
            return false;
    }

    public string GetDrapItemID()
    {
        return drapitemID;
    }

    public void Setting(DrapItem _drapi)
    {
        drapitemID = _drapi.drapitemID;
        drapMonsterID = _drapi.drapMonsterID;
        drapProb = _drapi.drapProb;
        prefab = _drapi.prefab;
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            //인벤토리 부르기
            Debug.Log("아이템 추가됨");

            if (coll.gameObject.GetComponent<Inventory>().AddItem(drapitemID))
            {
                Destroy(gameObject);
            }
            //GameObject.Find("ContentView").GetComponent<Inventory>().AddItem(drapitemID);
        }
    }

    public IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(30.0f);
        Debug.Log("아이템 사라짐");
        Destroy(gameObject);
    }

    public IEnumerator DrapItemUPAndDown()
    {
        bool upanddown = true;
        Vector3 currPosition = gameObject.transform.position;

        while (true)
        {
            if (upanddown)
            {
                gameObject.transform.Translate(0,0.01f,0);
                //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, currPosition + (Vector3.up * 1.2f), Time.deltaTime);

                if (currPosition.y + 1.0f <= gameObject.transform.position.y)
                {
                    upanddown = false;
                }
            }
            else
            {
                gameObject.transform.Translate(0, -0.01f, 0);
                //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, currPosition + (Vector3.up * -0.2f), Time.deltaTime);

                if (currPosition.y  > gameObject.transform.position.y)
                {
                    upanddown = true;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

    }

}
