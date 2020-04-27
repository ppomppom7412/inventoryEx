using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {
    public static bool inventoryButton = false;

    public GameObject invCanvas;
	void Start () {
        invCanvas.SetActive(inventoryButton);

    }
	
	void Update () {

        invBtn_Switch();
    }

    void FixedUpdate()
    {
        
    }

    void invBtn_Switch()
    {
        if (Input.GetKeyDown("i"))
        {
            inventoryButton = !inventoryButton;
            invCanvas.SetActive(inventoryButton);

        }
    }
}
