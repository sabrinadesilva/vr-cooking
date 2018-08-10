using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

// Attached to each of the ingredient prefabs
// Used in tandem with StrobeSelect script
// Allows user to click once to select, then click again to throw

public class SelectNThrow : NetworkBehaviour
{
    [HideInInspector]
    public bool grabbed = false;
    [HideInInspector]
    public Rigidbody myRb;
    StrobeSelect strobe;
    private Transform oldPlace;
    private Transform ShootLocation;
    [HideInInspector]
    public GameObject throwIng;
    public Transform spawnPos;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        strobe = GetComponent<StrobeSelect>();
        ShootLocation = GameObject.Find("ShootLoc").transform;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }

    /*
     * PickupOrDrop
     * Handle the event when the user clicks the button while 
     * gaze is on this object.  Toggle grabbed state.
     */

    public void PickupOrDrop()
    {
        //if(!isServer){
        //    return;
        //}
        //if(isClient){
        //    Debug.Log("HOST client pickup");
        //}

        if (grabbed)
        {
            transform.parent = null;  // release the object
            transform.LookAt(ShootLocation);
            myRb.isKinematic = false;
            myRb.useGravity = true;
            myRb.AddRelativeForce(30, 20, 500);
            //CmdThrow(throwIng);
            //Destroy(myRb.gameObject);
            grabbed = false;

        }
        else
        {
        //Vector3 regeneration = myRb.gameObject.transform.position;
        //myRb.gameObject.transform.position += new Vector3(0, 3, 0);
        ////GameObject newobj = myRb.gameObject;
        //CmdThrow(myRb.gameObject);

        ////myRb.gameObject.transform.position = regeneration;

            foreach (var go in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (go.GetComponent<IngredientGenerator>().isLocalPlayer)
                {
                    go.GetComponent<IngredientGenerator>().Regenerate(transform.position, transform.rotation);
                }
            }

        //myRb.gameObject.transform.position += new Vector3(0, 3, 0);
        //throwIng = Instantiate(myRb.gameObject, myRb.gameObject.transform);



            transform.parent = Camera.main.transform;  // attach object to camera
            grabbed = true;
            //strobe.trigger = true;   // turn on color strobe so we know we have it
            myRb.isKinematic = true; //  .useGravity = false;

            

        }
    }

    [Command]
    public void CmdThrow(GameObject toThrow)
    {
        var newObj = Instantiate(toThrow);

        newObj.GetComponent<Rigidbody>().isKinematic = false;
        newObj.GetComponent<Rigidbody>().useGravity = true;
        newObj.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 500);
        NetworkServer.Spawn(newObj);

    }
}
