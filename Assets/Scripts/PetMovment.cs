using UnityEngine;
using UnityEngine.AI;

public class PetMovment : MonoBehaviour{
    //public variables
        // reference to the player's transform.
        public Transform player;

         // maximum distance the pet will follow the player before teleporting.
        public float maxDistance;

        // maxium distance pet will stay away from player; prevents collision with player when moving
        public float stopDistance;

    // private variables
        // reference to the NavMeshAgent component for pathfinding.
        private NavMeshAgent navMeshAgent;

        // variable to store the distance between the pet and the player.
        private Vector3 distanceToPlayer;

    // gets and sets necessary enemy components.
    private void Start(){
        // get the NavMeshAgent component attached to this GameObject.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // set the NavMeshAgent stopping distance.
        navMeshAgent.stoppingDistance = stopDistance;
    }

    // pet follows player constantly 
    private void Update(){
        // set the pet's destination to the player's position if the player is not null.
        if (player != null){
            // use the NavMeshAgent to move towards the player's position.
            navMeshAgent.SetDestination(player.position);
        }

        // calculate the distance between the pet and the player, and if it exceeds the maximum distance, teleport the pet to the player's position.
        distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude > maxDistance){
            transform.position = player.position;
        }
    }
}
