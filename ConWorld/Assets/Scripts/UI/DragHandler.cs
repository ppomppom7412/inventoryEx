using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler{

    public static GameObject Darggeditem;    // i changed itembeigdraged to item.
    public static GameObject ProviousDarggedItem;
    public static Transform startParent;
    Vector3 startPosition;

    //bool start = true;
    //Sprite sprite;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Darggeditem = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Darggeditem.GetComponent<LayoutElement>().ignoreLayout = true;
        Darggeditem.transform.SetParent(Darggeditem.transform.parent.parent);
    }


    public void OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
        else if (transform.parent == startParent.parent)
        {
            //자신의 아빠에게 새로운 자식이 있는지 검사.
            if (startParent.childCount < 1)
            {
                transform.SetParent(startParent);
            }
            else if (startParent.childCount >= 1)
            {
                if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>())
                {
                    Debug.Log("경고 : 부모에게 새로운 자식이 있어 새로운 슬롯에 등록되어야하는데, 인벤토리를 찾지 못해서 돌아갑니다.");
                    return;
                }
                transform.SetParent(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().FindEmptySlot().transform);

                //슬롯이 꽉찼는데 드래그에 들고 있는 것이 있을 경우.
                //if (GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().FindEmptySlot() != null)
                //{
                //    transform.SetParent(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().FindEmptySlot().transform);
                //}
                //else
                //{
                //}

            }// END else if (startParent.childCount >= 1)

        } //end else if (transform.parent == startParent.parent)
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Darggeditem.GetComponent<LayoutElement>().ignoreLayout = false;
        ProviousDarggedItem = Darggeditem;
        Darggeditem = null;
    }

    public void BackToDrop()
    {
        if (ProviousDarggedItem == null)
            return;

        if (startParent.childCount <= 0)
        {
            ProviousDarggedItem.gameObject.transform.SetParent(startParent);
        }
        else
        {
            //인벤토리의 빈칸에 등록시켜준다.
        }

    }

}
