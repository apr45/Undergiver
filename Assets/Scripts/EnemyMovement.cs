using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour{
    // public variables
        // reference to the player's transform
        public Transform player;

        // speed at which the enemy moves towards the player
        public float speed;

        // enemy's detection distance to the player
        public float detectionRange;

        // number of seconds enemy has to not move
        public static float stunTime;

    // private variables
        // reference to the NavMeshAgent component for pathfinding
        private NavMeshAgent navMeshAgent;

        // variable to track if the player is within the enemy's detection range
        private bool isPlayerInRange = false;

        // variable to store the initial position of the enemy
        private Vector3 initialPosition;

        // reference script for despawning potion
        private DestroyPotion destroyPotion;


    // called once before the first execution of Update
    private void Start(){
        // get and store the NavMeshAgent component attached to this object
        navMeshAgent = GetComponent<NavMeshAgent>();

        // set the NavMeshAgent's speed to the specified speed variable
        navMeshAgent.speed = speed;

        // store the initial position of the enemy.
        initialPosition = transform.position;

        // get and store the potionDestroy script component attached to this object
        destroyPotion = GetComponent<DestroyPotion>();

        // sets up initial wait time
        stunTime = 5;
   }

    // enemy follows player if within a certain range
    private void Update(){
        // calculate the distance between the enemy and the player.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // check if the player is within the detection range.
        if (distanceToPlayer <= detectionRange){
            isPlayerInRange = true;
        } else {
            isPlayerInRange = false;
        }

        // check if the player is within range to determine the enemy's behavior.
        if (isPlayerInRange){
            // if the player is within range, set the enemy's destination to the player's position.
            navMeshAgent.SetDestination(player.position);
        } else {
            // if the player is out of range, stop the enemy's movement.
            navMeshAgent.SetDestination(initialPosition);
        }
    }

    // zombie stops movement if hit by potion
    private void OnCollisionEnter(Collision collision){
        if (destroyPotion != null && collision.gameObject.CompareTag("Potion")){
            Debug.Log("Potion hit Zombie");
            destroyPotion.OnCollisionEnter(collision);
            
            StartCoroutine(FreezeMovement(stunTime));
        }
    }

    // pauses zombie movement for a few seconds
    private IEnumerator FreezeMovement(float waitTime){
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(waitTime);
        navMeshAgent.isStopped = false;
    }
}
