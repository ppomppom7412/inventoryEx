using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //[SerializeField]
    //Transform slots;
    //[SerializeField]
    //Text inventoryText;
    public Item mountingItem;
    public GameObject mountingOb;
    public GameObject mountText;

    public int InventorySize;
    public GameObject inventoryView;

    public GameObject[] iveSlots;
    public GameObject slotPrefab;

    public Text explainViewText;

    void Start()
    {
        mountingItem = null;
        explainViewText.text = null;
        //inventoryView = GameObject.Find("ContentView");
        InitInventory();
    }

    void Update()
    { 

        if (UIHandler.inventoryButton)
        {
            if (Input.GetKeyDown("1"))
            {
                Debug.Log("GetKey 1");
                AddItem("item001");
            }
            if (Input.GetKeyDown("2"))
            {
                Debug.Log("GetKey 2");
                AddItem("item002");
            }
            if (Input.GetKeyDown("3"))
            {
                Debug.Log("GetKey 3");
                AddItem("item050");
            }
            if (Input.GetKeyDown("4"))
            {
                Debug.Log("GetKey 4");
                AddItem("item051");
            }
            if (Input.GetKeyDown("5"))
            {
                Debug.Log("GetKey 5");
                AddItem("item052");
            }
            if (Input.GetKeyDown("6"))
            {
                Debug.Log("GetKey 6");
                AddItem("item010");
            }
            if (Input.GetKeyDown("7"))
            {
                Debug.Log("GetKey 7");
                AddItem("item011");
            }
        }

    }

    public void ShowItemExplainViewText(string exp)
    {
        explainViewText.text = exp;
    }

    public bool DestroyMessage()
    {
        bool isPointer = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        //드래그 아이템이 존재하지 않고, 포인트에 어떤 UI도 안걸릴때
        if (isPointer && DragHandler.Darggeditem == null)
        {
            //true일때 UI에 걸렸다는 뜻.
            //Debug.Log("포인터 표시" + UnityEngine.EventSystems.EventSystem.current.gameObject.name + " : " + isPointer);
        }

        return false;
    }

    //인벤토리 초기화
    void InitInventory()
    {
        iveSlots = new GameObject[InventorySize];
        for (int i = 0; i < InventorySize; ++i)
        {
            iveSlots[i] = Instantiate(slotPrefab, inventoryView.transform);
            iveSlots[i].GetComponent<Slot>().type = 0;
        }

        mountingItem = null;
        mountText.SetActive(false);
    }

    //아이템 추가하기
    public bool AddItem(string _id)
    {
        //같은 아이템, 꽉차지 않았을때
        if (PlusSameItemCount(_id))
            return true;

        //빈칸확인
        GameObject emptySlot = FindEmptySlot();

        if (emptySlot == null)
            return false;

        ItemManager.instance.CreateItemUIWithID(_id, emptySlot.transform);
        return true;
    }

    //빈슬롯찾기
    public GameObject FindEmptySlot()
    {
        foreach (GameObject ob in iveSlots)
        { 

            if (ob.GetComponent<Slot>().item == null)
            {
                return ob;
            }
        }

        return null;
    }

    //같은 아이템찾아서 갯수추가하기
    public bool PlusSameItemCount(string _id)
    {
        for (int i = 0; i < InventorySize; i++)
        {
            if (iveSlots[i].GetComponent<Slot>().item == null)
                continue;

            Item item = iveSlots[i].GetComponent<Slot>().item.GetComponent<Item>();

            if (item.id == _id)
            {
                if (item.ConfirmTheMaxPackage())
                {
                    item.itemCount++;
                    return true;
                }//end  if (item.ConfirmTheMaxPackage())

            }//end if (item.id == _id)

        }//end for (int i = 0; i < InventorySize; i++)

        return false;

    }//end PlusSameItemCount(int _id)

    //같은 아이템찾아서 갯수 리턴
    public int FindItemCount(string _id)
    {
        int resultCount = 0;

        for (int i = 0; i < InventorySize; i++)
        {
            if (iveSlots[i].GetComponent<Slot>().item == null)
                continue;

            Item item = iveSlots[i].GetComponent<Slot>().item.GetComponent<Item>();

            if (item.id == _id)
            {
                resultCount += item.itemCount;

            }//end if (item.id == _id)

        }//end for (int i = 0; i < InventorySize; i++)

        return resultCount;

    }//end PlusSameItemCount(int _id)

    //장비착용
    public void AddEquit(Item _item, GameObject _ob)
    {
        if (mountingItem == _item && mountingOb == _ob)
        {
            mountingItem.UnActivate();
            if (CraftSlots.activateItem != null)
                CraftSlots.activateItem.UnActivate();

            mountingItem = null;
            mountingOb = null;
            CraftSlots.activateItem = new Item(); ;

            CraftSlots.instance.ClearCraftSlot();
            CraftSlots.instance.CloseCraftSlot();
        }
        else
        {
            mountingItem = _item;
            mountingOb = _ob;

            mountingItem.Activate();
            CraftSlots.instance.OpenCraftSlot();
        }

        if (!mountingOb)
        {
            mountText.SetActive(false);
        }
        else
        {
            mountText.SetActive(true);
            mountText.transform.SetParent(mountingOb.transform);
            mountText.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }


}//end class



