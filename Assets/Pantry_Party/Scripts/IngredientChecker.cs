using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// Attached to the recipe board
// When an ingredient collides with the bowl, this script is called
// If the ingredient matches one of those on the step, cross off (1) the ingredient
// If the ingredient is wrong, increment the wrong ingredient counter

public class IngredientChecker : NetworkBehaviour// Extend this or MonoBehaviour?
{

    public static IngredientChecker Instance;

    public int maxWrongIngredients = 20;
    private bool pizzaChosen = true;
    public Text strikes;
    public GameObject checkMark;
    public GameObject confetti;
    public GameObject gamePlay;
    public GameObject winScreen;
    public GameObject loseScreen;



    // Pizza: Array holding the recipe steps and their ingredients
    // (Adjustable in inspector)
    public GameObject[][] pizza = new GameObject[3][];
    public GameObject[] step1 = new GameObject[2];
    public GameObject[] step2 = new GameObject[2];
    public GameObject[] step3 = new GameObject[2];

    // Pie: Array holding the recipe steps and their ingredients
    public GameObject[][] pie = new GameObject[3][];
    public GameObject[] step1_pie = new GameObject[2];
    public GameObject[] step2_pie = new GameObject[2];
    public GameObject[] step3_pie = new GameObject[2];

    //Spawn pos on recipe board
    public Transform step1_spawn1;
    public Transform step1_spawn2;
    public Transform step2_spawn1;
    public Transform step2_spawn2;
    public Transform step3_spawn1;
    public Transform step3_spawn2;

    List<GameObject> recipeBoard = new List<GameObject>(); //list of gameobjects physically on the recipe board

    [HideInInspector] // Array of 1s and 0s for each recipe index
    public int[,] recipeProgress = new int[3, 2]; 

    [HideInInspector] // Which step of the recipe we're on
    public int currentStep = 0; 

    [HideInInspector] // How many wrong ingredients have been thrown
    public int wrongIngredients = 0; // Will be displayed on the recipe board
                                     
    [HideInInspector]
    public int whichRecipe;

    private void Awake(){
        //Singleton
        if (Instance == null){
            Instance = this;
        }
        else if (Instance != this){
            Destroy(this);
        }

    }

