using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderMenu : MonoBehaviour {

    //Pour avoir le slider courant
    Slider mySlider;

    //La vitesse a laquelle la valeur du slider diminue
    float decreaseSpeed = 0.5f;

	// Use this for initialization
	void Start () {
        mySlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        DecreaseSlider();
	}

    //Diminue le slider si il n'est pas toucher et si il est diférent de de la valeur max du slider
    void DecreaseSlider()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        if (mySlider.value != mySlider.maxValue)
        {
            //Decrease uniquement si la valeur minimum n'est pas atteinte
            if (mySlider.value != mySlider.minValue)
                mySlider.value -= decreaseSpeed * Time.deltaTime;
        }
        else
        {
            //Lance le jeu quand atteint le max et désactive l'interaction avec l'objet
            //Affiche aussi le UI du jeu
            mySlider.interactable = false;
            GameManager.instance.currentPhase = GameManager.Phase.InGame;
            UIManager.instance.SetInGameUI();
        }

    }

    //Si on retourne au menu reset les valeurs du slider
    public void ResetSlider()
    {
        mySlider.value = mySlider.minValue;
        mySlider.interactable = true;
    }
}
