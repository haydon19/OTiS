using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSetterObject : MonoBehaviour {
    public Text statName;
    public Text statTextValue;
    public int statValue = 0;
    public GameObject buttonContainer;
    public Button incButton, decButton;
    public List<StatBonus> statBonuses;
    // Use this for initialization

    void Awake() {
        List<Text> results = new List<Text>();
        
        GetComponentsInChildren<Text>(results);

        foreach (Text t in results)
        {
            if (t.name == "StatName")
            {
                statName = t;
            }

            if (t.name == "StatValue")
            {
                statTextValue = t;
            }
        }

        buttonContainer = transform.Find("ButtonHolder").gameObject;

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
        statBonuses = new List<StatBonus>();
        incButton.onClick.AddListener(IncrementStat);
        decButton.onClick.AddListener(DecrementStat);
    }

    public void IncrementStat()
    {
        if (statValue < CharacterCreationPanel.MAX_STAT_VALUE)
        {
            UpdateStat(1);
        }
    }

    public void DecrementStat()
    {
        if (statValue > CharacterCreationPanel.DEFAULT_STAT_VALUE)
        {
            UpdateStat(-1);

        }

    }

    public void AddStatBonus(StatBonus sb){
        statBonuses.Add(sb);
        UpdateStat(sb.BonusValue);
    }

    public void RemoveStatBonus(StatBonus sb)
    {
        statBonuses.Remove(sb);
        UpdateStat(-sb.BonusValue);
    }

    public void UpdateStat(int value)
    {
        string s;
        string valueString;
        statValue += value;
        valueString = statValue.ToString();
        if (statBonuses.Count > 0)
        {
            
            s = "<color=green>value</color>";
            statTextValue.text = s.Replace("value", valueString);
        } else
        {
            statTextValue.text = valueString;
        }
    }
}
