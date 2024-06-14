using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{

    // Start is called before the first frame update
    void Start()
    {
        magazineSize = 0;
        bulletsLeft = magazineSize;
        reloadTime = 0;
        UpdateBulletsUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Shoot()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo; //this will store information about the object that was hit by the ray
        if (Physics.Raycast(ray, out hitInfo, 3))
        {
            if (hitInfo.collider.GetComponent<Enemy>() != null)
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(50);
            }
        }
        //simulate a knife stab, by moving the knife forward for a short distance and then back in 0.1 seconds
        //the knife moves relative to its current position by -0.06 on the y-axis and 0.06 on the z-axis and -0.02 on the x-axis
        transform.Translate(Vector3.forward * 0.14f);
        transform.Translate(Vector3.up * 0.14f);
        transform.Translate(Vector3.right * -0.02f);
        Invoke("MoveBack", 0.1f);
    }

    void MoveBack()
    {
        transform.Translate(Vector3.forward * -0.14f);
        transform.Translate(Vector3.up * -0.14f);
        transform.Translate(Vector3.right * 0.02f);
    }

    public override void Reload()
    {
        // Knife doesn't reload
        return;
    }

    public override void ReloadCompleted()
    {
        // Knife doesn't reload
        return;
    }

    public override void UpdateBulletsUI()
    {
        // Knife doesn't have bullets
        bulletsLeftText.text = " ";
    }
}
