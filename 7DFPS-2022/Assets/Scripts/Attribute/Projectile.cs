using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public float Damage;

    bool launched = false;
    string tagToHit;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (launched)
        {
            transform.position += direction * Time.deltaTime * Speed;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToHit)
        {
            other.GetComponent<Health>().Damage(Damage);
        }

        if(other.tag != "Spell")
            Destroy(this.gameObject);

    }

    public void Launch(Vector3 direction, string tagToHit)
    {
        this.tagToHit = tagToHit;
        launched = true;
        this.direction = direction;
    }
}
