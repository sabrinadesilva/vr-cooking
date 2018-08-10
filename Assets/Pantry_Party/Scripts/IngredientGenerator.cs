using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class IngredientGenerator : NetworkBehaviour{//MonoBehaviour {
    
    static int pantryIngredients = 3;
    //public int numIngredients = 8;
    public GameObject[] allIngredients = new GameObject[11];
    public Transform ing1_spawn;
    public Transform ing2_spawn;
    public Transform ing3_spawn;

    public override void OnStartServer()
    {
        //Output that the Server has started
        Debug.Log("Server Started!");
    }

    public override void  OnStartLocalPlayer () {
        base.OnStartLocalPlayer();
        CmdNetworkSpawn();
	}

    [Command]
    public void CmdNetworkSpawn(){
        
        GameObject[] pantry = new GameObject[pantryIngredients];
        for (int i = 0; i < pantry.Length; i++)
        {
            pantry[i] = allIngredients[(int)Random.Range(0, allIngredients.Length)];
        }

        var ing1 = (GameObject)Instantiate(pantry[0], ing1_spawn.position, ing1_spawn.rotation);
        var ing2 = (GameObject)Instantiate(pantry[1], ing2_spawn.position, ing2_spawn.rotation);
        var ing3 = (GameObject)Instantiate(pantry[2], ing3_spawn.position, ing3_spawn.rotation);

        //if (NetworkServer.active)
        //{
            NetworkServer.Spawn(ing1);
            NetworkServer.Spawn(ing2);
            NetworkServer.Spawn(ing3);
        //}
       
    }

    public void Regenerate(Vector3 newPos, Quaternion newRot){
        var newIng = (GameObject)Instantiate(allIngredients[(int)Random.Range(0, allIngredients.Length)], newPos, newRot);
        CmdRegenerate(newIng);
    }

    [Command]
    public void CmdRegenerate(GameObject newIng){
        //if (NetworkServer.active)
        //{
            NetworkServer.Spawn(newIng);
        //}
    }

    public void AddChecks(GameObject check){

        CmdAddChecks(check);
    }

    [Command]
    public void CmdAddChecks(GameObject check){
        //if (NetworkServer.active)
        //{
            NetworkServer.Spawn(check);
        //}
    }
	
	void Update () {
        if(!isLocalPlayer){
            return;
        }
	}
}
