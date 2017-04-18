using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationPanel : MonoBehaviour {
    public InputField characterNameField;
    public StatSetterPanel startingStats;
    public Character player;
    public const int DEFAULT_STAT_VALUE = 5;
    public const int MAX_STAT_VALUE = 99;

    public static CharacterCreationPanel instance;
    public PartyMembersContainer partyMembersPanel;

    private void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

    }

    private void Start()
    {
        List<string> stats = new List<string>();
        stats.Add("Strength");
        stats.Add("Smarts");
        stats.Add("Agility");
        stats.Add("Piloting");
        startingStats.initStats(stats);

    }

    private void OnEnable()
    {

    }

    public void CreateCharacter()
    {

        Character temp = new Character(characterNameField.text,
            GameData.instance.nextCharID(),
            int.Parse(startingStats.StatObjectList["Strength"].statValue.text),
            int.Parse(startingStats.StatObjectList["Smarts"].statValue.text),
            int.Parse(startingStats.StatObjectList["Agility"].statValue.text),
            int.Parse(startingStats.StatObjectList["Piloting"].statValue.text));

        GameData.instance.party.Add(temp);


        partyMembersPanel.addCharacter(temp);
        //GameData.instance.player1.addTrait();

    }


}
