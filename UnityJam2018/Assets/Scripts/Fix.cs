using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix : MonoBehaviour {


    int platteform = 0;

	// Use this for initialization
	void Start () {
        platteform = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (platteform >= 4)
        { 
            Spawner.instance.MovingObjectSpawn();
            platteform = 0;
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Obstacle")
        {
            platteform = 0;
        }
        if (other.tag == "Platform")
        {
            platteform++;
        }



    }
}
