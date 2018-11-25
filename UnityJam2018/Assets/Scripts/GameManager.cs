using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Gere le jeu en fonction de la phase actuelle
        -Menu : Lance le UI du menu 
        -InGame : Lance le jeu, affichage le UI du jeu et spawn des plateforms et des obstacles en continu de façon
        aléatoire avec changement de biome 
        -Pause : Met le jeu sur pause en mettant le time scale à 0 et affiche le UI du menu pause
        -EndGame : Met le jeu sur pause en mettant le time scale à 0 et affiche le UI de fin de jeu

*/

public class GameManager : MonoBehaviour {

    public enum Phase
    {
        Menu,
        InGame,
        Pause,
        EndGame
    }
    public Phase currentPhase;
    public static GameManager instance;

    float timeScaleDecrease = 5f;
    float timeScaleIncrease = 2f;
    public float currentTimeScale;

	// Use this for initialization
	void Start () {
        if (!instance)
            instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        dispatchPhases();
        currentTimeScale = Time.timeScale;
	}

    void dispatchPhases()
    {
        switch(currentPhase)
        {
            case Phase.Menu:
                break;

            case Phase.InGame:
                //Si le jeu est en cours boucle la fonction pauseGame jusqu'a ce que le jeu s'arrete et se met en pause
                if (!Input.GetMouseButton(0) && Time.timeScale != 0) //Input.touchCount < 1
                    PauseGame();
                break;

            case Phase.Pause:
                //Si le jeu est en pause boucle la fonction resumeGame jusqu'a ce que le jeu reprenne
                if (Input.GetMouseButton(0) && Time.timeScale != 1) //Input.touchCount > 0
                    ResumeGame();
                break;

            case Phase.EndGame:
                break;
        }
    }

    void PauseGame()
    {
        if (Time.timeScale > 0.1)
        {
            Time.timeScale -= Time.deltaTime * timeScaleDecrease;
            SoundManager.instance.VolumeDown();
        }
        else
        {
            Time.timeScale = 0;
            UIManager.instance.SetPauseUI();
            currentPhase = Phase.Pause;
        }

    }

    void ResumeGame()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 0.1f;

        if (Time.timeScale < 1)
        {
            SoundManager.instance.VolumeUp();
            Time.timeScale += Time.deltaTime * timeScaleIncrease;
        }
        else
        {
            Time.timeScale = 1;
            UIManager.instance.SetInGameUI();
            currentPhase = Phase.InGame;
        }
    }

    public void EndTheGame()
    {
        //Effet de camera zoom sur le player + Rotation
        //A completer

        //Effet de ralenti sur le jeu
        Time.timeScale = 0.1f;
    }
}
