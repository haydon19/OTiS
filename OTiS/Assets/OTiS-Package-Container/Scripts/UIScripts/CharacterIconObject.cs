using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIconObject : MonoBehaviour {

    public Image portrait;
    public Text charName;
    public Text charHealth;

    public void Init()
    {
        List<Text> results = new List<Text>();
        GetComponentsInChildren<Text>(results);

        foreach (Text t in results)
        {
            if (t.name == "Character_Name")
            {
                charName = t;
            }

            if (t.name == "Character_Health")
            {
                charHealth = t;
            }
        }


        List<Image> images = new List<Image>();
        GetComponentsInChildren<Image>(images);

        foreach (Image b in images)
        {
            if (b.name == "Character_Portrait")
            {
                portrait = b;
            }

        }

        

    }


}
