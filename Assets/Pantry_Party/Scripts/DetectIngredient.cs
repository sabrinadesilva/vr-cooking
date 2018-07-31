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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit the bowl");
        GameObject ing = collision.gameObject;
        IngredientChecker.Instance.CheckIngredient(ing);

        // TODO: debug any non-food item being detected as a collision
    }
}
