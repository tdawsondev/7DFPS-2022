using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightSensor : MonoBehaviour
{
    public float VisionRange = 100f;
    public LayerMask ignore;
    public Transform eyes;
    public bool CanSeePlayer()
    {

        Vector3 playerTorso = Player.Instance.transform.position;
        Vector3 playerHead = new Vector3(playerTorso.x, playerTorso.y +.45f, playerTorso.z);
        Vector3 playerFeet = new Vector3(playerTorso.x, playerTorso.y - .45f, playerTorso.z);
        if(ShootRay(playerTorso) || ShootRay(playerHead) || ShootRay(playerFeet))
        {
            return true;
        }

        return false;
    }

    public bool ShootRay(Vector3 playerPosition)
    {
        RaycastHit hit;
        Vector3 direction = playerPosition - eyes.position;
        Ray ray = new Ray(eyes.position, direction.normalized);
        if (Physics.Raycast(ray, out hit, 1000f, ~ignore))
        {
            if (hit.transform.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
}
