using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterShip : Situation
{

    public EncounterShip(SituationTag type, int id) : base(type, id)
    {

    }

    SpaceShip enemyShip;
    public override void Initialize()
    {

        characters = new List<Character>();
        //This should be the enemy ship
        //ship = new SpaceShip(GameControllerScript.instance.getRandomShipName(), 5);
        enemyShip = new SpaceShip(GameControllerScript.instance.getRandomShipName(), 5);
        GameControllerScript.instance.enemyShip = this.enemyShip;
        subject = enemyShip.SubjectReference();
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
                    LogEntry("It's about to get spicy as " + characters[0].Name + " churns up the engines to the MAX. " + ship.Name + "'s thrusters fuckin' RIP OUT OF THERE.");
                    GameControllerScript.instance.LightYearsToEOU -= 3;
                    EventActions.loseResource(this, "Fuel");
                    enemyShip = null;
                }
                else
                {
                    LogEntry("Maybe " + ship.Name + " isn't as nimble as we thought, maybe " + characters[0].Name + " is just a shit pilot, only time will tell...");
                    GameControllerScript.instance.party.ship.Damage(5);
                    EventActions.loseResource(this, "Fuel");
                }
                break;
            case (OptionTag.Blast):
                if (characters[1].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(ship.Name + " fires at full power, blasting " + subject + " to bits.");
                    enemyShip = null;
                    EventActions.gainRandomResource(this);
                    GameControllerScript.instance.LightYearsToEOU -= 1;

                }
                else
                {
                    LogEntry(characters[1].Name + " misjudges the shot an leaves " + ship.Name + " vulnerable to counter fire.");
                    GameControllerScript.instance.party.ship.Damage(enemyShip.getStat("Blast"));
                }
                break;
            case (OptionTag.Board):
                if (characters[2].getStat("Piloting") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[2].Name + " boarded the enemy ship with ease! After a fierce battle on board you manage to steal some goods and make your way out.");
                    enemyShip = null;
                    EventActions.gainRandomResource(this);
                    EventActions.gainRandomResource(this);
                }
                else
                {
                    LogEntry("The boarding was botched! You'll have to come around to try again.");
                    EventActions.loseResource(this, "Fuel");
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                }
                break;
            case (OptionTag.Negotiate):
                if (characters[2].getStat("Mind") + Random.Range(0, 3) > 6)
                {
                    LogEntry(characters[2].Name + " convices the enemies to stand down.");
                    GameControllerScript.instance.LightYearsToEOU -= 1;

                }
                else
                {
                    LogEntry("The enemies decide not to attack however you must send them some resources as tribute.");
                    EventActions.loseRandomResource(this);
                    GameControllerScript.instance.LightYearsToEOU -= 1;
                }
                enemyShip = null;
                break;
        }

        Debug.Log(enemyShip);

        if (enemyShip == null)
        {
            
            GameControllerScript.instance.currentSituation = null;
        } else
        {
            GameControllerScript.instance.currentSituation = GameData.instance.shipBattle;
            GameControllerScript.instance.currentSituation.Initialize();
        }


    }

    public override void SetSummary()
    {
        summary = "You have encountered an enemy ship! On the side is it's name - " + subject;
    }

}