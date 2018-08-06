using UnityEngine;
using System.Collections;

namespace A07Examples
{
    public class Billboard : MonoBehaviour
    {
        public Transform ShootLocation;
        void Update()
        {
            transform.LookAt(ShootLocation);
        }
    }
}