using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportPoint;
    public bool active = false;
    public GameObject effects;

    // Start is called before the first frame update
    void Start()
    {
        effects.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(active && other.tag == "Player")
        {
            Player.Instance.playerMovement.Teleport(teleportPoint.position);
        }
    }

    public void Activate()
    {
        effects.SetActive(true);
        active = true;
    }
    public void Deactivate()
    {
        effects.SetActive(false);
        active = false;
    }
}
