using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectIngredient : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Ingredient"){
            Debug.Log(collision.gameObject.name + " Hit the bowl");
            GameObject ing = collision.gameObject;
            //Debug.Log("collision game object: " + ing + "ingredient check:" + IngredientChecker.Instance);
            IngredientChecker.Instance.CheckIngredient(ing);
        }
    }
}