	void Start () {


        //if (isServer && !isLocalPlayer)
        //{
            //if ((int)Random.Range(0, 1000000) % 2 == 0)
            //{
            //    whichRecipe = 0;
            //}
            //else
            //{
            //    whichRecipe = 1;
            //}
            //Debug.Log("Servers whichRecipe: " + whichRecipe);


            GameObject step1_1 = new GameObject(); //Recipe board objects
            GameObject step1_2 = new GameObject();
            GameObject step2_1 = new GameObject();
            GameObject step2_2 = new GameObject();
            GameObject step3_1 = new GameObject();
            GameObject step3_2 = new GameObject();

            gamePlay.SetActive(true);
            winScreen.SetActive(false);
            loseScreen.SetActive(false);

            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 2; j++)
                {
                    recipeProgress[k, j] = 0;
                }
            }

            // Initialize recipe pizza
            //pizza[0] = step1;
            //pizza[1] = step2;
            //pizza[2] = step3;

            // Initialize recipe pie
            pie[0] = step1_pie;
            pie[1] = step2_pie;
            pie[2] = step3_pie;

            //if (whichRecipe == 1)
            //{

                pizzaChosen = false;

                // Initialize visual recipe on the board (brute force for simplicity in spawn names)
                step1_1 = Instantiate(pie[0][0], step1_spawn1.position, step1_spawn1.rotation);
                step1_2 = Instantiate(pie[0][1], step1_spawn2.position, step1_spawn2.rotation);
                step2_1 = Instantiate(pie[1][0], step2_spawn1.position, step1_spawn1.rotation);
                step2_2 = Instantiate(pie[1][1], step2_spawn2.position, step1_spawn2.rotation);
                step3_1 = Instantiate(pie[2][0], step3_spawn1.position, step1_spawn1.rotation);
                step3_2 = Instantiate(pie[2][1], step3_spawn2.position, step1_spawn2.rotation);

            //}
            //else
            //{

            //    pizzaChosen = true;

            //    // Initialize visual recipe on the board (brute force for simplicity in spawn names)
            //    step1_1 = Instantiate(pizza[0][0], step1_spawn1.position, step1_spawn1.rotation);
            //    step1_2 = Instantiate(pizza[0][1], step1_spawn2.position, step1_spawn2.rotation);
            //    step2_1 = Instantiate(pizza[1][0], step2_spawn1.position, step1_spawn1.rotation);
            //    step2_2 = Instantiate(pizza[1][1], step2_spawn2.position, step1_spawn2.rotation);
            //    step3_1 = Instantiate(pizza[2][0], step3_spawn1.position, step1_spawn1.rotation);
            //    step3_2 = Instantiate(pizza[2][1], step3_spawn2.position, step1_spawn2.rotation);
            //}

            recipeBoard.Add(step1_1);
            recipeBoard.Add(step1_2);
            recipeBoard.Add(step2_1);
            recipeBoard.Add(step2_2);
            recipeBoard.Add(step3_1);
            recipeBoard.Add(step3_2);

            NetworkServer.Spawn(step1_1);
            NetworkServer.Spawn(step1_2);
            NetworkServer.Spawn(step2_1);
            NetworkServer.Spawn(step2_2);
            NetworkServer.Spawn(step3_1);
            NetworkServer.Spawn(step3_2);

        //}
        //CmdSpawnStrikes();
	}

    //public void CmdSpawnStrikes(){
    //    NetworkServer.Spawn(Instantiate(strikes));
    //}
	
	// Necessary??
	void Update () {
        if(!isLocalPlayer){
            return;
        }
        //strikes.text = "" + wrongIngredients;
	}

    public void CheckIngredient(GameObject ingredient){
        bool found = false;
        Debug.Log("Current Step: " + currentStep);

        if (pizzaChosen == true){
            for (int i = 0; i < pizza[currentStep].Length; i++){
                if (ingredient.name == pizza[currentStep][i].name + "(Clone)")
                {
                    CrossOffIngredient(currentStep, i);
                    found = true;
                    break;
                }
            }
        }else{
            for (int i = 0; i < pie[currentStep].Length; i++){
                if (ingredient.name == pie[currentStep][i].name + "(Clone)")
                {
                    CrossOffIngredient(currentStep, i);
                    found = true;
                    break;
                }
            }
        }
        if(found == false){

            //CmdAddStrike();
            //wrongIngredients++;
            wrongIngredients++;
            strikes.text = "" + wrongIngredients;

            if (wrongIngredients > maxWrongIngredients)
            {
                gamePlay.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(true);
                destroyForEnd();
            }
            Debug.Log("Wrong ingredient! Total: " + wrongIngredients);


        }
    }

    [Command]
    public void CmdAddStrike(){
        wrongIngredients++;
        strikes.text = "" + wrongIngredients;
    }



    //[ClientRpc]
    //public void RpcUpdate(int wrongIngredients){
    //    strikes.text = "" + wrongIngredients;
    //}


    void CrossOffIngredient(int step, int ingredient){

        recipeProgress[step,ingredient] = 1;

        int total = 0;
        for (int i = 0; i < 2; i++){
            if(recipeProgress[step,i] == 1){
                
                int listPos = step * 2 + i; //0,0 is 0th, 0,1 is 1st, 1,0 2nd, 1,1 3rd, 2,0 4th
                GameObject newCheck = Instantiate(checkMark, recipeBoard[listPos].transform.position + (new Vector3(0,0,-1)), recipeBoard[listPos].transform.rotation);
                NetworkServer.Spawn(newCheck);
                total++;
            }
        }
        Debug.Log("Ingredient crossed off for step " + currentStep);
        Debug.Log("Total correct in the step so far: " + total);
        if(total == 2){
            currentStep++;
            Debug.Log("Moved to step " + currentStep);
            if(currentStep > 2){
                currentStep = 2;

                Instantiate(confetti, confetti.transform.position, confetti.transform.rotation);
                destroyForEnd();
                gamePlay.SetActive(false);
                winScreen.SetActive(true);

                // TODO: make the pie appear instead of pizza
            }
        }
    }

    public void destroyForEnd(){
        
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Ingredient");
        foreach (var a in toDestroy)
        {
            Destroy(a);
        }
        GameObject[] checks = GameObject.FindGameObjectsWithTag("Check");
        foreach (var a in checks)
        {
            Destroy(a);
        }
    }
}
