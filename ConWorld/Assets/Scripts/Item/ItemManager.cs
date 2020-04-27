using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ItemManager : MonoBehaviour {
    public GameObject[] EquipUIPrefabs;
    public GameObject[] PartsUIPrefabs;
    public GameObject[] ConsumeUIPrefabs;
    public GameObject[] EtcUIPrefabs;

    static public ItemManager instance;

    public List<CraftData> creaftdataList;
    public enum CraftdataIndex { Result, Base, Parts, None };

    [SerializeField]
    public List<Parts> partsItemList;
    public List<Equip> equipItemList;
    public List<Potion> consumeItemList;
    public List<Etc> etcItemList;

    public enum PartsItemIndex { ItemID, Name, Explain,MaxPackage, IsConsume};
    public enum EquipItemIndex { ItemID, Name, Explain, MaxPackage, Attack, AttackSpeed, Range, Effect, Usesp};
    public enum ConsumeItemIndex { ItemID, Name, Explain, MaxPackage, Hpheal, Spheal, Effect };
    public enum EtcItemIndex { ItemID, Name, Explain, MaxPackage };


    void Awake()
    {
        //싱글톤이면 SET을 막아두는 것이 좋다. 생성또한 마찬가지!
        if (!instance)
        {
            //싱글톤
            instance = FindObjectOfType(typeof(ItemManager)) as ItemManager; //new ItemManager();

            //크레프팅 데이터 셋팅
            creaftdataList = ReadCraftData("/Resources/XML/CraftData.xml");

            //각각의 아이템 데이터 셋팅
            partsItemList = ReadPartsData("/Resources/XML/partsItem.xml");
            equipItemList = ReadEquitData("/Resources/XML/equipItem.xml");
            consumeItemList = ReadPotionData("/Resources/XML/consumeItem.xml");
            etcItemList = ReadEtcData("/Resources/XML/etcItem.xml");
        }
    }

    public void Start()
    {

    }

    private ItemManager()
    {
    }

    //*-------------데이터가 비어있는지 확인해주는 함수
    private bool CheckedDataSet(string data)
    {
        if (data != "")
            return true;

        return false;
    }

    //*-------------파일 불러오기
    public List<Equip> ReadEquitData(string filePath)
    {
        Debug.Log(Application.dataPath + filePath + "경로로 파일을 엽니다.");

        XmlDocument document = new XmlDocument();
        document.Load(Application.dataPath + filePath);

        XmlElement ElementList = document["rows"]; //list name

        List<Equip> DataList = new List<Equip>();

        foreach (XmlElement xmlElement in ElementList.ChildNodes)
        {
            //ItemID, Name, Explain, MaxPackage, Attack, AttackSpeed, Range, Effect, Usesp
            Equip data = new Equip();
            data.id = xmlElement.ChildNodes[(int)EquipItemIndex.ItemID].InnerText;
            data.itemName = xmlElement.ChildNodes[(int)EquipItemIndex.Name].InnerText;
            data.exp = xmlElement.ChildNodes[(int)EquipItemIndex.Explain].InnerText;
            data.maxPackage = int.Parse(xmlElement.ChildNodes[(int)EquipItemIndex.MaxPackage].InnerText);
            data.attack = float.Parse(xmlElement.ChildNodes[(int)EquipItemIndex.Attack].InnerText);
            data.attackspeed = float.Parse(xmlElement.ChildNodes[(int)EquipItemIndex.AttackSpeed].InnerText);
            data.range = float.Parse(xmlElement.ChildNodes[(int)EquipItemIndex.Range].InnerText);
            data.effect = int.Parse(xmlElement.ChildNodes[(int)EquipItemIndex.Effect].InnerText);
            data.usesp = int.Parse(xmlElement.ChildNodes[(int)EquipItemIndex.Usesp].InnerText);
            data.itemType = (int)Slot.SlotType.Equip;
            //Debug.Log("장비 아이템 생성 :" + data.id);
            DataList.Add(data);
        }

        return DataList;
    }

    public List<Potion> ReadPotionData(string filePath)
    {
        Debug.Log(Application.dataPath + filePath + "경로로 파일을 엽니다.");

        XmlDocument document = new XmlDocument();
        document.Load(Application.dataPath + filePath);

        XmlElement ElementList = document["rows"]; //list name

        List<Potion> DataList = new List<Potion>();

        foreach (XmlElement xmlElement in ElementList.ChildNodes)
        {
            //ConsumeItemIndex { ItemID, Name, Explain, MaxPackage, Hpheal, Spheal, Effect };
            Potion data = new Potion();
            data.id = xmlElement.ChildNodes[(int)ConsumeItemIndex.ItemID].InnerText;
            data.itemName = xmlElement.ChildNodes[(int)ConsumeItemIndex.Name].InnerText;
            data.exp = xmlElement.ChildNodes[(int)ConsumeItemIndex.Explain].InnerText;
            data.maxPackage = int.Parse(xmlElement.ChildNodes[(int)ConsumeItemIndex.MaxPackage].InnerText);
            data.hpHeal = int.Parse(xmlElement.ChildNodes[(int)ConsumeItemIndex.Hpheal].InnerText);
            data.spHeal = int.Parse(xmlElement.ChildNodes[(int)ConsumeItemIndex.Spheal].InnerText);
            data.effect = xmlElement.ChildNodes[(int)ConsumeItemIndex.Effect].InnerText;
            data.itemType = (int)Slot.SlotType.Potion;
            //Debug.Log("소비 아이템 생성 :" + data.id);
            DataList.Add(data);
        }

        return DataList;
    }

    public List<Parts> ReadPartsData(string filePath)
    {
        Debug.Log(Application.dataPath + filePath + "경로로 파일을 엽니다.");

        XmlDocument document = new XmlDocument();
        document.Load(Application.dataPath + filePath);

        XmlElement ElementList = document["rows"]; //list name

        List<Parts> DataList = new List<Parts>();

        foreach (XmlElement xmlElement in ElementList.ChildNodes)
        {
            //PartsItemIndex { ItemID, Name, Explain,MaxPackage, IsConsume};

            Parts data = new Parts();
            data.id = xmlElement.ChildNodes[(int)PartsItemIndex.ItemID].InnerText;
            data.itemName = xmlElement.ChildNodes[(int)PartsItemIndex.Name].InnerText;
            data.exp = xmlElement.ChildNodes[(int)PartsItemIndex.Explain].InnerText;
            data.maxPackage = int.Parse(xmlElement.ChildNodes[(int)PartsItemIndex.MaxPackage].InnerText);
            data.IsIntinite = int.Parse(xmlElement.ChildNodes[(int)PartsItemIndex.IsConsume].InnerText);
            data.itemType = (int)Slot.SlotType.Parts;
            //Debug.Log("부품 아이템 생성 :" + data.id);
            DataList.Add(data);
        }

        return DataList;
    }

    public List<Etc> ReadEtcData(string filePath)
    {
        Debug.Log(Application.dataPath + filePath + "경로로 파일을 엽니다.");

        XmlDocument document = new XmlDocument();
        document.Load(Application.dataPath + filePath);

        XmlElement ElementList = document["rows"]; //list name

        List<Etc> DataList = new List<Etc>();

        foreach (XmlElement xmlElement in ElementList.ChildNodes)
        {
            //ConsumeItemIndex { ItemID, Name, Explain, MaxPackage, Hpheal, Spheal, Effect };
            Etc data = new Etc();
            data.id = xmlElement.ChildNodes[(int)ConsumeItemIndex.ItemID].InnerText;
            data.itemName = xmlElement.ChildNodes[(int)ConsumeItemIndex.Name].InnerText;
            data.exp = xmlElement.ChildNodes[(int)ConsumeItemIndex.Explain].InnerText;
            data.maxPackage = int.Parse(xmlElement.ChildNodes[(int)ConsumeItemIndex.MaxPackage].InnerText);
            data.itemType = (int)Slot.SlotType.Potion;
            //Debug.Log("기타 아이템 생성 :" + data.id);
            DataList.Add(data);
        }

        return DataList;
    }

    public List<CraftData> ReadCraftData(string filePath)
    {
        Debug.Log(Application.dataPath + filePath + "경로로 파일을 엽니다.");

        XmlDocument document = new XmlDocument();
        document.Load(Application.dataPath + filePath);

        XmlElement ElementList = document["rows"]; //list name

        List<CraftData> DataList = new List<CraftData>();

        foreach (XmlElement xmlElement in ElementList.ChildNodes)
        {
            CraftData data = new CraftData();
            data.resultID = xmlElement.ChildNodes[(int)CraftdataIndex.Result].InnerText;
            data.baseID = xmlElement.ChildNodes[(int)CraftdataIndex.Base].InnerText;

            for (int i = 0, j = 0; i < 3; ++i)
            {
                if (CheckedDataSet(xmlElement.ChildNodes[(int)CraftdataIndex.Parts + i].InnerText))
                {
                    data.partsID[i] = xmlElement.ChildNodes[(int)CraftdataIndex.Parts + i].InnerText;
                    j++;
                    data.sizeParts = j;
                }
                else
                {
                    data.partsID[i] = "";
                }

                //Debug.Log(data.partsID[i] + " craftid["+i+"]");
            }
            //Debug.Log("완성ID" + data.resultID + "재료ID" + data.baseID +"파츠갯수"+ data.sizeParts.ToString());

            DataList.Add(data);
        }

        return DataList;
    }


    //*-------------아이템 타입 리턴 함수
    public int FinditemTypeWithItemID(string _id)
    {
        List<Item> tempList = new List<Item>();
        foreach(Item i in equipItemList)
        {
            tempList.Add(i);
        }
        foreach (Item i in consumeItemList)
        {
            tempList.Add(i);
        }
        foreach (Item i in partsItemList)
        {
            tempList.Add(i);
        }
        foreach (Item i in etcItemList)
        {
            tempList.Add(i);
        }

        Item tempItem = tempList.Find((x) => { return x.id == _id; });
        int type = tempItem.itemType;
        return type;

    }

    //*-------------ID에 따른 아이템 생성후 리턴
    public Equip FindingEquipItem(string _id)
    {
        return equipItemList.Find(x => { return x.id == _id; });
    }

    public Potion FindingConsumeItem(string _id)
    {
        return consumeItemList.Find(x => { return x.id == _id; });
    }

    public Parts FindingPartsItem(string _id)
    {
        return partsItemList.Find(x => { return x.id == _id; });
    }

    public Etc FindingEtcItem(string _id)
    {
        return etcItemList.Find(x => { return x.id == _id; });
    }

    //*-------------아이템 UI프리팹 생성에 필요한 함수
    public bool CreateEquipUI(Equip _eq, Transform _parent)
    {
        GameObject ob;
        Equip obEquip;
        switch (_eq.id)
        {
            case "item001":
                ob = Instantiate(EquipUIPrefabs[0], _parent);
                obEquip = ob.AddComponent<Equip>(); 
                break;
            case "item002":
                ob = Instantiate(EquipUIPrefabs[1], _parent);
                obEquip = ob.AddComponent<Equip>();
                break;
            case "item003":
                ob = Instantiate(EquipUIPrefabs[2], _parent);
                obEquip = ob.AddComponent<Equip>();
                break;
            case "item004":
                ob = Instantiate(EquipUIPrefabs[3], _parent);
                obEquip = ob.AddComponent<Equip>();
                break;
            case "item005":
                ob = Instantiate(EquipUIPrefabs[4], _parent);
                obEquip = ob.AddComponent<Equip>();
                break;
            case "item006":
                ob = Instantiate(EquipUIPrefabs[5], _parent);
                obEquip = ob.AddComponent<Equip>();
                break;
            case "item007":
                ob = Instantiate(EquipUIPrefabs[6], _parent);
                obEquip = ob.AddComponent<Equip>();
                break;
            case "item008":
                ob = Instantiate(EquipUIPrefabs[7], _parent);
                obEquip = ob.AddComponent<Equip>();
                break;

            default:
                return false;
        }

        obEquip.id = _eq.id;
        obEquip.itemName = _eq.itemName;
        obEquip.itemType = _eq.itemType;
        obEquip.maxPackage = _eq.maxPackage;
        obEquip.attack = _eq.attack;
        obEquip.attackspeed = _eq.attackspeed;
        obEquip.range = _eq.range;
        obEquip.effect = _eq.effect;
        obEquip.exp = _eq.exp;
        obEquip.itemCount = 1;

        return true;
    }

    public bool CreateConsumeUI(Potion _po, Transform _parent)
    {
        GameObject ob;
        Potion obPotion;
        switch (_po.id)
        {
            case "item010":
                ob = Instantiate(ConsumeUIPrefabs[0], _parent);
                obPotion = ob.AddComponent<Potion>();
                break;
            case "item011":
                ob = Instantiate(ConsumeUIPrefabs[1], _parent);
                obPotion = ob.AddComponent<Potion>();
                break;
            case "item012":
                ob = Instantiate(ConsumeUIPrefabs[2], _parent);
                obPotion = ob.AddComponent<Potion>();
                break;
            case "item013":
                ob = Instantiate(ConsumeUIPrefabs[3], _parent);
                obPotion = ob.AddComponent<Potion>();
                break;
            case "item014":
                ob = Instantiate(ConsumeUIPrefabs[4], _parent);
                obPotion = ob.AddComponent<Potion>();
                break;

            default:
                return false;
        }

        obPotion.id = _po.id;
        obPotion.itemName = _po.itemName;
        obPotion.itemType = _po.itemType;
        obPotion.hpHeal = _po.hpHeal;
        obPotion.spHeal = _po.spHeal;
        obPotion.effect = _po.effect;
        obPotion.maxPackage = _po.maxPackage;
        obPotion.exp = _po.exp;
        obPotion.itemCount = 1;

        return true;
    }

    public bool CreatePartsUI(Parts _pa, Transform _parent)
    {
        GameObject ob;
        Parts obParts;
        switch (_pa.id)
        {
            case "item050":
                ob = Instantiate(PartsUIPrefabs[0], _parent);
                obParts = ob.AddComponent<Parts>();
                break;
            case "item051":
                ob = Instantiate(PartsUIPrefabs[1], _parent);
                obParts = ob.AddComponent<Parts>();
                break;
            case "item052":
                ob = Instantiate(PartsUIPrefabs[2], _parent);
                obParts = ob.AddComponent<Parts>();
                break;
            case "item053":
                ob = Instantiate(PartsUIPrefabs[3], _parent);
                obParts = ob.AddComponent<Parts>();
                break;
            case "item054":
                ob = Instantiate(PartsUIPrefabs[4], _parent);
                obParts = ob.AddComponent<Parts>();
                break;

            default:
                return false;
        }

        obParts.id = _pa.id;
        obParts.itemName = _pa.itemName;
        obParts.itemType = _pa.itemType;
        obParts.maxPackage = _pa.maxPackage;
        obParts.IsIntinite = _pa.IsIntinite;
        obParts.exp = _pa.exp;
        obParts.itemCount = 1;

        return true;
    }

    public bool CreateEtcUI(Etc _pa, Transform _parent)
    {
        GameObject ob;
        Etc obEtc;
        switch (_pa.id)
        {
            case "item100":
                ob = Instantiate(EtcUIPrefabs[0], _parent);
                obEtc = ob.AddComponent<Etc>();
                break;
            case "item101":
                ob = Instantiate(EtcUIPrefabs[1], _parent);
                obEtc = ob.AddComponent<Etc>();
                break;
            case "item102":
                ob = Instantiate(EtcUIPrefabs[2], _parent);
                obEtc = ob.AddComponent<Etc>();
                break;
            case "item103":
                ob = Instantiate(EtcUIPrefabs[3], _parent);
                obEtc = ob.AddComponent<Etc>();
                break;
            case "item104":
                ob = Instantiate(EtcUIPrefabs[4], _parent);
                obEtc = ob.AddComponent<Etc>();
                break;
            case "item105":
                ob = Instantiate(EtcUIPrefabs[5], _parent);
                obEtc = ob.AddComponent<Etc>();
                break;
            case "item106":
                ob = Instantiate(EtcUIPrefabs[6], _parent);
                obEtc = ob.AddComponent<Etc>();
                break;

            default:
                return false;
        }

        obEtc.id = _pa.id;
        obEtc.itemName = _pa.itemName;
        obEtc.itemType = _pa.itemType;
        obEtc.maxPackage = _pa.maxPackage;
        obEtc.exp = _pa.exp;
        obEtc.itemCount = 1;

        return true;
    }

    //*-------------아이템 생성에 필요한 함수
    public bool CreateItemUIWithID(string _id, Transform _parent)
    {
        int type = FinditemTypeWithItemID(_id);

        switch (type)
        {
            case (int)Slot.SlotType.Equip:
                Equip equit = FindingEquipItem(_id);
                CreateEquipUI(equit, _parent);
                return true;

            case (int)Slot.SlotType.Potion:
                Potion potion = FindingConsumeItem(_id);
                CreateConsumeUI(potion, _parent);
                return true;

            case (int)Slot.SlotType.Parts:
                Parts parts = FindingPartsItem(_id);
                CreatePartsUI(parts, _parent);
                return true;

            case (int)Slot.SlotType.Etc:
                Etc etc = FindingEtcItem(_id);
                CreateEtcUI(etc, _parent);
                return true;

            default:
                Debug.Log("아이템에 해당하는 아이디가 없습니다.");
                break;
        }//end switch (type)
        return false;
    }

    public Item CreateItemWithID(string _id)
    {
        switch (FinditemTypeWithItemID(_id))
        {
            case (int)Slot.SlotType.Equip:
                Equip equit = FindingEquipItem(_id);
                return equit;

            case (int)Slot.SlotType.Potion:
                Potion potion = FindingConsumeItem(_id);
                return potion;

            case (int)Slot.SlotType.Parts:
                Parts parts = FindingPartsItem(_id);
                return parts;

            case (int)Slot.SlotType.Etc:
                Etc etc = FindingEtcItem(_id);
                return etc;

            default:
                return null;
        }
    }

    //*-------------크레프트에 필요한 함수
    public List<CraftData> LookForDataByBaseID(string _baseid)
    {
        return creaftdataList.FindAll(x => x.baseID == _baseid);
    }

    public bool CheckForParts(CraftData craftdata,string _part,int _slotnumber)
    {
        if (craftdata.partsID[_slotnumber] == _part)
        {
            return true;
        }

        return false;
    }

    public string FindResultIDWithParts(CraftData craftdata, string _part1="", string _part2="", string _part3="")
    {
        if (craftdata.partsID[0] == _part1)
            if (craftdata.partsID[1] == _part2)
                if (craftdata.partsID[2] == _part3)
                    return craftdata.resultID;

        return "";
    }
}
