using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Attached to each of the ingredient prefabs
// Used in tandem with StrobeSelect script
// Allows user to click once to select, then click again to throw

public class SelectNThrow : NetworkBehaviour {
    [HideInInspector]
    public bool grabbed = false;
    Rigidbody myRb;
    StrobeSelect strobe;
    private Transform oldPlace;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        strobe = GetComponent<StrobeSelect>();
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
        if (grabbed)
        {  // now drop it
            transform.parent = null;  // release the object
            myRb.isKinematic = false; 
            myRb.useGravity = true;
            myRb.AddRelativeForce(30, 20, 500);
            grabbed = false;
            strobe.trigger = false;
        }
        else
        {
            foreach(var go in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (go.GetComponent<IngredientGenerator>().isLocalPlayer ) {
                    go.GetComponent<IngredientGenerator>().Regenerate(transform.position, transform.rotation);
                }
            }

            transform.parent = Camera.main.transform;  // attach object to camera
            grabbed = true;
            strobe.trigger = true;   // turn on color strobe so we know we have it
            myRb.isKinematic = true; //  .useGravity = false;
        }
    }
}
