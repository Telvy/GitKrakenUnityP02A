using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BaseEnemyAI : MonoBehaviour
{


    [SerializeField] AudioClip _alertSound = null;
    [SerializeField] AudioClip _deathSound = null;
    [SerializeField] AudioClip _fireSound = null;
    [SerializeField] AudioClip _takeDamageSound = null;
    [SerializeField] ParticleSystem _enemyDeathExplosion = null;
    public GameObject currentTarget; //graphics to manipulate
    public GameObject IdleView; //default view position during Idle state
    public Transform turretHead; //object we are going to rotate
    public Transform muzzle; //spawn projectile
    public GameObject projectilePrefab; //projectile that spawns from the muzzle
    public LayerMask enemyLayerMask;
    public LevelController scoreIncrease;

    //LevelController scoreIncrease = gameObject.GetComponent<LevelController>();


    //enemy AI attack value(s)
    public float attackDist;
    public float attackDamage;
    public float shotCoolDown;
    private float timer;
    public float lookSpeed = 2f;

    //enemy health value(s)
    int enemyHealth = 100;

    public bool showRange = false;

    //state machine values
    public enum ENEMYAI_STATE
    {
        Idle,
        Alert,
        Attack
    }

    public ENEMYAI_STATE enemyAI_State;

    private Quaternion previousRotation;
    public float turnSpeed = 8f;
    public float turnCoolDown = 4f;
    private float timerTurnCoolDown = 0f;
    public Collider player;
    public Transform playerTransform;
    void Start()
    {
        //InvokeRepeating(nameof(CheckForTarget), 0, 0.5f);
        //InvokeRepeating("OnTriggerEnter", 0, 0.5f);
        previousRotation = transform.rotation;
        
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(player);
        
        StateMachine();

        //if(currentTarget != null)
        //{
        //    EnemyAIfollowTarget();
        //}

        //timer += Time.deltaTime;
        //if(timer>= shotCoolDown)
        //{
        //    if(currentTarget != null)
        //    {
        //        timer = 0;
        //        EnemyAIshoot();
        //    }
        //}
    }

    private void StateMachine()
    {
        switch (enemyAI_State)
        {
            default: //Idle
                transform.rotation = Quaternion.RotateTowards(transform.rotation, previousRotation, turnSpeed * Time.deltaTime);
                if (timerTurnCoolDown <= 0)
                {
                    player = FoundTarget();
                    if(player != null)
                    {
                        rotateTowardsEnemy();
                        enemyAI_State = ENEMYAI_STATE.Alert;
                    }
                }
                break;
            case ENEMYAI_STATE.Alert:
                player = FoundTarget();
                if (player != null)
                {
                    rotateTowardsEnemy();
                    if (facingEnemy())
                    {
                        //EnemyAIshoot();
                        timer += Time.deltaTime;
                        if (timer >= shotCoolDown)
                        {
                            if (currentTarget != null)
                            {
                                timer = 0;
                                EnemyAIshoot();
                            }
                        }
                    }
                }
                else
                {
                    enemyAI_State = ENEMYAI_STATE.Idle;
                }
                break;
            case ENEMYAI_STATE.Attack:
                
                rotateTowardsEnemy();
                if (facingEnemy())
                {
                    //EnemyAIshoot();
                    timer += Time.deltaTime;
                    if (timer >= shotCoolDown)
                    {
                        if (currentTarget != null)
                        {
                            timer = 0;
                            EnemyAIshoot();
                        }
                    }
                }
                break;
        }
    }

    bool facingEnemy()
    {
        float angle = Vector3.SignedAngle(transform.forward, PlayerDirection(), Vector3.up);
        return (angle <= 1 && angle >= 0) || (angle >= -1 && angle <= 0);
    }

    Vector3 PlayerDirection()
    {
        return (playerTransform.position - transform.position).normalized;
    }

    public void rotateTowardsEnemy()
    {
        
        Quaternion lookDirection = Quaternion.LookRotation(PlayerDirection());
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, turnSpeed * Time.deltaTime);
    }

    //private void StateMachine()
    //{
    //    switch (enemyAI_State)
    //    {
    //        case ENEMYAI_STATE.Idle:
    //            EnemyAIfollowDefaultIdleview();
    //            //Debug.Log("Do nothing for now");
    //            break;

    //        case ENEMYAI_STATE.Alert:
    //            EnemyAIfollowTarget();
    //            StartCoroutine(setToAttack());
    //            break;

    //        case ENEMYAI_STATE.Attack:
    //            if (currentTarget != null)
    //            {
    //                EnemyAIfollowTarget();
    //            }

                //timer += Time.deltaTime;
                //if (timer >= shotCoolDown)
                //{
                //    if (currentTarget != null)
                //    {
                //        timer = 0;
                //        EnemyAIshoot();
                //    }
                //}
    //            break;   
    //    }
    //}



       public IEnumerator setToAlert(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        enemyAI_State = ENEMYAI_STATE.Alert;
    }

    public void setToAttack()
    {
        enemyAI_State = ENEMYAI_STATE.Attack;
    }

    IEnumerator setToIdle()
    {
        yield return new WaitForSeconds(4f);
        enemyAI_State = ENEMYAI_STATE.Idle;
    }

    private Collider FoundTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist, enemyLayerMask);

        for (int i = 0; i < colls.Length; i++)
        {
            
            if (colls[i].CompareTag("Player"))
            {
                //OneShotSoundManager.PlayClip2D(_alertSound, 1);
                return colls[i];
            }
        }
        return null;
    }

    private void EnemyAIfollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - transform.position;
        targetDir.y = 0;
        turretHead.forward = targetDir;
    }

    private void EnemyAIfollowDefaultIdleview()
    {
        Vector3 targetDir = IdleView.transform.position - transform.position;
        targetDir.y = 0;
        turretHead.forward = targetDir;
    }

    private void EnemyAIshoot()
    {
        Debug.Log("the enemy shot the player");
        OneShotSoundManager.PlayClip2D(_fireSound, 1);
        GameObject projectileShoot = Instantiate(projectilePrefab, muzzle.transform.position, muzzle.rotation);
        Debug.Log($"{muzzle.rotation}");
    }

    public void EnemyAItakeDamage(int _damageToTake)
    {
        OneShotSoundManager.PlayClip2D(_takeDamageSound, 1);
        
        enemyHealth -= _damageToTake;
        if (enemyHealth <= 0)
        {
            
            scoreIncrease.IncreaseScore(20);
            OneShotSoundManager.PlayClip2D(_deathSound, 1);
            ParticleSystem deathParticles = Instantiate(_enemyDeathExplosion, transform.position, Quaternion.identity);
            deathParticles.Play();
            Destroy(gameObject);
        }
    }

    //used to visualize the overlapSphere
    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}
