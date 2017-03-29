using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public StartMenuScript StartMenu;


    public void OpenStartMenu()
    {
        StartMenu.gameObject.SetActive(!StartMenu.gameObject.activeSelf);
    }


}
