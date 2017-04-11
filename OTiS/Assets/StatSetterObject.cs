using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSetterObject : MonoBehaviour {
    public Text statName;
    public Text statValue;
    public GameObject buttonContainer;
    public Button incButton, decButton;
	// Use this for initialization
	
	void Awake () {
        List<Text> results = new List<Text>();
        GetComponentsInChildren<Text>(results);

        foreach (Text t in results)
        {
            if(t.name == "StatName")
            {
                statName = t;
            }

            if (t.name == "StatValue")
            {
                statValue = t;
            }
        }

        buttonContainer =  transform.FindChild("ButtonHolder").gameObject;

        List<Button> buttons = new List<Button>();
        GetComponentsInChildren<Button>(buttons);

        foreach (Button b in buttons)
        {
            if (b.name == "AddButton")
            {
                incButton = b;
            }

            if (b.name == "SubtractButton")
            {
                decButton = b;
            }
        }

        incButton.onClick.AddListener(IncrementStat);
        decButton.onClick.AddListener(DecrementStat);
    }

    public void IncrementStat()
    {
        if (int.Parse(statValue.text) < CharacterCreationPanel.MAX_STAT_VALUE)
        {
            statValue.text = "" + (int.Parse(statValue.text) + 1);
        }
    }

    public void DecrementStat()
    {
        if (int.Parse(statValue.text) > CharacterCreationPanel.DEFAULT_STAT_VALUE)
        {
            statValue.text = "" + (int.Parse(statValue.text) - 1);
        }

    }

}
