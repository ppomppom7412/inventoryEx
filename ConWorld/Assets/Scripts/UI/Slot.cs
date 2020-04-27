using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler{
    [SerializeField]
    public enum SlotType {AllItem,Equip,Parts,Potion,Skill,Craft,Etc,None };
    [SerializeField]
    public enum ActionType { Equit, Consume, Skill,Craft, None };
    public int type;
    public int action;
    public string setKey;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void Start()
    {
        //StartCoroutine(QuikPress());
    }

    public void Update()
    { 
        if ((setKey == "") || (!item))
        {

        }

        else
        {
            if (Input.GetKeyDown(setKey))
            {
                item.GetComponent<Item>().Activate();
            }

        }//end  else

        //EquitSlotAction();
    }

    #region IdropHandler implementation
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.Darggeditem.GetComponent<Item>() != null)
        {
            //SettingSlotAction();

            //슬롯이 비어있고 슬롯타입에 맞는 아이템이 왔을때
            if (!item && ((DragHandler.Darggeditem.GetComponent<Item>().itemType == type) || (type == (int)SlotType.AllItem)))
            {
                DragHandler.Darggeditem.transform.SetParent(transform);

                //크래프트 검사
                if ((action == (int)ActionType.Craft))
                   CraftSlots.instance.CraftingParts();
                
                return;
            }
            //슬롯에는 아이템이 있지만 슬롯의 아이템과 드래그의 아이템이 동일할때
            else if ((item != null)&& (DragHandler.Darggeditem.GetComponent<Item>().id == item.GetComponent<Item>().id))
            {
                Item slotItem = item.GetComponent<Item>();
                Item dragItem = DragHandler.Darggeditem.GetComponent<Item>();

                //가지고 있는 아이템의 개수가 최대묶음을 넘지 않았을때
                if (slotItem.ConfirmTheMaxPackage())
                {
                    //경우1. 드래그의 아이템수 + 슬롯 아이템수 > 최대묶음
                    if (dragItem.itemCount + slotItem.itemCount > slotItem.maxPackage)
                    {
                        int temMinus;
                        temMinus = slotItem.maxPackage - slotItem.itemCount;
                        slotItem.itemCount = slotItem.maxPackage;

                        dragItem.itemCount -= temMinus;
                    }
                    //경우2. 드래그의 아이템수 + 슬롯 아이템수 <= 최대묶음
                    else if (dragItem.itemCount + slotItem.itemCount <= slotItem.maxPackage)
                    {
                        slotItem.itemCount += dragItem.itemCount;
                        Destroy(DragHandler.Darggeditem);
                    }

                }
                //최대묶음과 같거나 넘었다면 의미없다.
            }
        }
        //스킬로 할려면 추가수정DragHandler.Darggeditem.GetComponent<skill>() != null)
        else if (DragHandler.Darggeditem.GetComponent<Item>() != null)
        {

        }

    }
    #endregion

}
