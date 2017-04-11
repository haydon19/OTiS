using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITraitObject : MonoBehaviour {

    public Text traitName;
    public Toggle traitToggle;
    // Use this for initialization

    void Awake()
    {

        traitName = GetComponentInChildren<Text>();
        traitToggle = GetComponentInChildren<Toggle>();

    }
}
