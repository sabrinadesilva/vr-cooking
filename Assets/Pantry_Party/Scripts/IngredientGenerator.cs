using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {
    
    static int pantryIngredients = 3;
    public GameObject[] allIngredients = new GameObject[3];
    public Transform ing1_spawn;
    public Transform ing2_spawn;
    public Transform ing3_spawn;

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
	
	void Update () {
		
	}
}
