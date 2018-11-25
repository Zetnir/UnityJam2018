using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject myCam;

    public Vector3 MenuPos;
    public Vector3 InGamePos;
    public Vector3 EndGamePos;

    public Vector3 MenuRot;
    public Vector3 InGameRot;
    public Vector3 EndGameRot;

    public bool cameraInGame = false;

    public float speedRotation = 0.01f;

    public static CameraManager instance;
    // Use this for initialization
	void Start () {
        myCam = gameObject;
        if (!instance)
            instance = this;

        myCam.transform.rotation = Quaternion.Euler(MenuRot);
        myCam.transform.position = MenuPos;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.currentPhase == GameManager.Phase.Menu && cameraInGame)
        {
            SetCameraToGame();
        }
	}

    public void SetCameraToGame()
    {
        if (myCam.transform.rotation != Quaternion.Euler(InGameRot))
        {
            Quaternion target = Quaternion.Euler(InGameRot);
            myCam.transform.rotation = Quaternion.Slerp(myCam.transform.rotation, target, speedRotation * Time.deltaTime);
        }
        else
        {
            //myCam.transform.rotation = Quaternion.Euler(InGameRot);
            cameraInGame = false;
            GameManager.instance.currentPhase = GameManager.Phase.InGame;
            Spawner.instance.gameObject.SetActive(true);

        }
    }
}
