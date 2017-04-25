using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterContainer : MonoBehaviour {

    public CharacterInfoObject logItemProto;
    Dictionary<string, CharacterInfoObject> logItemList = new Dictionary<string, CharacterInfoObject>();
    public static CharacterContainer instance;

    void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

    }

    public void populateContainer()
    {
        

        foreach (Character p in GameControllerScript.instance.party.getParty())
        {
            if (logItemList.ContainsKey(p.CharID))
            {
                updateCharacterInfo(p);
            } else
            {
                addCharacter(p);
            }
        }
        //LogItem temp = newLogItem(eventObject.Summary);

    }


    public void updateCharacterInfo(Character characterObject)
    {
        CharacterInfoObject charInfo = logItemList[characterObject.CharID];
        charInfo.Description.text = characterObject.Name + "\n" + "<color=red>Health:</color>" + characterObject.getStat("Health");
    }

    public void removeCharacter(Character characterObject)
    {
        CharacterInfoObject charInfo = logItemList[characterObject.CharID];
        logItemList.Remove(characterObject.CharID);
        Destroy(charInfo.gameObject);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void addCharacter(Character characterObject)
    {
        CharacterInfoObject newItem = Instantiate(logItemProto, transform.position, transform.rotation, transform) as CharacterInfoObject;
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        newItem.Character = characterObject;
        newItem.Description.supportRichText = true;
        newItem.Description.text = characterObject.Name + "\n" + "<color=red>Health:</color>" + characterObject.getStat("Health");

        logItemList.Add(characterObject.CharID, newItem);

    }

}
