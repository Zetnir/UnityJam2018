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

	// Use this for initialization
	void Start () {
        if (!instance)
            instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        dispatchPhases();
	}

    void dispatchPhases()
    {
        switch(currentPhase)
        {
            case Phase.Menu:
                UIManager.instance.SetMenuUI();
                break;

            case Phase.InGame:
                UIManager.instance.SetInGameUI();
                break;

            case Phase.Pause:
                UIManager.instance.SetPauseUI();
                break;

            case Phase.EndGame:
                UIManager.instance.SetEndGameUI();
                break;
        }
    }
}
