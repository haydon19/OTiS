using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMembersContainer : MonoBehaviour {



    public PartyMemberIconObject proto;
    Dictionary<string, PartyMemberIconObject> partyMemberObjects = new Dictionary<string, PartyMemberIconObject>();
    public static PartyMembersContainer instance;

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


    public void addCharacter(Character characterObject)
    {
        PartyMemberIconObject newItem = Instantiate(proto, transform.position, transform.rotation, transform) as PartyMemberIconObject;
        newItem.Description.text = characterObject.Name;
        newItem.Sprite.sprite = GameData.instance.characterPortraitDictionary[characterObject.Race];
        partyMemberObjects.Add(characterObject.CharID, newItem);


    }
}
