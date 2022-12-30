using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpell : MonoBehaviour
{

    public Spell spell;
    public GameObject spellEffects;
    public bool hasSpell = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && hasSpell)
        {
            PickUp();
        }
    }
    public void PickUp()
    {
        HUDManager.instance.DisplayInformation("Enhanced Fireball Acquired!");
        AudioManager.instance.Play("Stinger");
        Player.Instance.magicSystem.rightHand.EquipSpell(spell);
        hasSpell = false;
        spellEffects.SetActive(false);

    }
}
