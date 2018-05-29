using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterAsteroid : Situation
{

    public EncounterAsteroid(SituationTag type, int id) : base(type, id)
    {

    }

    public override void Initialize()
    {
        characters = new List<Character>();

        subject = "the rogue asteroid";
        ship = GameControllerScript.instance.party.ship;
        SetOptions();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionTag oType)
    {
        base.HandleEvent(oType);

        switch (oType)
        {
            case (OptionTag.Avoid):
                if (characters[0].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    if (randomSkillGain(characters[0], "Piloting"))
                    {

                        LogEntry("You seem to be at one with the controls of the ship, you feel your skills as a pilot grow!\n\n" + characters[0].Name + "'s Piloting increases! " + characters[0].getStat("Piloting"));


                    }
                    else
                    {
                        LogEntry("You race to the helm, and dive into the pilots seat in an effort to steer the ship clear of the asteroid. A narrow success.");
                        GameControllerScript.instance.party.ship.Damage(5);
                        //LogEntry(ship.Name + " has lost " + 5 + " shields.");
                        //GameControllerScript.instance.party.changeShipStat("Shields", -5);
                    }
                }
                else
                {

                    if (randomSkillLoss(characters[0], "Piloting"))
                    {
                        characters[0].changeStat("Piloting", -1);
                        LogEntry("You scrape against the asteriods hard exterior, a compartment of the ship holding ESSENTIAL supplies has been damaged and it's contents blown into space! Maybe you weren't meant to hang around the pilots seat...\n\n" + characters[0].Name + "'s Piloting decreases!  " + characters[0].getStat("Piloting"));
                    }
                    else
                    {
                        LogEntry("Your heroic dive to the pilots seat is misaligned, luckily it wasn't the hardest asteroid in the galaxy, and your ship destroys it with it's hull.");
                    }
                }
                GameControllerScript.instance.LightYearsToEOU -= 2;
                EventActions.loseResource(this, "Fuel");
                break;
            case (OptionTag.Blast):
                if (characters[1].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry("Ya fuckin blasted it bud.");
                }
                else
                {
                    LogEntry("Ya fucked up blasting it.");
                }
                GameControllerScript.instance.LightYearsToEOU -= 1;
                EventActions.loseResource(this, "Fuel");
                break;
            case (OptionTag.Land):
                if (characters[2].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[2].Name + " has successfully landed on the Asteroid, allowing the party to scavange for resources.");
                    EventActions.gainRandomResource(this);
                    EventActions.gainRandomResource(this);
                    GameControllerScript.instance.locationState = LocationState.Land;
                }
                else
                {
                    LogEntry(characters[2].Name + " misjudges the landing and the ship scapes along the surface of the asteroid. The party decides to cut their losses and not try again.");
                    GameControllerScript.instance.party.ship.Damage(5);
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                    EventActions.loseResource(this, "Fuel");
                }

                break;
        }

        GameControllerScript.instance.currentSituation = null;

    }

    public override void SetSummary()
    {
        summary = "You are travelling through space... A rogue asteroid appears!";
    }

}