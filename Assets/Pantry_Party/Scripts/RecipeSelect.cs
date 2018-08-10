using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelect : MonoBehaviour {
    //bool selected = false;
    // Use this for initialization

    public GameObject prefab;

	void Start () {
        
	}

    public void OpenGUI(){

        GameObject.Instantiate(prefab);
    }
}
