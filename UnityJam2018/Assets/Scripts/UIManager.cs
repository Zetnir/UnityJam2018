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



    // Use this for initialization
    void Start () {
        if (!instance)
            instance = this;

        SetMenuUI();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void MenuLaunchPlay()
    {
        GameManager.instance.currentPhase = GameManager.Phase.InGame;
        SetInGameUI();
    }

    public void SetMenuUI()
    {
        EndGameUI.SetActive(false);
        MenuUI.SetActive(true);
    }

    public void SetPauseUI()
    {
        InGameUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void SetInGameUI()
    {
        MenuUI.SetActive(false);
        PauseUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    public void SetEndGameUI()
    {
        InGameUI.SetActive(false);
        EndGameUI.SetActive(true);
    }

}
