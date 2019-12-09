using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Projectiles (deafult)")]
    public float fireRate = 1f;
    private float fireCntDown = 0f;
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int dmgOverTime = 30;
    public float slowPercentage = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem waterbeamParticles;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    public Animator enemyAnimator;

    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDist = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDist)
            {
                shortestDist = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if(closestEnemy != null && shortestDist <= range)
        {
            target = closestEnemy.GetComponent<Enemy>().hitPoint;
            targetEnemy = closestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    waterbeamParticles.Stop();
                }
                    
            }

            return;
        }

        LockOnTarget();

        if(useLaser)
        {
            Laser();
        }

        else
        {
            if (fireCntDown <= 0f)
            {
                Shoot();
                fireCntDown = 1f / fireRate;
            }

            fireCntDown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(dmgOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercentage);

        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            waterbeamParticles.Play();
        }
            

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 impactDir = firePoint.position - target.position;

        waterbeamParticles.transform.rotation = Quaternion.LookRotation(impactDir);

        waterbeamParticles.transform.position = target.position + impactDir.normalized;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
