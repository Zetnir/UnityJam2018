using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    //UI objects for the Menu
    public GameObject MenuUI;
    public Slider SliderMenuUI;


    public GameObject PauseUI;
    public GameObject InGameUI;


    public GameObject EndGameUI;
    public Slider ExitSliderUI;
    public Slider ReplaySliderUI;



    // Use this for initialization
    void Start () {
        if (!instance)
            instance = this;

        SetMenuUI();
	}
	
	// Update is called once per frame
	void Update () {

	}


    //Affiche le UI du menu
    public void SetMenuUI()
    {
        EndGameUI.SetActive(false);
        MenuUI.SetActive(true);
    }

    public void ResetMenuUI()
    {
        SliderMenuUI.value = SliderMenuUI.minValue;
        SliderMenuUI.interactable = true;
    }

    //Affiche le UI de pause
    public void SetPauseUI()
    {
        InGameUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    //Affiche le UI inGame
    public void SetInGameUI()
    {
        MenuUI.SetActive(false);
        PauseUI.SetActive(false);
        EndGameUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    //Affiche le UI de fin de jeu
    public void SetEndGameUI()
    {
        InGameUI.SetActive(false);
        EndGameUI.SetActive(true);
    }

    public void ResetEndGameUI()
    {
        ReplaySliderUI.value = SliderMenuUI.minValue;
        ReplaySliderUI.interactable = true;
        ExitSliderUI.value = SliderMenuUI.minValue;
        ExitSliderUI.interactable = true;
    }
}
