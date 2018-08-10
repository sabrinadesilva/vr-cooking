using UnityEngine;
using System.Collections;


    public class Billboard : MonoBehaviour
    {
        private Transform ShootLocation;


        void Start()
        {
            ShootLocation = GameObject.Find("ShootLoc").transform;
            //ShootLocation
        }

        void Update()
        {
        transform.LookAt(ShootLocation);

        }
    }
