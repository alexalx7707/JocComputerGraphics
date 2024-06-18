using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AlertSystem : MonoBehaviour
{
    public static AlertSystem Instance { get; private set; } // Singleton
    public bool IsPlayerSpotted { get; private set; } = false; // Is the player spotted?
    public Vector3 LastKnownPosition { get; private set; } // Last known position of the player

    private List<Enemy> enemies = new List<Enemy>(); // List of enemies

    private bool isAlerted = false; // Is the enemy alerted?

    private float timer = 0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void Alert(Vector3 playerPosition)
    {
        IsPlayerSpotted = true;
        LastKnownPosition = playerPosition;
        NotifyAllEnemies();
    }
    public void RegisterEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    private void NotifyAllEnemies()
    {
        if (!isAlerted)
        {
            foreach (var enemy in enemies)
            {
                enemy.LastKnowPos = LastKnownPosition + (Random.insideUnitSphere * 10);
                enemy.stateMachine.ChangeState(new AlertState());
            }
            isAlerted = true;
        }
    }

    public void ResetAlert()
    {
        isAlerted = false;
        IsPlayerSpotted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlerted)
        {
            timer += Time.deltaTime;
            if (timer >= 7f)
            {
                ResetAlert();
                timer = 0f;
            }
        }
    }
}
