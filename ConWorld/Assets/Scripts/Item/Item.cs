using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    static public GameObject player;

    public string id;
    public int maxPackage;
    public string itemName;
    public string exp;
    public int itemCount;
    public int itemType;
    public bool isActivate;

    void Awake()
    {

    }

    void Start()
    {
        init();
    }

    void Update()
    {

    }

    protected virtual void init()
    {
        //itemSetting();
        id = "";
        isActivate = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Activate()
    {
        if (!isActivate)
        {
            Debug.Log("아이템 사용(" + itemName + ") : " + exp);

            isActivate = true;
            //아이템 사용후 곡 소비확인해주자.
        }
    }

    public virtual void UnActivate()
    {
        if (isActivate)
        {
            Debug.Log("아이템 사용중지(" + itemName + ") : " + exp);

            isActivate = false;
            //아이템 사용후 곡 소비확인해주자.
        }
    }

    //꽉찼는지 확인
    public bool ConfirmTheMaxPackage()
    {
        if (itemCount >= maxPackage)
        {
            //Debug.Log("최대묶음을 넘었습니다.");
            return false;
        }

        else
        {
            //Debug.Log("최대묶음을 넘지 않았습니다.");
            return true;
        }
    }

    //개수를 줄어주거나 줄어줄 개수가 없으면 아이템 삭제
    //삭제되면 true;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("눌림 : "+ eventData.pointerId);

        //베이스 무기 장착
        if (eventData.pointerId == -2)
        {
            GameObject currOb = eventData.pointerCurrentRaycast.gameObject;
            if (!currOb)
                return;

            if (itemType == (int)Slot.SlotType.Equip)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().AddEquit(this, gameObject);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().ShowItemExplainViewText(exp);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().ShowItemExplainViewText("");
    }
}
