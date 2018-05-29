using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyInfoPanel : MonoBehaviour
{

    public CharacterInfoObject logItemProto;
    Dictionary<string, CharacterInfoObject> logItemList = new Dictionary<string, CharacterInfoObject>();
    public static EnemyInfoPanel instance;

    public void Start()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

    }

    public void addLogItem(Situation eventObject)
    {
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
        Debug.Log("BEING REMOVED - Name: " + characterObject.Name + " ID: " + characterObject.CharID);

    }

    public void newCharacter(Character characterObject)
    {
        CharacterInfoObject newItem = Instantiate(logItemProto, transform.position, transform.rotation, transform) as CharacterInfoObject;
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        newItem.Description.supportRichText = true;
        newItem.Description.text = characterObject.Name + "\n" + "<color=red>Health:</color>" + characterObject.getStat("Health");
        Debug.Log("Name: " + characterObject.Name + " ID: " + characterObject.CharID);
        logItemList.Add(characterObject.CharID, newItem);

    }

}
