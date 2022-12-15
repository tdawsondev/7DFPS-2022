using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootingRange = 15f;
    public int shotsInBurst = 3;
    public float timeBetweenShots = 0.1f;
    public float timeBetweenBursts = 1.4f;
    public float Inacuracy = 1f;
    

    public bool readyToShoot = true;

    public bool shootingBurst = false;
    public bool doneShooting = true;

    //used to prevent firing multipe sets if still in range.
    public bool waitingAfterFinished = true;

    public int burstsFired = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartShooting(int numberOfBursts)
    {
        if (!shootingBurst)
        {
            waitingAfterFinished = false;
            doneShooting = false;
            readyToShoot = false;
            StartCoroutine(Shoot(numberOfBursts));
        }
    }

    IEnumerator Shoot(int numberOfBursts)
    {
        while(burstsFired < numberOfBursts)
        {
            yield return StartCoroutine(ShootBurst(numberOfBursts));
            burstsFired++;
            if(burstsFired < numberOfBursts)
                yield return new WaitForSeconds(timeBetweenBursts);
            
            shootingBurst = false;
        }


        doneShooting = true;
        yield return new WaitForSeconds(.2f);
        AfterShootingDone();
        
    }

    IEnumerator ShootBurst(int numberOfBursts)
    {
        shootingBurst = true;
        int shotsFired = 0;
        //build up animation would go here
        while (shotsFired < shotsInBurst)
        {
            FireSingleShot();
            shotsFired++;
            yield return new WaitForSeconds(timeBetweenShots);
        }
        yield return null;

    }

    void FireSingleShot()
    {
        Transform target = Player.Instance.transform;
        Vector3 direction = target.position + Random.insideUnitSphere * Inacuracy - shootPoint.position;
        Quaternion lookDirection = Quaternion.LookRotation(direction.normalized);
        Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, lookDirection).GetComponent<Projectile>();
        projectile.Launch(direction.normalized, "Player");
    }

    public void AfterShootingDone()
    {
        burstsFired = 0;
        //Debug.Log("Shots Reset");
        readyToShoot = true;
        shootingBurst= false;
        StartCoroutine(waitTest());
        
    }
    IEnumerator waitTest()
    {
        yield return new WaitForSeconds(2f);
        waitingAfterFinished = true;
    }
}
