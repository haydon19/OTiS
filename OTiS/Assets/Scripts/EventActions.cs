using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventActions  {

    static List<int> randomAmounts = new List<int> { 5, 10, 15, 20 };

    public static string getRandomStat()
    {
        
        return GameControllerScript.instance.characterStatNames[Random.Range(0, GameControllerScript.instance.characterStatNames.Count)];
    }

	public static string getRandomResource()
    {
        return GameControllerScript.instance.resourceNames[Random.Range(0, GameControllerScript.instance.resourceNames.Count)];
    }
	
	public static void gainRandomResource(Event activeEvent)
    {
        string randomResource = getRandomResource();
        int amount = randomAmounts[Random.Range(0, randomAmounts.Count)];

        
        GameControllerScript.instance.party.changeShipStat(randomResource, amount);
        activeEvent.Summary += "\nThe party has gained " + amount + " " + randomResource + ".";
    }

    public static void loseRandomResource(Event activeEvent)
    {
        string randomResource = getRandomResource();
        int amount = randomAmounts[Random.Range(0, randomAmounts.Count)];


        GameControllerScript.instance.party.changeShipStat(randomResource, -amount);
        activeEvent.Summary += "\nThe party loses " + amount + " " + randomResource + ".";
    }


}
