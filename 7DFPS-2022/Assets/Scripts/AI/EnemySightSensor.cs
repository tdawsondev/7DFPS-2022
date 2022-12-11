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
        RaycastHit hit;
        Vector3 direction = Player.Instance.transform.position - eyes.position;
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
