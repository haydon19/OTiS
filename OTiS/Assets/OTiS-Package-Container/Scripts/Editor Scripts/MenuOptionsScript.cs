using UnityEditor;
using UnityEngine;

public class MenuOptionsScript {

    [MenuItem("Assets/Create/Situation/EncounterPlanet")]
    public static void PlanetEncounterAsset()
    {
        ScriptableObjectUtility.CreateAsset<EncounterPlanet>();
    }

    [MenuItem("Assets/Create/Situation/EncounterNPC")]
    public static void NPCEncounterAsset()
    {
        ScriptableObjectUtility.CreateAsset<EncounterNPC>();
    }

    [MenuItem("Assets/Create/Situation/EncounterShip")]
    public static void ShipEncounterAsset()
    {
        ScriptableObjectUtility.CreateAsset<EncounterShip>();
    }

    [MenuItem("Assets/Create/Situation/EncounterAsteroid")]
    public static void AsteroidEncounterAsset()
    {
        ScriptableObjectUtility.CreateAsset<EncounterAsteroid>();
    }

    [MenuItem("Assets/Create/Situation/ShipBattle")]
    public static void ShipBattleAsset()
    {
        ScriptableObjectUtility.CreateAsset<ShipBattle>();
    }

}
