using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIconsPanel : MonoBehaviour {

    

    private Character activeCharacter;
    public static CharacterIconsPanel instance;
    public Text activeCharacterLabel;

    public CharacterIconObject charIconProto;
    Dictionary<string, CharacterIconObject> charIconList = new Dictionary<string, CharacterIconObject>();

    public void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        activeCharacter = null;

    }

    public Character ActiveCharacter
    {
        get
        {
            return activeCharacter;
        }

        set
        {
            Character newActive = value;
            if (newActive != activeCharacter)
            {
                activeCharacter = value;
                updateActiveCharacter();
            }
        }
    }

    public void updateActiveCharacter()
    {
        /*
        activeCharacterLabel.text = activeCharacter.Name + "'s Stats";
        foreach (KeyValuePair<string, int> stat in activeCharacter.Stats)
        {
            Debug.Log("stat: " + stat + "activeCharacter: " + activeCharacter.Name + "key: " + stat.Key + "value: " + stat.Value);
            CharacterStatPanel.instance.setStat(stat.Key, stat.Value.ToString());
        }
        */

    }

    public void populateContainer()
    {


        foreach (Character p in GameControllerScript.instance.party.getParty())
        {
            if (charIconList.ContainsKey(p.CharID))
            {
                updateCharacterInfo(p);
            }
            else
            {
                addCharacter(p);
            }
        }
        //LogItem temp = newLogItem(eventObject.Summary);

    }


    public void updateCharacterInfo(Character characterObject)
    {
        CharacterIconObject charInfo = charIconList[characterObject.CharID];
        charInfo.charHealth.text = characterObject.getStat("Health") + " / " + "15";

    }

    public void removeCharacter(Character characterObject)
    {
        CharacterIconObject charInfo = charIconList[characterObject.CharID];
        charIconList.Remove(characterObject.CharID);
        Destroy(charInfo.gameObject);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void addCharacter(Character characterObject)
    {
        CharacterIconObject newItem = Instantiate(charIconProto, transform.position, transform.rotation, transform) as CharacterIconObject;
        //LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        newItem.Init();
        newItem.charName.text = characterObject.Name;
        newItem.portrait.sprite = GameData.instance.characterPortraitDictionary[characterObject.Race];
        newItem.charHealth.text = characterObject.getStat("Health") + " / " + "15";

        charIconList.Add(characterObject.CharID, newItem);

    }
}
