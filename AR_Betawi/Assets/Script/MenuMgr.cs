using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMgr : MonoBehaviour {
    public GameObject mainMenu;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartAR()
    {
        mainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
    }

}
