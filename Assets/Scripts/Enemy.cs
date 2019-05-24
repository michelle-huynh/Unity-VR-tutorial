using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Workshop
{
    public class Enemy : MonoBehaviour
    {

        //How much damage does the enemy deal to the player
        public int Damage = 1;


        //Enemy navigation
        //#regions let you collapse code blocks
        #region Navigation
        public float MinRadius, MaxRadius;
        public float MinWander, MaxWander;
        private NavMeshAgent Agent;
        #endregion 

        void OnEnable()
        {
            //Finds the navmesh agent component on this gameobject
            Agent = GetComponent<NavMeshAgent>();
            //Starts the enemy's movement
            Wander();
        }

        //Called whenever an object enters the trigger
        private void OnTriggerEnter(Collider other)
        {
            //Checks if the object that entered the trigger has the tag player
            if (other.tag == "Player")
            {
                //Gets the reference to the player component on the player
                var player = other.GetComponent<Player>();
                //Ensures that the player has a player component
                if (player != null)
                {
                    //Attacks the player
                    Attack(player);
                }
                else
                {
                    //Prints an error to the console 
                    Debug.LogError("Player has no Player Component");
                }
            }
        }

        /// <summary>
        ///  Deals Damage to the player
        /// </summary>
        /// <param name="player"></param>
        private void Attack(Player player)
        {
            //Modifies the player's health based on the damage. Note the negative sign as we use the same method for increasing/decreasing health
            player.ModifyHealth(-Damage);
        }

        /// <summary>
        /// Enemy movement on the navmesh
        /// </summary>
        private void Wander()
        {
            //Randomly picks a radius between the ranges
            var WanderRadius = Random.Range(MinRadius, MaxRadius);
            //Randomly picks an in interval for the enemy to move
            var WanderTime = Random.Range(MinWander, MaxWander);
            //Sets the destination to the new random location
            Agent.SetDestination(RandomNavmeshLocation(WanderRadius));
            //Calls Wander again after the random interval
            Invoke("Wander", WanderTime);
        }

        //Picks a random spot within the radius for the enemy to move to
        public Vector3 RandomNavmeshLocation(float radius)
        {
            //Picks a random direction and distance based off of the radius
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            //Adds the new location to the current location
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            //Checks to make sure the position is valid
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }
    }

}

