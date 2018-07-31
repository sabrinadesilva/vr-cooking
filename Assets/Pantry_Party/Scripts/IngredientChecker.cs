using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the recipe board
// When an ingredient collides with the bowl, this script is called
// If the ingredient matches one of those on the step, cross off (1) the ingredient
// If the ingredient is wrong, increment the wrong ingredient counter

public class IngredientChecker : MonoBehaviour {

    public static IngredientChecker Instance;

    [HideInInspector]
    public int currentStep = 0; // Which step of the recipe we're on

    [HideInInspector]
    public int wrongIngredients = 0; // How many wrong ingredients have been thrown
                                     // Will be displayed on the recipe board

    public GameObject[][] pizza;
    public GameObject[] step1 = new GameObject[2];
    public GameObject[] step2 = new GameObject[2];
    public GameObject[] step3 = new GameObject[2];
    [HideInInspector]
    public int[][] recipeProgress;

    // Later implementation: allow user to select 1 of 2 recipes
	void Start () {

        // Initialize recipe step progress
        // 0 means incomplete, 1 means complete
        for (int i = 0; i < recipeProgress.Length; i++){
            recipeProgress[i] = new int[2];
        }
        for (int k = 0; k < recipeProgress.Length; k++){
            for (int j = 0; j < recipeProgress[k].Length; j++){
                recipeProgress[0][0] = 0;
            }
        }

        // Initialize recipe
        pizza[0] = step1;
        pizza[1] = step2;
        pizza[2] = step3;

	}
	
	
	void Update () {
		
	}

    void RemoveIngredient(GameObject ingredient){
        
    }

    public void CheckIngredient(GameObject ingredient){
        bool found = false;
        for (int i = 0; i < pizza[currentStep].Length; i++){
            if(ingredient = pizza[currentStep][i]){
                CrossOffIngredient(currentStep, i);
                found = true;
            }
        }
        if(found == false){
            wrongIngredients++;
            Debug.Log("Wrong ingredient!");
        }
    }

    void CrossOffIngredient(int step, int ingredient){
        recipeProgress[step][ingredient] = 1;
        if(ingredient == recipeProgress[step].Length){
            currentStep++;
            Debug.Log("Moved to next step");
        }
        Debug.Log("Ingredient crossed off");
        // TODO: Visually cross off the ingredient on the board
    }
}
