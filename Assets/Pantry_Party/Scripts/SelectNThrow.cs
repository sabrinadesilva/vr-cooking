using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to each of the ingredient prefabs
// Used in tandem with StrobeSelect script
// Allows user to click once to select, then click again to throw

public class SelectNThrow : MonoBehaviour {

    public bool grabbed = false;  // have i been picked up, or not?
    Rigidbody myRb;
    StrobeSelect strobe;
    private Transform oldPlace;

    // Use this for initialization
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        strobe = GetComponent<StrobeSelect>();
    }

    // Update is called once per frame
    void Update()
    {

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
            myRb.AddRelativeForce(30,20, 500);
            grabbed = false;
            strobe.trigger = false;

            // Should be changed to detect the transform it was taken from
            // TODO: debug why this does not work
            Debug.Log("Should regenerate now");
            IngredientGenerator.Instance.Regenerate(IngredientGenerator.Instance.ing1_spawn);
        }
        else
        {   // pick it up:
            // make it move with gaze, keeping same distance from camera
            oldPlace = transform;
            Debug.Log("oldPlace = " + oldPlace);
            transform.parent = Camera.main.transform;  // attach object to camera
            grabbed = true;
            strobe.trigger = true;   // turn on color strobe so we know we have it
            myRb.isKinematic = true; //  .useGravity = false;

        }
    }
}
