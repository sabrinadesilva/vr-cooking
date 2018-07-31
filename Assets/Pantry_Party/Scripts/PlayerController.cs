using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.parent.transform.position = transform.position + Vector3.up;

    }
    void Update()
    {
        if (!isLocalPlayer){
            return;
        }
        Camera.main.transform.parent.transform.position = transform.position + Vector3.up;
    }
}

