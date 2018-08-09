using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

// Attached to each of the ingredient prefabs
// Used in tandem with StrobeSelect script
// Allows user to click once to select, then click again to throw

public class SelectNThrow : NetworkBehaviour {
    [HideInInspector]
    public bool grabbed = false;
    [HideInInspector]
    public Rigidbody myRb;
    StrobeSelect strobe;
    private Transform oldPlace;
    private Transform ShootLocation;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        strobe = GetComponent<StrobeSelect>();
        ShootLocation = GameObject.Find("ShootLoc").transform;
    }

    void Update()
    {
        if(!isLocalPlayer){
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
        if(!isServer){
            return;
        }

        if (grabbed)
        {  // now drop it
            Debug.Log("Should throw now");

            transform.parent = null;  // release the object
            transform.LookAt(ShootLocation);
            myRb.isKinematic = false; 
            myRb.useGravity = true;
            myRb.AddRelativeForce(30, 20, 500);

            grabbed = false;

        }
        else
        {
            foreach(var go in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (go.GetComponent<IngredientGenerator>().isLocalPlayer ) {
                    go.GetComponent<IngredientGenerator>().Regenerate(transform.position, transform.rotation);
                }
            }
            Debug.Log("Should pick up now");
            transform.parent = Camera.main.transform;  // attach object to camera
            grabbed = true;
            //strobe.trigger = true;   // turn on color strobe so we know we have it
            myRb.isKinematic = true; //  .useGravity = false;
        }
    }
}
