using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public Health health;
    public NavMeshAgent agent;
    public int XpValue;
    public Material FlashColor;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    Material originalColor;
    public GameObject explosionPrefab;
    private void Awake()
    {
        health.Damaged += TookDamage;
    }
    private void OnDestroy()
    {
        health.Damaged -= TookDamage;
    }

    public virtual void TookDamage(float amount, Transform tran)
    {
        if (health.Dead)
        {
            SpawnManager.instance.spawnCount--;
            ScoreManager.instance.GainXP(XpValue);
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 3f);
            Destroy(gameObject);  
        }
        StartCoroutine(DamageFlash());
        

    }

    /// <summary>
    /// Used to Set variables when an enemy is spawned in
    /// </summary>
    public virtual void OnSpawn()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if(mesh)
            originalColor = mesh.materials[0];
        if(skinnedMeshRenderer)
            originalColor = skinnedMeshRenderer.materials[0];
    }

    public IEnumerator DamageFlash()
    {
        if (mesh)
        {
            mesh.material = FlashColor;
        }
        if (skinnedMeshRenderer)
        {

            skinnedMeshRenderer.material = FlashColor;
        }
        yield return new WaitForSeconds(.05f);
        if (mesh)
        {
            mesh.material = originalColor;
        }
        if (skinnedMeshRenderer)
        {
            skinnedMeshRenderer.material = originalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
