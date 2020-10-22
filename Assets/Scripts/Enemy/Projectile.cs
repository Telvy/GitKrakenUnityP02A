using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private TelvyTimer bulletLifetime = new TelvyTimer();
    public float projectileSpeed = 5f;
    [System.Serializable]
    public class TelvyTimer
    {
        public float set = 1f;
        [System.NonSerialized] public float countdown = 0f;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        bulletLifetime.countdown = bulletLifetime.set;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * projectileSpeed * Time.deltaTime);
        bulletLifetime.countdown -= Time.deltaTime;
        if(bulletLifetime.countdown <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player collided with enemy");
        }

        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            //Debug.Log("enemy damage player");
            playerHealth.hurtPlayer(30);
            Destroy(gameObject);
        }
    }
}
