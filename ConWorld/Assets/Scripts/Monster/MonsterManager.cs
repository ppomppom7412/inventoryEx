using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class MonsterManager : MonoBehaviour {

    public static MonsterManager Instance;

    public List<Monster> monsterDBList;
    public List<DrapItem> drapItemDBList;

    public enum MonsterDataIndex { Index, Name, Attack, AttackSpeed, Range, HP, MoveSpeed };
    public enum DrapItemDataIndex { MonsterID, DrapItemID, Order, Probability, PresentQ, PreogressQ };

    public GameObject[] monsterFrifabs;
    public GameObject[] spwnPoints;
    public GameObject[] drapItems;

    void Awake()
    {
        monsterDBList = ReadToMonster("/Resources/XML/monlist.xml");
        drapItemDBList = ReadToDrapItem("/Resources/XML/drapItem.xml");
    }

    void Start() {
        Instance = this;

        CreateMonster("mon07", 0, spwnPoints[0].transform);
        CreateMonster("mon09", 1, spwnPoints[1].transform);
    }

    void Update() {

    }

    //몬스터아이디를 검사해서 드랍아이템결정
    public DrapItem FindToDrapItem(string _monsterid)
    {
        List<DrapItem> list = new List<DrapItem>();

        for (int i = 0; i < drapItemDBList.Count; i++)
        {
            //Debug.Log("drapItemDBList["+i+"].drapMonsterID("+ drapItemDBList[i].drapMonsterID+ ") == _monsterid("+_monsterid+")");

            if (drapItemDBList[i].drapMonsterID == _monsterid && drapItemDBList[i].CheckPresent())
            {
                //퀘스트에 따라 리스트에 추가하지 않음.
                //관계없다면 추가.
                list.Add(drapItemDBList[i]);
            }
        }

        if (list.Count < 1)
        {
            Debug.Log("드랍할 아이템을 찾지 못하였습니다.");
            return null;
        }

        //우선순위가 있을시
        //list.Sort() >인덱스와 인터페이스 사용
        DrapItem select = new DrapItem();
        select.order = 100;
        for (int j = 0; j < list.Count; j++)
        {
            for (int n=0; n < list.Count; ++n)
            {
                //우선순위 결정.
                if (select.order > list[n].order)
                {
                    select = list[n];
                }

            }

            if (select.ProbabilityDrap())
            {
                DrapItemPrefabsSetting(ref select);
                return select;
            }
            else
            {   
                list.Remove(select);
                list.Sort();
            }

        }

        return new DrapItem();
    }

    public void DrapItemPrefabsSetting(ref DrapItem item)
    {
        switch (item.drapitemID)
        {
            case "item102":
                item.prefab = drapItems[0];
                break;
            case "item104":
                item.prefab = drapItems[1];
                break;
            case "item011":
                item.prefab = drapItems[2];
                break;
            case "item006":
                item.prefab = drapItems[3];
                break;

            default:
                break;
        }
    }

    public void CreateMonster(string _monIndex,int _modelIndex, Transform _setPos)
    {
        //Debug.Log("Create Monster() "+ monsterDBList[_monIndex].m_name);

        GameObject newMonster = Instantiate(monsterFrifabs[_modelIndex], _setPos.position, _setPos.rotation);
        //newMonster.tag = "Monster";

        if (newMonster.GetComponent<Monster>()) {
            Monster dataSetMonster = newMonster.GetComponent<Monster>();
            Monster setmonster = monsterDBList.Find(x => { return x.m_index == _monIndex; });

            dataSetMonster.m_index = setmonster.m_index;
            dataSetMonster.m_name = setmonster.m_name;
            dataSetMonster.m_attack = setmonster.m_attack;
            dataSetMonster.m_attackRange = setmonster.m_attackRange;
            dataSetMonster.m_attackSpeed = setmonster.m_attackSpeed;
            dataSetMonster.m_MaxHP = setmonster.m_HP;
            dataSetMonster.m_HP = setmonster.m_MaxHP;
            dataSetMonster.m_moveSpeed = setmonster.m_moveSpeed;

            //Debug.Log(dataSetMonster.m_name + " - "+dataSetMonster.m_attack + " " + dataSetMonster.m_attackSpeed + " " + dataSetMonster.m_attackRange + " " + dataSetMonster.m_HP + " " + dataSetMonster.m_moveSpeed);
        }

    }

    public List<Monster> ReadToMonster(string filePath)
    {
        Debug.Log(Application.dataPath + filePath + "경로로 파일을 엽니다.");

        XmlDocument document = new XmlDocument();
        document.Load(Application.dataPath + filePath);

        XmlElement ElementList = document["root"]; //list name

        List<Monster> DataList = new List<Monster>();

        foreach (XmlElement xmlElement in ElementList.ChildNodes)
        {
            Monster monster = new Monster();

            monster.m_index = xmlElement.ChildNodes[(int)MonsterDataIndex.Index].InnerText;
            monster.m_name = xmlElement.ChildNodes[(int)MonsterDataIndex.Name].InnerText;
            monster.m_attack = int.Parse(xmlElement.ChildNodes[(int)MonsterDataIndex.Attack].InnerText);
            monster.m_attackSpeed = float.Parse(xmlElement.ChildNodes[(int)MonsterDataIndex.AttackSpeed].InnerText);
            monster.m_attackRange = int.Parse(xmlElement.ChildNodes[(int)MonsterDataIndex.Range].InnerText);
            monster.m_HP = int.Parse(xmlElement.ChildNodes[(int)MonsterDataIndex.HP].InnerText);
            monster.m_moveSpeed = float.Parse(xmlElement.ChildNodes[(int)MonsterDataIndex.MoveSpeed].InnerText);
            //Debug.Log(monster.m_name);
            //Debug.Log(monster.m_attack+" "+monster.m_attackSpeed+ " "+monster.m_attackRange+ " " + monster.m_HP+ " " + monster.m_moveSpeed);

            DataList.Add(monster);
        }

        return DataList;
    }

    public List<DrapItem> ReadToDrapItem(string filePath)
    {
        Debug.Log(Application.dataPath + filePath + "경로로 파일을 엽니다.");

        XmlDocument document = new XmlDocument();
        document.Load(Application.dataPath + filePath);

        XmlElement ElementList = document["rows"]; //list name

        List<DrapItem> DataList = new List<DrapItem>();

        foreach (XmlElement xmlElement in ElementList.ChildNodes)
        {
            DrapItem drap = new DrapItem();

            drap.drapMonsterID = (xmlElement.ChildNodes[(int)DrapItemDataIndex.MonsterID].InnerText);
            drap.drapitemID = xmlElement.ChildNodes[(int)DrapItemDataIndex.DrapItemID].InnerText;
            drap.order = int.Parse(xmlElement.ChildNodes[(int)DrapItemDataIndex.Order].InnerText);
            drap.drapProb = float.Parse(xmlElement.ChildNodes[(int)DrapItemDataIndex.Probability].InnerText);
            drap.presentQ = xmlElement.ChildNodes[(int)DrapItemDataIndex.PresentQ].InnerText;
            drap.preogressQ = xmlElement.ChildNodes[(int)DrapItemDataIndex.PreogressQ].InnerText;
            //Debug.Log("드랍 아이템 : "+drap.drapitemID);
            //Debug.Log(monster.m_attack+" "+monster.m_attackSpeed+ " "+monster.m_attackRange+ " " + monster.m_HP+ " " + monster.m_moveSpeed);

            DataList.Add(drap);
        }

        return DataList;
    }
}

