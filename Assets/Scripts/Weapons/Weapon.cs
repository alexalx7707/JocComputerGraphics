using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Transform playerGunBarrel;
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading = false;
    public TextMeshProUGUI bulletsLeftText;
    public Camera cam;

    public abstract void Shoot();
    public abstract void Reload();
    public abstract void ReloadCompleted();
    public abstract void UpdateBulletsUI();

    
}
