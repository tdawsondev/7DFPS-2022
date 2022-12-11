using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ProjectileSpell : MonoBehaviour
{

    public SpellScriptableObject SpellToCast;

    private SphereCollider myCollider;
    private Rigidbody myRigidbody;

    private Vector3 projectileDirection;

    private bool launched;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = SpellToCast.SpellRadius;

        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.isKinematic = true;

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
        //apply spell effects
        if (other.tag != "Player")
            Destroy(this.gameObject);
    }

    public void Launch(Vector3 direction)
    {
        launched = true;
        projectileDirection = direction;
        Debug.Log(direction);
    }

}
