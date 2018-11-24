using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    /*
     Etat initial avec une section pregenerer
     S'occupe de gerer le spawn des diferents objets et des plateformes de jeu
     Creation lorsque trigger Exit 
     Condition de creation = un objet a ete detruit
     Gere la liste des prefabs
     Gere le choix aleatoire et le placement sur les plateformes

    Possede:
               Les differentes positions possibles (gameObject qui servent de spawnPoints)
               Les differents prefab en fonction des biomes
               Les différentes variables pour contenir l'effet aleatoire
               Le nombre de plateforme de transitions

    Fonctions:  
               PlatteformSpawn()
               MovingObjectSpawn()
                
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     SET LES DIFFERENTES PLATEFORMES (MEME PREFAB MAIS TEXTURE QUI EST SET EN FONCTION DU BIOME)
     
     
     SpawnOnTriggerExit du collider correspondant (de meme taille que la plateforme)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     SET & SPAWN LES DIFFERENTS OBJETS (LISTE DE PREFAB EN FONCTION DU BIOME) 
    
    

    */

    

    public GameObject[] movingObjectsSpawnPoints;
    public Transform plateformTransform;

    public GameObject plateforme;

    private List<GameObject> biomeObstacles = new List<GameObject>();
    private Material spawningMaterial;

    public int transitionPlateformNb;

    public string filePathSnow;
    public string filePathPlain;
    public string filePathDesert;

 



    // Use this for initialization
    void Start ()
    {



        OnBiomeChange();
       
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        


	}




    public void OnBiomeChange()
    {
        string path = "";

        switch (Player.biomeActuel)
        {
            case Player.Biomes.snow:
                path = filePathSnow;
                    break;
                

            case Player.Biomes.plain:
                path = filePathPlain;
                break;

            case Player.Biomes.desert:
                path = filePathDesert;
                break;

            default:
                break;

        }


        if (biomeObstacles.Count > 0)
            biomeObstacles.Clear();

        Material m = Resources.Load<Material>(path);
        GameObject o = Resources.Load<GameObject>(path);
        GameObject[] tempList = o.GetComponentsInChildren<GameObject>();


        spawningMaterial = m;
        foreach (GameObject i in tempList)
        {
            biomeObstacles.Add(i);
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Obstacles"))
            MovingObjectSpawn();
        else if (other.tag.Contains("Plateform"))
            PlateformSpawn();


    }

    private void MovingObjectSpawn()
    {







    }

    private void PlateformSpawn()
    {
        GameObject g = Instantiate(plateforme, plateformTransform);
        MeshRenderer r = g.GetComponent<MeshRenderer>();
        r.material= spawningMaterial;
       
    }

}
