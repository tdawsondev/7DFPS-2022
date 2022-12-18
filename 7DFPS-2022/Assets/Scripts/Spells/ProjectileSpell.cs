using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ProjectileSpell : Spell
{

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
            if (tagToHit == "Enemy")
            {
                other.gameObject.GetComponent<EnemyReference>().baseEnemy.health.Damage(SpellToCast.Damage);
            }
            else
            {
                other.GetComponent<Health>().Damage(SpellToCast.Damage);
            }
        }

        if (other.tag != "Spell")
            Destroy(this.gameObject);

    }

    public void Launch(Vector3 direction, string tagToHit)
    {
        this.tagToHit = tagToHit;
        launched = true;
        projectileDirection = direction;
    }

    public override void CastSpell(Transform castPoint)
    {
        if (Player.Instance.mana.currentMana >= SpellToCast.ManaCost)
        {
            ProjectileSpell ps = Instantiate(this, castPoint.position, castPoint.rotation).GetComponent<ProjectileSpell>();
            ps.Launch(Camera.main.transform.forward, "Enemy");

            //Lower mana
            Player.Instance.mana.LoseMana(SpellToCast.ManaCost);
        }
    }

}
