using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float health = 100f; //the health of the enemy
    public StateMachine stateMachine; //ERA PRIVATE INAINTE ***
    private NavMeshAgent agent;
    private GameObject player; //the player object
    private Vector3 lastKnowPos; //the last known position of the player
    //private bool isAlerted = false; //is the enemy alerted
    public NavMeshAgent Agent { get => agent; } //property to access the navmesh agent from other scripts (like the states)
    public GameObject Player { get => player; } //property to access the player object from other scripts (like the states)
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; } //property to access the last known position of the player from other scripts (like the states)
    //the setter also allows the last known position to be changed from other scripts (like the states)
    public Path path; //the path the enemy will follow

    [Header("Sight Values")]
    public float sightDistance = 30f; //how far the enemy can see
    public float fieldOfView = 75f; //the field of view of the enemy
    public float eyeHeight; //the height of the enemy's eyes

    [Header("Weapon Values")]
    public Transform gunBarrel; //the position the bullets will spawn from
    [Range(0.1f, 10f)]
    public float fireRate; //how fast the enemy can shoot

    [SerializeField] //this will make the field visible in the inspector for debugging purposes.
    private string currentState; //the name of the current state
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

        AlertSystem.Instance.RegisterEnemy(this); //***
    }

    // Update is called once per frame
    void Update()
    {
        currentState = stateMachine.activeState.ToString();
        CanSeePlayer();
       
    }

    void OnDestroy()
    {
        AlertSystem.Instance.UnregisterEnemy(this);
    } //***

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            //is the player close enough to be seen
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight); //get the direction to the player
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward); //get the angle between the direction to the player and the forward direction of the enemy
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView) //if the player is within the field of view of the enemy
                {
                    //is the player in line of sight (i.e. not behind a wall)
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection); //create a ray from the enemy to the player
                    RaycastHit hitInfo = new RaycastHit(); //store information about what the ray hits
                    if (Physics.Raycast(ray, out hitInfo, sightDistance)) //if the ray hits something
                    {
                        if(hitInfo.transform.gameObject == player)
                        {
                            AlertSystem.Instance.Alert(player.transform.position);
                            return true;
                        }
                    }
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.white); //draw the ray in the editor
                }
            }
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        //make the enemy look at the player when it takes damage and enter in the attack state if it's not already in it
        transform.LookAt(player.transform);
        if(stateMachine.activeState.ToString() != "AttackState")
        {
           agent.SetDestination(transform.position); //stop the enemy from moving
            stateMachine.ChangeState(new AttackState());
        }
    }
}
