using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SafeManager : MonoBehaviour
{
    public static SafeManager Instance; //the instance of the SafeManager
    public int safeCount; //the number of safes that have been opened
    public GameObject winText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(safeCount == 3)
        {
            PlayerWin();
            Invoke("EndGame", 7f);
        }
    }

    private IEnumerator WinText()
    {
        yield return new WaitForSeconds(3f);
        winText.gameObject.SetActive(true);
    }

    public void PlayerWin()
    {
        GameObject.FindObjectOfType<ScreenFader>().StartFade();
        StartCoroutine(WinText());
    }

    public void EndGame()
    {
        Time.timeScale = 0; //stop time
    }
}
