using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the recipe board
// When an ingredient collides with the bowl, this script is called
// If the ingredient matches one of those on the step, cross off the ingredient
// If the ingredient is wrong, increment the wrong ingredient counter

public class IngredientChecker : MonoBehaviour {

    public GameObject[][] pizza;
    public GameObject[] step1 = new GameObject[2];
    public GameObject[] step2 = new GameObject[2];
    public GameObject[] step3 = new GameObject[2];

	void Start () {
        // Later implementation: allow user to select 1 of 2 recipes

        // Initialize recipe
        pizza[0] = step1;
        pizza[1] = step2;
        pizza[2] = step3;

        for (int i = 0; i < pizza.Length; i++){
            
        }

        // Instantiate
	}
	
	
	void Update () {
		
	}

    void RemoveIngredient(GameObject ingredient){
        
    }

    void CheckIngredient(){
        
    }

    void CrossOffIngredient(){
        
    }

    void UpdateWrongIng(){
        
    }
}
