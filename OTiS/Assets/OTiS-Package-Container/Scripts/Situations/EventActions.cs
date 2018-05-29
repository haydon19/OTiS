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
	
	public static void gainRandomResource(Situation activeEvent)
    {
        string randomResource = getRandomResource();
        int amount = randomAmounts[Random.Range(0, randomAmounts.Count)];

        
        GameControllerScript.instance.party.changeShipStat(randomResource, amount);
        activeEvent.LogEntry("The party has gained " + amount + " " + randomResource + ".");
    }

    public static void loseRandomResource(Situation activeEvent)
    {
        string randomResource = getRandomResource();
        int amount = randomAmounts[Random.Range(0, randomAmounts.Count)];


        GameControllerScript.instance.party.changeShipStat(randomResource, -amount);
        activeEvent.LogEntry("The party loses " + amount + " " + randomResource + ".");
    }

    public static void loseResource(Situation activeEvent, string resource)
    {
        int amount = randomAmounts[Random.Range(0, randomAmounts.Count)];


        GameControllerScript.instance.party.changeShipStat(resource, -amount);
        activeEvent.LogEntry("The party loses " + amount + " " + resource + ".");
    }

    public static void gainResource(Situation activeEvent, string resource)
    {
        int amount = randomAmounts[Random.Range(0, randomAmounts.Count)];


        GameControllerScript.instance.party.changeShipStat(resource, -amount);
        activeEvent.LogEntry("The party loses " + amount + " " + resource + ".");
    }


}
