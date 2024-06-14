using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{ 
    // Start is called before the first frame update
    void Start()
    {
        magazineSize = 7;
        bulletsLeft = magazineSize;
        reloadTime = 2f;
        UpdateBulletsUI();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateBulletsUI();
    }

    public override void Shoot()
    {
        if (bulletsLeft > 0)
        {
            bulletsLeft--;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hitInfo; //this will store information about the object that was hit by the ray
            if (Physics.Raycast(ray, out hitInfo, 50))
            {
                if (hitInfo.collider.GetComponent<Enemy>() != null)
                {
                    hitInfo.collider.GetComponent<Enemy>().TakeDamage(10);
                }
            }
            //instantiate a bullet at the barrel's position
            GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, playerGunBarrel.position, playerGunBarrel.transform.rotation * Quaternion.Euler(90, 0, 0));
            bullet.GetComponent<Rigidbody>().velocity = playerGunBarrel.forward * 400; //400 is the speed of the bullet
        }
        else
        {
            
            return;
        }
    }

    public override void Reload()
    {
        if (bulletsLeft == magazineSize || isReloading) return; //if the magazine is full or we are already reloading, return (do nothing)
        isReloading = true;
        //make the text black while reloading
        bulletsLeftText.color = Color.black;
        Invoke("ReloadCompleted", reloadTime); //invoke the reload completed function after 4 seconds
    }

    public override void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        bulletsLeftText.color = Color.white;
        isReloading = false;
    }
    public override void UpdateBulletsUI()
    {
        bulletsLeftText.text = bulletsLeft + " / " + magazineSize;
    }
}
