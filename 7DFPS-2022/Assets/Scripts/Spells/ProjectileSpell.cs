using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ProjectileSpell : MonoBehaviour
{

    public SpellScriptableObject SpellToCast;

    private Vector3 projectileDirection;

    private bool launched;
    private string tagToHit;

    private void Awake()
    {
        Destroy(this.gameObject, SpellToCast.LifeTime);
    }

    public void Update()
    {
        if (launched) { 
            if (SpellToCast.Speed > 0)
            {
                //transform.Translate(projectileDirection * SpellToCast.Speed * Time.deltaTime);
                transform.position += projectileDirection * Time.deltaTime * SpellToCast.Speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToHit)
        {
            other.GetComponent<Health>().Damage(SpellToCast.Damage);
        }

        Destroy(this.gameObject);

    }

    public void Launch(Vector3 direction, string tagToHit)
    {
        this.tagToHit = tagToHit;
        launched = true;
        projectileDirection = direction;
    }

}
