using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager  {

    //Party members
    public List<Character> partyMembers = new List<Character>();
    int supplies;
    int fuel;
    public SpaceShip ship;
    //Inventory?
    //Cargo?

    public void addPartyMember(Character character)
    {
        partyMembers.Add(character);
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
