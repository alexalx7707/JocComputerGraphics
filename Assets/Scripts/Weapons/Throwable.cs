using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Weapon
{
    [SerializeField]
    private float delay = 3f; //the delay before the throwable explodes
    [SerializeField]
    private float damageRadius = 20f; //the radius of the damage
    [SerializeField]
    private float explosionForce = 26f; //the force of the explosion
    //the explosion effect
    [SerializeField]
    private GameObject explosionEffect;
    private PlayerHealth playerHealth; //the player's health
    private float countdown; //the countdown timer
    private bool hasExploded = false; //has the throwable exploded
    public bool hasBeenThrown = false; //has the throwable been thrown
    private Rigidbody rb; //the rigidbody of the throwable
    public enum ThrowableType
    {
        Grenade
    }

    public ThrowableType throwableType; //the type of throwable

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay; //set the countdown to the delay
        rb = GetComponent<Rigidbody>(); //get the rigidbody component
        magazineSize = 0;
        bulletsLeft = 1;
        reloadTime = 1f;
        UpdateBulletsUI();
        //disable the rigidbody so the throwable doesn't fall to the ground
        rb.isKinematic = true;
        rb.useGravity = false;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBulletsUI();
        if (hasBeenThrown) //if the throwable has been thrown
        {
            countdown -= Time.deltaTime; //decrement the countdown
            if (countdown <= 0f && !hasExploded) //if the countdown has reached 0 and the throwable has not exploded
            {
                Explode(); //explode the throwable
                hasExploded = true; //set hasExploded to true
            }
        }
    }

    private void Explode()
    {
        if(hasExploded) return;
        hasExploded = true; //set hasExploded to true
        //instantiate the explosion effect at the throwable's position
        Instantiate(explosionEffect, transform.position, transform.rotation);
        
        //get all the colliders in the damage radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);

        foreach (Collider nearbyObject in colliders)
        {
            //get the enemy component of the nearby object
            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                //calculate the distance from the throwable to the enemy
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                float distanceToPlayer = Vector3.Distance(transform.position, playerHealth.transform.position);
                //calculate the damage based on the distance
                float damage = 100 - (distance * 10);
                float damageToPlayer = 100 - (distanceToPlayer * 10);
                //deal damage to the enemy
                enemy.TakeDamage(damage);
                playerHealth.TakeDamage(damageToPlayer);
            }
        }

        //Destroy(gameObject); //destroy the throwable
        //setactive false instead of destroying the object
        gameObject.SetActive(false);
    }

    public override void Shoot()
    {
        if(bulletsLeft > 0)
        {
            bulletsLeft--;
            hasBeenThrown = true;

            transform.SetParent(null); //unparent the throwable so that it doesn't follow the player

            rb.isKinematic = false;
            rb.useGravity = true;

            // Add force in the forward direction of the player
            Vector3 throwDirection = cam.transform.forward + cam.transform.up * 0.5f;
            rb.AddForce(throwDirection * explosionForce, ForceMode.Impulse);
        }
        else
        {
            return;
        }
    }

    public override void Reload()
    {
        if (this == null) return; // Check if the object still exists
        if (bulletsLeft == 1 || isReloading || magazineSize == 0) return;
        isReloading = true;
        //make the text black while reloading
        bulletsLeftText.color = Color.black;
        Invoke("ReloadCompleted", reloadTime);
    }

    public override void ReloadCompleted()
    {
        if (this == null) return; // Check if the object still exists
        bulletsLeft = 1;
        magazineSize -= 1;
        bulletsLeftText.color = Color.white;
        isReloading = false;
    }

    public override void UpdateBulletsUI()
    {
        bulletsLeftText.text = bulletsLeft + " / " + magazineSize;
    }
}
