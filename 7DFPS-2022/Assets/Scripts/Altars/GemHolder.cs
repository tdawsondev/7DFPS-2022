using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This whole script is gross. But I'm tired and this is just for prototyping anyway so here we are.
/// </summary>
public class GemHolder : MonoBehaviour
{
    public Portal portal;
    public string color;
    public List<GemHolder> otherHolders;
    public GameObject objectEffect;
    public bool hasObject;

    // Start is called before the first frame update
    void Start()
    {
      objectEffect.SetActive(false);   
    }


    private void OnTriggerEnter(Collider other)
    {
        if(!hasObject && other.tag == "Player")
        {
            // yikes
            if(Player.Instance.hasBlue && color == "Blue")
            {
                AddObject();
            }
            if (Player.Instance.hasRed && color == "Red")
            {
                AddObject();
            }
            if (Player.Instance.hasGreen && color == "Green")
            {
                AddObject();
            }
            
        }
    }

    public void AddObject()
    {
        objectEffect.SetActive(true);
        hasObject = true;

        bool openPortal = true;
        foreach(GemHolder holder in otherHolders)
        {
            if(!holder.hasObject)
                openPortal = false;
        }
        if (openPortal)
        {
            portal.Activate();
        }
    }
}
