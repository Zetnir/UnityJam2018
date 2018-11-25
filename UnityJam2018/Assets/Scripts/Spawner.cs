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
    public GameObject Ghost;

    public List<GameObject> biomeObstacles = new List<GameObject>();
    public Material spawningMaterial;


    //Variables de transition
    public int transitionPlateformNb;
    private bool inTransition;
    public GameObject transitionPlatform;

    //File paths
    public string filePathSnow;
    public string filePathPlain;
    public string filePathDesert;

    /*Gains pour le spawn*/
    public int maxModuloValue;
    private int primaryValue;
    public int position;
    private int lastObjectSize;

   

    //Instance
    public static Spawner instance;

    // Use this for initialization
    void Start ()
    {
        if (!instance)
            instance = this;

        position = 0;
        Random.InitState(Mathf.FloorToInt(Time.deltaTime * 1999));

        OnBiomeChange();

        PlateformSpawn();
        MovingObjectSpawn();
       
    }
	
	// Update is called once per frame
	void Update ()
    {


    }



    //Change le materiel actuel et la liste du prefab en fonction du biome
    public void OnBiomeChange()
    {
        string path = "";

        switch (Player.nextBiome)
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
        if (other.tag.Contains("Ghost"))
            MovingObjectSpawn();
        else if (other.tag.Contains("Platform"))
        {
            PlateformSpawn();
            Debug.Log("Spawn Platform");
        }


    }

    private void MovingObjectSpawn()
    {

        GameObject spawnGhost = Instantiate(Ghost, movingObjectsSpawnPoints[0].position, Quaternion.identity);
        //Tant qu'il y a de la place sur la plateforme
        while (position < 5)
        { 
            //Primary value corresponds a la variable aleatoire

            primaryValue =  Random.Range(0, maxModuloValue);
            GameObject g = ObjectToSpawn(primaryValue);
            Debug.Log(position);
            Transform t= movingObjectsSpawnPoints[position];

            //Verifier qu'on spawn un vrai objet
            if (g)
            {   //Pour gerer les differents ofset de position sur la plateforme, offset de 1 quand cest un objet de taille 3 
                if (lastObjectSize == 3 && position < 2)
                {
                    t = movingObjectsSpawnPoints[position + 1];
                    GameObject c = Instantiate(g, t.position, Quaternion.identity);
                    position += 3;
                }
                else if (lastObjectSize == 2 && position < 3) //Faire la moyene des deux points si c'est un objet de taille 2
                {
                    Vector3 newPosition = new Vector3(((movingObjectsSpawnPoints[position].position.x + movingObjectsSpawnPoints[position + 1].position.x) / 2), movingObjectsSpawnPoints[position].position.y, movingObjectsSpawnPoints[position].position.z);

                    GameObject c = Instantiate(g, newPosition, Quaternion.identity);
                    position += 2;
                }
                else if (lastObjectSize == 1)
                {
                    GameObject c = Instantiate(g, t.position, Quaternion.identity);
                    position++;
                }
               


            }
            else
                position++;


        }
        position = 0;


    }

    //Permet de generer a partir du nombre aleatoire l'objet a faire apparaitre
    private GameObject ObjectToSpawn(int value)
    {   //Value corresponds a un nombre aleatoire entre 0 et MaxModuloValue 

        //Retourne un objet de taille 1 
        if (value <= 1)
        {
            Debug.Log("Returning :" + biomeObstacles[0].name);
            
            lastObjectSize = 1;
            return biomeObstacles[0];

        }
        else if (value <= 3)
        {   //Retourne un objet de taille 2 en 3
            Debug.Log("Returning :" + biomeObstacles[1].name);
           
            lastObjectSize = 2;
            return biomeObstacles[1];

        }
        else if (value <= 5)
        {
            //Retourne un objet de taille 3
            Debug.Log("Returning :" + biomeObstacles[2].name);
            lastObjectSize = 3;
            return biomeObstacles[2];
        }
        else
        {
            Debug.Log("Returning : Nothing" );
            //Retourne un objet vide de taille 1
            lastObjectSize = 1;
           
            return null;
        }
        
    
    } 

    //Spawn des plateformes
    private void PlateformSpawn()
    {
        if (inTransition)
        {
            GameObject g = Instantiate(transitionPlatform, transform.position, Quaternion.identity);
            inTransition = false;
        }
        else
        { 
            GameObject g = Instantiate(plateforme, transform.position, Quaternion.identity);
            g.GetComponent<Renderer>().materials[0] = spawningMaterial;

        }
    }
}
