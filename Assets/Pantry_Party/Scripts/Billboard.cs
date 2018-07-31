using UnityEngine;
using System.Collections;

namespace A07Examples
{
    public class Billboard : MonoBehaviour
    {

        void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}