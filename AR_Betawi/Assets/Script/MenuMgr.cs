using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMgr : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject petunjukMenu;
    public AudioSource backsound;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartAR()
    {
        mainMenu.SetActive(false);
        petunjukMenu.SetActive(false);
        backsound.Pause();
    }

    public void BackToMainMenuFromAR()
    {
        mainMenu.SetActive(true);
        petunjukMenu.SetActive(false);
        backsound.Play();
    }
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        petunjukMenu.SetActive(false);
        //backsound.Play();
    }
    public void OpenPetunjuk()
    {
        petunjukMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
