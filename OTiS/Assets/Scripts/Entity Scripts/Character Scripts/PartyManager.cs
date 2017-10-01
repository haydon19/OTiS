using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager  {

    //Party members
    public List<Character> partyMembers;
    int supplies;
    int fuel;
    public SpaceShip ship;
    public Dictionary<string, int> partyStats;
    //Inventory?
    //Cargo?

    public PartyManager()
    {
        partyMembers = new List<Character>();
        ship = new SpaceShip("The Aloha", 10);
        foreach(KeyValuePair<string, int> item in ship.ShipResources)
        {
            if(PartyInfoPanel.instance != null)
            PartyInfoPanel.instance.setStat(item.Key, item.Value.ToString());
        }
        ShipInfoPanel.instance.CurrentShip = ship;
        ShipInfoPanel.instance.updateCurrentShip();

        supplies = 5;


    }
    
    
    
    public void EventTick()
    {
        if(ship.ToRegen < ship.RegenRate)
        ship.ToRegen++;

        if (ship.getStat("Shields") < ship.getStat("MaxShields") && ship.ToRegen >= ship.RegenRate)
        {
            ship.Regenerate();
            ship.ToRegen = 0;
        }

        /*
        if(ship.getStat("Shields") < ship.getStat("MaxShields") && ship.ToRegen >= ship.RegenRate)
        {
            ship.changeStat("Shields", 2);
            if (ship.getStat("Shields") > ship.getStat("MaxShields"))
                ship.setStat("Shields", ship.getStat("MaxShields"));
            ship.ToRegen = 0;
        }
        */
    }



    public void addPartyMember()
    {
        Character temp;
        temp = new Character(GameControllerScript.instance.getRandomName(), GameData.instance.getRandomRace(), GameData.instance.getRandomGender(), GameData.instance.nextCharID(), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        partyMembers.Add(temp);
        //CharacterContainer.instance.addCharacter(temp);
    }

    public void addPartyMember(Character character)
    {
        partyMembers.Add(character);
        CharacterIconsPanel.instance.addCharacter(character);
        //CharacterContainer.instance.addCharacter(character);

    }

    public List<Character> getParty()
    {
        return partyMembers;
    }


    public void changeShipStat(string stat, int change)
    {

        ship.changeStat(stat, change);
        if (PartyInfoPanel.instance != null)
        PartyInfoPanel.instance.setStat(stat, ship.getStat(stat).ToString());
        ShipInfoPanel.instance.updateCurrentShip();
    }

    public void updateShipStat(string stat)
    {
        PartyInfoPanel.instance.setStat(stat, ship.getStat(stat).ToString());
        ShipInfoPanel.instance.updateCurrentShip();
    }

    //FOR ALL FUEL RELATED THINGS
    public void addFuel(int num)
    {
        fuel += num;
    }

    public void loseFuel(int num)
    {
        if(fuel > 0){
            fuel -= num;
        }
    }

    //For all ship related things

    public string getShip()
    {
        return ship.ToString();
    }

    
    public void setShip(SpaceShip newShip)
    {
        ship = newShip;
    }
}
