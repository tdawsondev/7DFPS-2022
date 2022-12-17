using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMelee : MonoBehaviour
{
    public float damage;
    public float damageRadius;
    public Transform damageBox;
    public bool readyForMelee = true;
    public float waitTimeAfterSwing;
    public int currentNumberofSwings;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        readyForMelee = true;
        currentNumberofSwings = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Swing()
    {
        readyForMelee = false;
        animator.SetTrigger("Swing");
    }

    public void CheckDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(damageBox.position, damageRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Player")
            {
                Player.Instance.health.Damage(damage, transform);
            }
        }
        currentNumberofSwings++;
        
    }
    public void StartMeleeRecharge()
    {
        StartCoroutine(RechargeMelee());
    }
    IEnumerator RechargeMelee()
    {
        readyForMelee = false;
        yield return new WaitForSeconds(waitTimeAfterSwing);
        readyForMelee=true;

    }
    public void ResetCurrentSwings()
    {
        currentNumberofSwings=0;
    }
}
