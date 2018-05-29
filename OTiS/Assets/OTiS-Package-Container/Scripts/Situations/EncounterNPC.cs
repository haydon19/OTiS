using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterNPC : Situation
{

    public EncounterNPC(SituationTag type, int id) : base(type, id)
    {

    }

    public override void Initialize()
    {
        characters = new List<Character>();

        characters.Add(GameControllerScript.instance.createNPC());
        subject = characters[0].SubjectReference();
        SetOptions();

        EventPanelScript.instance.SetEvent(this);
    }

    public override void HandleEvent(OptionTag oType)
    {
        base.HandleEvent(oType);

        switch (oType)
        {
            case (OptionTag.Recruit):
                if (characters[1].getStat("Mind") + Random.Range(0, 10) > 6)
                {
                    LogEntry(characters[1].Name + " convices " + characters[0].Name + " to join the party with a triumphant speech.");
                    GameControllerScript.instance.party.addPartyMember(characters[0]);
                    LogEntry(characters[0].Name + " joins the party!");

                }
                else
                {
                    LogEntry("AWKWARD! " + characters[0].Name + " rejects the invitation from " + characters[1].Name + " to join the party.");
                }
                break;
            case (OptionTag.Gossip):

                LogEntry(characters[2].Name + " learns about a nearby planet rich with resources from " + characters[0].Name);
                EventActions.gainRandomResource(this);
                GameControllerScript.instance.LightYearsToEOU -= 2;
                break;
            case (OptionTag.Intimidate):
                if (characters[3].getStat("Strength") > Random.Range(0, 10))
                {
                    LogEntry(characters[0].Name + " backs away as " + characters[3].Name + " threatens them into giving up supplies.");
                    EventActions.gainRandomResource(this);
                }
                else
                {
                    LogEntry(characters[0].Name + " stands up to " + characters[3].Name + ", causing them to give up supplies in embarassment.");
                    EventActions.loseRandomResource(this);
                }
                break;
        }
        LogEntry("The crew fires up the ship and heads back into space.");
        GameControllerScript.instance.locationState = LocationState.Space;

        GameControllerScript.instance.currentSituation = null;

    }

    public override void SetSummary()
    {
        summary = "The party happens upon a mysterious stranger named " + characters[0].Name + ".";
    }
}

