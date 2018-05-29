using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPlanet : Situation
{

    public EncounterPlanet(SituationTag type, int id) : base(type, id)
    {

    }

    public override void Initialize()
    {
        characters = new List<Character>();

        //characters.Add();
        subject = "Planet X01";
        SetOptions();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionTag oType)
    {
        base.HandleEvent(oType);

        switch (oType)
        {
            case (OptionTag.Land):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[0].Name + " has successfully landed on " + subject + " allowing the party to scavange for resources.");
                    EventActions.gainRandomResource(this);
                    EventActions.gainRandomResource(this);
                    GameControllerScript.instance.locationState = LocationState.Land;
                }
                else
                {
                    LogEntry("As " + characters[0].Name + " descends into the atmosphere, an electrical storm knocks out the " + GameControllerScript.instance.party.ship.Name + "'s engines, causing it to crash.");
                    GameControllerScript.instance.party.ship.Damage(5);

                    EventActions.loseResource(this, "Fuel");
                    GameControllerScript.instance.locationState = LocationState.Land;
                }
                break;
            case (OptionTag.Scan):
                if (Random.Range(0, 100) > 60)
                {
                    LogEntry(characters[1].Name + " scans " + subject + " and determines it is rich with minerals.");
                    EventActions.gainRandomResource(this);
                }
                else
                {
                    LogEntry("The atmosphere of " + subject + " is creating too much interference to properly scan.");
                }
                EventActions.loseResource(this, "Fuel");
                break;
            case (OptionTag.Bypass):

                LogEntry(characters[2].Name + " guides the ship around the planet.");
                EventActions.loseResource(this, "Fuel");
                GameControllerScript.instance.LightYearsToEOU -= 2;
                break;

        }

        GameControllerScript.instance.currentSituation = null;

    }

    public override void SetSummary()
    {
        summary = "The party spots a planet nearby. A quick XooXle search reveals it's name - " + subject;
    }
}