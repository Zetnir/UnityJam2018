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

    
    //Liste des transform pour les spawns
    public Transform [] movingObjectsSpawnPoints;
    public Transform plateformTransform;

    //Prefab de la plateform
    public GameObject plateforme;

    public List<GameObject> biomeObstacles = new List<GameObject>();
    public Material spawningMaterial;


    //Nombre de plateforme de transition
    public int transitionPlateformNb;


    //File paths
    public string filePathSnow;
    public string filePathPlain;
    public string filePathDesert;

    /*Gains pour le spawn*/
    public int maxModuloValue;
    private int primaryValue;
    private int position;
    private int lastObjectSize;

    // Use this for initialization
    void Start ()
    {


        position = 0;
        Random.InitState(Mathf.FloorToInt(Time.deltaTime * 1999));


        PlateformSpawn();

        OnBiomeChange();
       
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        


	}



    //Change le materiel actuel et la liste du prefab en fonction du biome
    public void OnBiomeChange()
    {
        string path = "";

        switch (Player.currentBiome)
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

        //Import de la texture et conversion en material
        Object[] TempMaterialList = Resources.LoadAll(path, typeof(Material));
        spawningMaterial = TempMaterialList[0]as Material;

        //Import des props et conversion en GameObjects
        Object[] tempList = Resources.LoadAll(path, typeof(GameObject));
        foreach(object obj in tempList)
        {
            biomeObstacles.Add(obj as GameObject);
        }


    }

    //Gere le spawn on collider exit
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Obstacles"))
            MovingObjectSpawn();
        else if (other.tag.Contains("Plateform"))
            PlateformSpawn();


    }

    private void MovingObjectSpawn()
    {
        
        //Tant qu'il y a de la place sur la plateforme
        while (position < 5)
        { 
            //Primary value corresponds a la variable aleatoire

            primaryValue =  Random.Range(0, maxModuloValue);
            GameObject g = ObjectToSpawn(primaryValue);
            Transform t= movingObjectsSpawnPoints[position];

            //Verifier qu'on spawn un vrai objet
            if (g)
            {   //Pour gerer les differents ofset de position sur la plateforme, offset de 1 quand cest un objet de taille 3 
                if (lastObjectSize == 3)
                    t = movingObjectsSpawnPoints[position + 1];
                else if (lastObjectSize == 2) //Faire la moyene des deux points si c'est un objet de taille 2
                    t.SetPositionAndRotation(new Vector3(((movingObjectsSpawnPoints[position].position.x + movingObjectsSpawnPoints[position+1].position.x) /2), movingObjectsSpawnPoints[position].position.y, movingObjectsSpawnPoints[position].position.z), movingObjectsSpawnPoints[position].rotation);
                Instantiate(g,t);
            }


        }



    }

    //Permet de generer a partir du nombre aleatoire l'objet a faire apparaitre
    private GameObject ObjectToSpawn(int value)
    {

        //Retourne un objet de taille 1 
        if (value <= maxModuloValue - 7 - position)
        {
            position++;
            lastObjectSize = 1;
            return biomeObstacles[0];

        }
        else if (value <= maxModuloValue - 5 - position)
        {   //Retourne un objet de taille 2
            position += 2;
            lastObjectSize = 2;
            return biomeObstacles[1];

        }
        else if (value <= maxModuloValue - 3 - position)
        {
            //Retourne un objet de taille 3
            position += 3;
            lastObjectSize = 3;
            return biomeObstacles[2];
        }
        else
        {
            //Retourne un objet vide de taille 1
            lastObjectSize = 1;
            position++;
            return null;
        }
        
    
    } 




    //Spawn des plateformes
    private void PlateformSpawn()
    {
        GameObject g = Instantiate(plateforme, transform.position, Quaternion.identity);
        g.GetComponent<Renderer>().materials[0] = spawningMaterial;
    }

}