//입력 규칙 : Application.dataPath + "/Output/ItemList_Attributes.xml" (<경로)
//application.dataPath은 해당 프로젝트 위치를 찾아줌

//XmlElement의 변수이름은 xmlelement로 가정
//문자열 : xmlelement.GetAttribute("데이터이름");
//정수 :  System.Convert.ToInt32(xmlelement.GetAttribute("데이터이름"));
//실수 :  System.Convert.ToSingle (xmlelement.GetAttribute("데이터이름"));


///*****************위 예시 코드는 큰 틀안에 바로 적용될 경우이고
///
///*****************현재 적용된 코드는 큰 틀안에 작은 틀이 생겨 데이터를 담아둔 경우

//주의 : 한글 string은 읽히지 않아 오류가 뜹니다. 데이터는 무조건 영어로 해주세요!!

//문자열 : xmlElement.ChildNodes[원하는 속성 인덱스].InnerText
//정수   : int.Parse(xmlElement.ChildNodes[원하는 속성 인덱스].InnerText)
//실수   : float.Parse(xmlElement.ChildNodes[원하는 속성 인덱스].InnerText)

//테스팅용 코드
/////
//Debug.Log(Application.dataPath + "/Resources/XML/example.xml" + "경로로 파일을 엽니다.");

//XmlDocument document = new XmlDocument();
//document.Load(Application.dataPath + "/Resources/XML/example.xml");

//XmlElement ElementList = document["catalog"]; //

//string text = ElementList.ChildNodes[0].ChildNodes[1].InnerText;

//Debug.Log("ElementList author: " + text);

///////

