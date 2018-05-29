using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationPanel : MonoBehaviour {
    public InputField characterNameField;
    public StatSetterPanel startingStats;
    
    public Character player;
    public Image characterPortrait;
    public Dropdown characterRace, characterGender;
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
        stats.Add("Mind");
        stats.Add("Agility");
        stats.Add("Piloting");
        startingStats.initStats(stats);
        characterRace.onValueChanged.AddListener(CharacterRaceChanged);
        

    }

    public void CharacterRaceChanged(int raceValue)
    {
        //Debug.Log(characterRace.options[raceValue].text);
        characterPortrait.sprite = GameData.instance.characterPortraitDictionary[characterRace.options[characterRace.value].text];
    }

    private void OnEnable()
    {

    }

    public void CreateCharacter()
    {
        Debug.Log(characterGender.options[characterGender.value].text);
        Character temp = new Character(characterNameField.text,
            characterRace.options[characterRace.value].text,
            characterGender.options[characterGender.value].text,
            GameData.instance.nextCharID(),
            startingStats.StatObjectList["Strength"].statValue,
            startingStats.StatObjectList["Mind"].statValue,
            startingStats.StatObjectList["Agility"].statValue,
            startingStats.StatObjectList["Piloting"].statValue);

        GameData.instance.party.Add(temp);


        partyMembersPanel.addCharacter(temp);
        //GameData.instance.player1.addTrait();

    }


}
