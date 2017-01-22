   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour {

    public MenuController menuController;

	// Use this for initialization
	void Start () {
        menuController = GetComponent<MenuController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            menuController.LoadScene("menuScene");
        }
	}
}
