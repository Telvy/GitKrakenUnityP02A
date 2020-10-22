using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAI : MonoBehaviour
{

    private GameObject currentTarget;
    public Transform turretHead; //object we are going to rotate
    public Transform muzzle; //spawn projectile
    public GameObject projectilePrefab;

    public float attackDist;
    public float attackDamage;
    public float shotCoolDown;
    private float timer;
    public float lookSpeed = 2f;

    int enemyHealth = 100;

    public bool showRange = false;

    
    void Start()
    {
        InvokeRepeating("CheckForTarget", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null)
        {
            FollowTarget();
        }

        timer += Time.deltaTime;
        if(timer>= shotCoolDown)
        {
            if(currentTarget != null)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);

        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if(colls[i].tag == "Player")
            {
                float distance = Vector3.Distance(transform.position, colls[i].transform.position);

                if(distance < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = distance;
                }
            }
        }
    }

    private void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - transform.position;
        targetDir.y = 0;
        turretHead.forward = targetDir;
    }

    private void Shoot()
    {
        Debug.Log("the enemy shot the player");
        GameObject projectileShoot = Instantiate(projectilePrefab, muzzle.transform.position, muzzle.rotation);
        Debug.Log($"{muzzle.rotation}");
    }

    public void TakeDamage(int _damageToTake)
    {
        enemyHealth -= _damageToTake;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}
