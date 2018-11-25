using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    /*
     Possede une vitesse 
     Existe sur un gameObject avec un collider
    */


    public static float speed = 7;

    // Use this for initialization
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destroyer")
            Destroy(gameObject);
    }
}
