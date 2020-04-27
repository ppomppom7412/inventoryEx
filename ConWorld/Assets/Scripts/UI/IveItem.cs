using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IveItem : MonoBehaviour {

    public Text countText;

    public void Settingcounttext()
    {
        countText = gameObject.GetComponent<Text>();
        countText.rectTransform.anchorMin = new Vector2(1, 0);
        countText.rectTransform.anchorMax = new Vector2(1, 0);
        countText.rectTransform.localPosition = new Vector3(-10, +10, 0);
        countText.color = Color.white;
        countText.alignment = TextAnchor.MiddleCenter;
    }

    public void ShowItemCount()
    {
        if (transform.parent.GetComponent<Item>().itemCount <= 1)
        {
            countText.text = "";
            return;
        }
        else
        {
            countText.text = transform.parent.GetComponent<Item>().itemCount.ToString();
        }

    }

    void Start () {
        Settingcounttext();
    }

	void Update () {
        ShowItemCount();
    }
}
