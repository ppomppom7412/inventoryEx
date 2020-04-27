using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlots : MonoBehaviour
{
    static public CraftSlots instance;
    static public Item activateItem;

    private Inventory inventory;
    private Slot[] cratfSlot;
    private string[] previousSlot;

    public GameObject[] gameObjectSlot;

    void Start()
    {
        instance = this;
        activateItem = new Item();
        //gameObjectSlot = new GameObject[gameObject.transform.GetChildCount()];
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        cratfSlot = new Slot[3];
        previousSlot = new string[3];

        for (int i=0; i<3;++i)
        {
            //gameObjectSlot[i] = gameObject.transform.GetChild(i).GetComponent<GameObject>();

            if (gameObjectSlot[i].GetComponent<Slot>() != null)
            {
                cratfSlot[i] = gameObjectSlot[i].GetComponent<Slot>();
                cratfSlot[i].type = (int)Slot.SlotType.Parts;
                cratfSlot[i].action = (int)Slot.ActionType.Craft;
            }
            else
            {
                Debug.Log("Craft란에 Slot이 필요합니다.");
                return;
            }
        }

        CloseCraftSlot();
    }

    void Update()
    {
        if (!ComparedToPrevious())
        {
            if (activateItem.isActivate == true)
            {
                activateItem.UnActivate();
                activateItem = new Item();
                inventory.mountingItem.Activate();
            }

            CraftingParts();
        }
    }

    public bool ComparedToPrevious()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (cratfSlot[i].item != null && cratfSlot[i].item.GetComponent<Item>() != null)
            {
                if (previousSlot[i] != cratfSlot[i].item.GetComponent<Item>().id)
                    return false;
            }
            else
            {
                if (previousSlot[i] != "")
                    return false;
            }
        }

        return true;
    }

    public void ClearCraftSlot()
    {
        for (int i = 0; i < 3; ++i) {

            if (cratfSlot[i].item == null)
                continue;

            GameObject empty = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().FindEmptySlot();
            cratfSlot[i].item.transform.SetParent(empty.transform);
        }

    }

    public void CloseCraftSlot()
    {
        for (int i = 0; i < 3; ++i)
        {
            cratfSlot[i].type = (int)Slot.SlotType.None;
        }
    }

    public void OpenCraftSlot()
    {
        for (int i = 0; i < 3; ++i)
        {
            cratfSlot[i].type = (int)Slot.SlotType.Parts;
        }
    }

    public bool CraftingParts()
    {
        //장착하고 있는 기본 무기 확인
        if (inventory == null || inventory.mountingItem == null)
            return false;

        string[] ids = new string[3];

        //슬롯아이템 등록
        for (int i = 0; i < 3; ++i)
        {
            if (cratfSlot[i].item != null && cratfSlot[i].item.GetComponent<Item>() != null) {
                ids[i] = cratfSlot[i].item.GetComponent<Item>().id;
                previousSlot[i] = ids[i];
            }
            else {
                ids[i] = "";
                previousSlot[i] = ids[i];
            }
        }

        List<CraftData> currentCraftdataList = ItemManager.instance.LookForDataByBaseID(inventory.mountingItem.id);

        if (currentCraftdataList == null)
            return false;

        foreach (CraftData data in currentCraftdataList)
        {
            string check = ItemManager.instance.FindResultIDWithParts(data, ids[0], ids[1], ids[2]);
            if (check != "")
            {
                Item craftresult = ItemManager.instance.CreateItemWithID(check);
                if (activateItem.isActivate != true)  {
                    inventory.mountingItem.UnActivate();
                    activateItem = craftresult;
                    activateItem.Activate();
                    return true;
                }

                else {
                    if (activateItem.id == craftresult.id)
                        return false;

                    activateItem.UnActivate();
                    activateItem.Activate();
                    return true;
                } //end else
            }// if
        }//end foreach
        return false;
    }

}






///////================================================================

