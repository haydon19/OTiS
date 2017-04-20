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
        ShipInfoPanel.instance.CurrentShip = ship;

        PartyInfoPanel.instance.setStat("Ship", ship.Name);
        PartyInfoPanel.instance.setStat("Fuel", ship.getStat("Fuel").ToString());
        PartyInfoPanel.instance.setStat("Shields", ship.getStat("Shields").ToString());

        supplies = 5;
        PartyInfoPanel.instance.setStat("Supplies", supplies.ToString());
        PartyInfoPanel.instance.setStat("Blast", ship.getStat("Blast").ToString());


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


    //FOR ALL SUPPLIES RELATED THINGS
    public void addSupplies(int num)
    {
        supplies += num;
    }

    public void loseSupplies(int num)
    {
        if (supplies > 0)
        {
            supplies -= num;
        }
    }

    public void changeShipStat(string stat, int change)
    {

        ship.changeStat(stat, change);
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
