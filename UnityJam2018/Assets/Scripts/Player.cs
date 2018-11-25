﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/*
  
    Poser le script sur un gameObject (Le player)
    Classe Player, unique dans le jeu
    Gere le changement des meshs et des fs
    Optionel ramasse les objets bonus
    soccupe de calculer les points en fonction du temps de jeu courbe lineaire (int)
    Suis le input pour determiner la position sur la plaque (axe x uniquement)
    Animation de mort
     
     
     
*/



public class Player : MonoBehaviour {

    //Etat du joueur
    public bool alive;

    //Liste des biomes et biome actuel
    public enum Biomes {snow,plain,desert};
    public static Biomes currentBiome;
    public static Biomes nextBiome;
    //Mesh du gameObject en fonction des biomes 

    [Tooltip("Meshs du gameObject en fonction des biomes Dans l'ordre : Snow plain desert  ")]

    public Mesh[] playerMeshs;

    //Points accumules 
    public float points;
    public int multiplicateur;

    //Animation de mort
    public AnimationClip deathAnimation;

    //Pour pouvoir aller chercher la seule instance de la classe
    public static Player playerInstance;
    

    //Variables de taille de l'ecran
    private float width = Screen.width;
    private float height = Screen.height;
    public float ratioScreenScene;


    //Variables prives
    private MeshFilter meshFilter;

    //Debug variables 
    public Text pointsTxt;

	// Use this for initialization
	void Start ()
    {
        //Initialiser l'instance
        if (!playerInstance)
            playerInstance = this;

        //Set le mesh du joueur
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = playerMeshs[0];

        //Initialiser les autres variables
        points = 0;
        alive = true;

        currentBiome = Biomes.snow;
        nextBiome = currentBiome;
	}

   
    // Pour le calcul des points
    //Gagne de plus en plus de points en fonction du temps
    void Update () {

        if(alive)
            points += Time.deltaTime * multiplicateur;

        pointsTxt.text = Mathf.FloorToInt(points).ToString();




        //Déterminer le changement de phase
        if (points % 600 == 0)
        { 
            if (currentBiome == Biomes.desert)
                nextBiome = Biomes.snow;
            else
                nextBiome++;

            Spawner.instance.OnBiomeChange();
        }

        //Conditions en fonction de l'état du jeu 


    }

    //S'occupe du deplacement du joueur
    private void FixedUpdate()
    {
       if (Input.touchCount > 0 )
        {
            //Retourne la position du touch dans un vecteur 2D 
            Touch input = Input.GetTouch(0);

            //Calculer la nouvelle position de l'objet en fonction des dimentions de l'écran et du ratio entre l'ecran et la scene unity
            float newXPosition = (((input.position.x - (width / 2)) / (width / 2)) * ratioScreenScene);
           
            //Set la nouvelle position si dans les bornes  
            if(newXPosition <= 2 && newXPosition >= -2)
                transform.SetPositionAndRotation(new Vector3(newXPosition, transform.position.y, transform.position.z), transform.rotation) ;

        }



    }
    //Gerer le changement de mesh en fonction du nouveau biome dans le game manager
    public void OnBiomeChange(Biomes biome)
    {
        switch (biome)
        {
            case Biomes.snow:
                meshFilter.mesh = playerMeshs[0];
                break;
            case Biomes.plain:
                meshFilter.mesh = playerMeshs[1];
                break;
            case Biomes.desert:
                meshFilter.mesh = playerMeshs[2];
                break;

            default:
                break;
        }




    }
    //Lancer l'animation de fin de jeu
    public void OnDestroy()
    {
        
    }
    
  
    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            //Gere l'evenement lors de la collision avec un obstacle 
            case "Obstacle":
                alive = false;
                GameManager.instance.EndTheGame();
                //Destroy(gameObject,deathAnimation.length);
                break;
            //Gere l'evenement lors de la collision avec un marqueur de changement de biome
            case "Snow":
                OnBiomeChange(Biomes.snow);
                currentBiome = Biomes.snow;
                break;
            case "Desert":
                OnBiomeChange(Biomes.desert);
                currentBiome = Biomes.desert;
                break;
            case "Plain":
                OnBiomeChange(Biomes.plain);
                currentBiome = Biomes.plain;
                break;


            default:
                break;
        }

        
    }
}
