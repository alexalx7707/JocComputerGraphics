using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool bulletDestroyed = false;
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            //if the bullet hits the player, deal damage
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);
        }
        if (hitTransform.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            //if the bullet hits an enemy, deal damage
            //hitTransform.GetComponent<Enemy>().TakeDamage(10);
        }
        Destroy(gameObject);
        bulletDestroyed = true;
    }

    private void Update()
    {
        //if the bullet hasn't hit anything after 3 seconds, destroy it
        if (!bulletDestroyed)
        {
            Destroy(gameObject, 3f);
        }
    }
}
