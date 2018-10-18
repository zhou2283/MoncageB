using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;

public class SaveManager : UnitySingleton<SaveManager> {

    string path;
    public int i = 0;
    void Start()
    {
        path = Application.dataPath + "/saveData.xml";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            print(i);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGameData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGameData();
        }
    }

    public bool CheckGameData()
    {
        return File.Exists(path);
    }
    public void InitializeGameData()
    {
        SaveGameData();
    }

    public void SaveGameData()
    {
        XmlDocument xml = new XmlDocument();
        XmlElement root = xml.CreateElement("saveData");
        XmlElement zoneGroupElement = xml.CreateElement("zoneGroup");
        XmlElement markPositionGroupElement = xml.CreateElement("markPositionGroup");
        //write zone/scene/animatePart
        ZoneGroupManager zoneGroupManager = GameObject.Find("ZoneGroup").transform.GetComponent<ZoneGroupManager>();
        for (int i = 1; i <= 4; i++)
        {
            XmlElement zoneElement = xml.CreateElement("zone" + i.ToString());
            zoneElement.SetAttribute("active", zoneGroupManager.maskArray[i].gameObject.activeSelf.ToString());
            zoneElement.SetAttribute("activeSceneName", "null");
            foreach (Transform scene in zoneGroupManager.zoneArray[i])
            {
                XmlElement sceneElement = xml.CreateElement("scene");
                sceneElement.SetAttribute("name", scene.name);
                zoneElement.AppendChild(sceneElement);
                //set zone-active-scene
                if (scene.gameObject.activeSelf == true)
                {
                    zoneElement.SetAttribute("activeSceneName", scene.name);
                }
                //find all animatePart
                AnimatePartBase[] animatePartBaseGroup = scene.GetComponentsInChildren<AnimatePartBase>(true);
                foreach (AnimatePartBase child in animatePartBaseGroup)
                {
                    XmlElement animatePartElement = xml.CreateElement("animatePart");
                    animatePartElement.SetAttribute("name", child.transform.name);
                    animatePartElement.SetAttribute("phase", child.phase.ToString());
                    sceneElement.AppendChild(animatePartElement);
                }

            }
            //if the mask is inactive, also set zone-active-scene to null
            /*
            if (zoneGroupManager.maskArray[i].gameObject.activeSelf == false)
            {
                zoneElement.SetAttribute("activeSceneName", "null");
            }
            */
            //set children
            zoneGroupElement.AppendChild(zoneElement);
        }
        //write markPosition
        Transform markPositionGroup = GameObject.Find("MarkPositionGroup").transform;
        foreach (Transform child in markPositionGroup)
        {
            XmlElement markPositionElement = xml.CreateElement("markPosition");
            markPositionElement.SetAttribute("name", child.gameObject.name);
            markPositionElement.SetAttribute("phase", child.GetComponent<MarkPositionBase>().phase.ToString());
            markPositionElement.SetAttribute("active", child.gameObject.activeSelf.ToString());
            markPositionGroupElement.AppendChild(markPositionElement);
        }
        //set children
        root.AppendChild(zoneGroupElement);
        root.AppendChild(markPositionGroupElement);
        xml.AppendChild(root);
        //save it
        xml.Save(path);
    }

    public void LoadGameData()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(path);
        XmlNode root = xml.SelectSingleNode("saveData");
        XmlNode zoneGroupNode = root.SelectSingleNode("zoneGroup");
        XmlNode markPositionGroupNode = root.SelectSingleNode("markPositionGroup");
        //read zone/scene/animatePart
        ZoneGroupManager zoneGroupManager = GameObject.Find("ZoneGroup").transform.GetComponent<ZoneGroupManager>();
        for (int i = 1; i <= 4; i++)
        {
            XmlNode zoneNode = zoneGroupNode.SelectSingleNode("zone" + i.ToString());
            //set mask
            zoneGroupManager.maskArray[i].gameObject.SetActive(BoolParse(zoneNode.Attributes["active"].Value));
            //set scene
            foreach (Transform scene in zoneGroupManager.zoneArray[i])
            {   
                XmlNode sceneNode = zoneNode.SelectNodes("scene[@name='" + scene.gameObject.name + "']")[0];
                //set active scene
                if (scene.gameObject.name == zoneNode.Attributes["activeSceneName"].Value)
                {
                    scene.gameObject.SetActive(true);
                }
                else
                {
                    scene.gameObject.SetActive(false);
                }
                //set animate part
                //find all animatePart
                AnimatePartBase[] animatePartBaseGroup = scene.GetComponentsInChildren<AnimatePartBase>(true);
                List<Transform> animatePartList = new List<Transform>();
                foreach (AnimatePartBase child in animatePartBaseGroup)
                {
                    child.SetPhase(int.Parse(sceneNode.SelectNodes("animatePart[@name='" + child.gameObject.name + "']")[0].Attributes["phase"].Value));
                }
            }
        }
        //set markPosition
        Transform markPositionGroup = GameObject.Find("MarkPositionGroup").transform;
        foreach (Transform child in markPositionGroup)
        {
            child.GetComponent<MarkPositionBase>().SetPhase(int.Parse(markPositionGroupNode.SelectNodes("markPosition[@name='" + child.gameObject.name + "']")[0].Attributes["phase"].Value));
            child.gameObject.SetActive(BoolParse(markPositionGroupNode.SelectNodes("markPosition[@name='" + child.gameObject.name + "']")[0].Attributes["active"].Value));
        }
    }

    bool BoolParse(string str)
    {
        if (str.ToUpper() == "TRUE")
        {
            return true;
        }
        else if (str.ToUpper() == "FALSE")
        {
            return false;
        }
        else
        {
            print("Bool Parse Error: " + str.ToUpper());
            return false; 
        }
    }
}


