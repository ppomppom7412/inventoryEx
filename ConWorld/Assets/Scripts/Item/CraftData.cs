using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

[SerializeField]
public class CraftData {
    public string baseID;
    public string resultID;
    public string[] partsID;
    public int sizeParts;

    public CraftData()
    {
        partsID = new string[3];
    }

    public bool IsParts(string _partid)
    {
        for (int i = 0; i < 3; i++)
        {
            if (partsID[i] == _partid)
                return true;

        }
        return false;
    }

}