using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer; //used to smoothly transition the health bar
    public float maxHealth = 100f;
    public float chipSpeed = 2f; //how fast the health bar will update
    public Image frontHealthBar; //the front image of the health bar
    public Image backHealthBar; //the back image of the health bar
    public TextMeshProUGUI healthText; //the text displaying the health value
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); //make sure health is between 0 and maxHealth
        UpdateHealthUI(); //update the health bar
        
    }

    // UpdateHealthUI will update the health bar to reflect the current health value
    public void UpdateHealthUI()
    {
        
        float fillF = frontHealthBar.fillAmount; //get the current fill amount of the front health bar
        float fillB = backHealthBar.fillAmount; //get the current fill amount of the back health bar
        float hFraction = health / maxHealth; //get the fraction of health remaining, between 0 and 1, so we can compare it to the fill amount
        if(fillB > hFraction) //we've taken damage
        {
            frontHealthBar.fillAmount = hFraction; //set the front health bar to the new health fraction
            backHealthBar.color = Color.red; //change the back health bar to red
            lerpTimer += Time.deltaTime; //increment the lerpTimer by the time since the last frame, so we can smoothly transition the health bar
            float percentComplete = lerpTimer/chipSpeed; //calculate how far along the lerp we are as a percentage of the total time it should take to complete the lerp
            percentComplete = percentComplete * percentComplete; //square the percentComplete to make the lerp more exponential, so the health bar will update slower at the beginning and faster at the end
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete); //lerp the back health bar fill amount to the new health fraction over time
            //Mathf.Lerp(a, b, t) will return a value between a and b, where t is the percentage of the way from a to b
        }
        else if(fillF < hFraction) //we've healed
        {
            backHealthBar.color = Color.green; //change the back health bar to green
            backHealthBar.fillAmount = hFraction; //set the back health bar to the new health fraction
            lerpTimer += Time.deltaTime; //increment the lerpTimer by the time since the last frame, so we can smoothly transition the health bar
            float percentComplete = lerpTimer/chipSpeed; //calculate how far along the lerp we are as a percentage of the total time it should take to complete the lerp
            percentComplete = percentComplete * percentComplete; //square the percentComplete to make the lerp more exponential, so the health bar will update slower at the beginning and faster at the end
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete); //lerp the front health bar fill amount to the new health fraction over time
        }
        
        healthText.text = Mathf.Round(health) + " / " + Mathf.Round(maxHealth); //update the health text to display the current health value
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f; //reset the lerpTimer, so the health bar will update immediately
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f; //reset the lerpTimer, so the health bar will update immediately
    }
}
