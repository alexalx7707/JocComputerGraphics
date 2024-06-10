using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStamina : MonoBehaviour
{
    PlayerMotor playerMotor;
    public float stamina;
    public float maxStamina = 100f;
    public float chipSpeed = 2f; //how fast the stamina bar will update
    public float jumpStaminaCost = 20f; //how much stamina jumping will cost
    public float sprintStaminaCost = 10f; //how much stamina sprinting will cost
    public float staminaRegenRate = 5f; //how fast stamina will regenerate
    private bool isGrounded;
    public Image StaminaBar; //the image of the stamina bar
    public TextMeshProUGUI staminaText; //the text displaying the stamina value
    
    private bool isSprinting = false;
    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;    
        playerMotor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina = Mathf.Clamp(stamina, 0, maxStamina); //make sure health is between 0 and maxHealth
        UpdateStaminaUI();
        if (isSprinting)
        {
            stamina -= sprintStaminaCost * Time.deltaTime;
            if(stamina <= 0)
            {
                stamina = 0;
                SprintCanceled();
            }
        }
        else
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }

        if (stamina <= 0 && GetComponent<PlayerMotor>().playerSpeed != 5f)
        {
            playerMotor.SetPlayerSpeed(5f);
        }
    }

    public void JumpPerformed()
    {
        stamina -= jumpStaminaCost;
    }

    public void SprintPerformed()
    {
        isSprinting = true;
    }
    public void SprintCanceled()
    {
        isSprinting = false;

        if(playerMotor.playerSpeed == 8f)
        {
            playerMotor.SetPlayerSpeed(5f);
        }
    }

    public void UpdateStaminaUI()
    {
        float hFraction = stamina / maxStamina; //get the fraction of stamina remaining, between 0 and 1, so we can compare it to the fill amount
        StaminaBar.fillAmount = hFraction; //set the stamina bar to the new stamina fraction
        staminaText.text = Mathf.Round(stamina) + " / " + Mathf.Round(maxStamina); //update the stamina text to display the current stamina value
    }
}
