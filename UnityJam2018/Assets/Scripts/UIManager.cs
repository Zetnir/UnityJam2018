using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
	// Use this for initialization
	void Start () {
        if (!instance)
            instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMenuUI()
    {

    }

    public void SetInGameUI()
    {

    }

    public void SetPauseUI()
    {

    }

    public void SetEndGameUI()
    {

    }
}
