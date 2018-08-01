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
    public GameObject checkMark;

    public GameObject[][] pizza = new GameObject[3][];
    public GameObject[] step1 = new GameObject[2];
    public GameObject[] step2 = new GameObject[2];
    public GameObject[] step3 = new GameObject[2];
    [HideInInspector]
    public int[,] recipeProgress = new int[3,2];

    //Spawn pos
    public Transform step1_spawn1;
    public Transform step1_spawn2;
    public Transform step2_spawn1;
    public Transform step2_spawn2;
    public Transform step3_spawn1;
    public Transform step3_spawn2;

    List<GameObject> recipeBoard = new List<GameObject>(); //list of gameobjects physically on the recipe board



    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }


    // Later implementation: allow user to select 1 of 2 recipes
	void Start () {
        
        for (int k = 0; k < 3; k++){
            for (int j = 0; j < 2; j++){
                recipeProgress[k,j] = 0;
            }
        }

        // Initialize recipe
        pizza[0] = step1;
        pizza[1] = step2;
        pizza[2] = step3;

        // Initialize visual recipe on the board (this is a clunky way of doing it sorry lol)
        GameObject step1_1 = Instantiate(pizza[0][0], step1_spawn1.position, step1_spawn1.rotation);
        GameObject step1_2 = Instantiate(pizza[0][1], step1_spawn2.position, step1_spawn2.rotation);
        GameObject step2_1 = Instantiate(pizza[1][0], step2_spawn1.position, step1_spawn1.rotation);
        GameObject step2_2 = Instantiate(pizza[1][1], step2_spawn2.position, step1_spawn2.rotation);
        GameObject step3_1 = Instantiate(pizza[2][0], step3_spawn1.position, step1_spawn1.rotation);
        GameObject step3_2 = Instantiate(pizza[2][1], step3_spawn2.position, step1_spawn2.rotation);


        recipeBoard.Add(step1_1);
        recipeBoard.Add(step1_2);
        recipeBoard.Add(step2_1);
        recipeBoard.Add(step2_2);
        recipeBoard.Add(step3_1);
        recipeBoard.Add(step3_2);
	}
	
	
	void Update () {
		
	}

    public void CheckIngredient(GameObject ingredient){
        bool found = false;
        Debug.Log("Current Step: " + currentStep);
        for (int i = 0; i < pizza[currentStep].Length; i++){
            if(ingredient = pizza[currentStep][i]){
                Debug.Log("correct ingredient at position " + i);
                CrossOffIngredient(currentStep, i);
                found = true;
            }
            if (found == true)
                break;
        }
        if(found == false){
            wrongIngredients++;
            Debug.Log("Wrong ingredient!");
        }
    }

    void CrossOffIngredient(int step, int ingredient){

        recipeProgress[step,ingredient] = 1;

        int total = 0;
        for (int i = 0; i < 2; i++){
            if(recipeProgress[step,i] == 1){
                int listPos = step*2 + i; //0,0 is 0th, 0,1 is 1st, 1,0 2nd, 1,1 3rd, 2,0 4th

                // TODO: Visually cross off the ingredient on the board
                Instantiate(checkMark, recipeBoard[listPos].transform.position + (new Vector3(0,0,-1)), recipeBoard[listPos].transform.rotation);
                total++;
            }
        }
        Debug.Log("total correct in the step so far: " + total);
        if(total == 2){
            currentStep++;
            Debug.Log("Moved to step " + currentStep);
            if(currentStep > 2){
                currentStep = 2;

                // Call the win screen/lose screen
            }

        }
        Debug.Log("Ingredient crossed off for step " + currentStep);

    }
}
