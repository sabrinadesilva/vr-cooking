using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class IngredientGenerator : NetworkBehaviour{//MonoBehaviour {
    
    static int pantryIngredients = 3;
    public GameObject[] allIngredients = new GameObject[3];
    public Transform ing1_spawn;
    public Transform ing2_spawn;
    public Transform ing3_spawn;

    public static IngredientGenerator Instance;


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

	void Start () {

        // Select the random ingredients
        GameObject[] pantry = new GameObject[pantryIngredients];
        for (int i = 0; i < pantry.Length; i++){
            pantry[i] = allIngredients[(int)Random.Range(0, allIngredients.Length)];
        }

        // Spawn them at the spawn points
        var ing1 = (GameObject)Instantiate(pantry[0], ing1_spawn.position, ing1_spawn.rotation);
        var ing2 = (GameObject)Instantiate(pantry[1], ing2_spawn.position, ing2_spawn.rotation);
        var ing3 = (GameObject)Instantiate(pantry[2], ing3_spawn.position, ing3_spawn.rotation);

	}

    public void Regenerate(Vector3 newPos, Quaternion newRot){
        var newIng = (GameObject)Instantiate(allIngredients[(int)Random.Range(0, allIngredients.Length)], newPos, newRot);
        Debug.Log("New ingredient should have been generated");
    }
	
	void Update () {
		
	}
}
