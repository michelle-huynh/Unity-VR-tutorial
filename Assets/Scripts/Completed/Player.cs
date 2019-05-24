using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CompletedSolution
{
    public class Player : MonoBehaviour
    {
        //For storing the player's current health
        public int Health;

        //Reference to the navmesh agent component that controls the player's movement
        private NavMeshAgent Agent;

        //On Enable runs whenever the gameobject is activated.
        //This can occur when the game object is instanitated,
        //or when it is enabled with gameobject.SetActive(true)
        void OnEnable()
        {
            //Finds the Navmesh component on the player game object.
            Agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            // Move this object to the position clicked by the mouse.
            if (Input.GetMouseButtonDown(0))
            {
                //temporary variable that stores the hit value of the raycast
                RaycastHit hit;
                //Creates a temporary variable that stores a ray that originating from the mouse position
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Checks to see if the raycast of ray hit anything and stores the results in hit
                if (Physics.Raycast(ray, out hit))
                {
                    //Ensures that hit has a transform component (this is redundant and should always be true)
                    if (hit.transform != null)
                    {
                        //Sets the player's navmesh destination to the point where the player clicked
                        SetDestination(hit.point);
                    }
                }

            }
        }

        //A wrapper for the navmesh agent so that other scripts like the game manager can set the destination
        public void SetDestination(Vector3 destination)
        {
            //sets the destination of the nav mesh agent
            Agent.SetDestination(destination);
        }

        //modifies the player's health based off of an amount
        public void ModifyHealth(int amount)
        {
            //Adds the amount to the health (if negative number it'll subtract)
            Health += amount;
            //Updates the UI in the game manager to reflect the change in health
            GameManager.instance.UpdateHealthText();
            //If the player's health drops to zero, respawn the player
            if (Health <= 0)
            {
                //Respawns the player
                GameManager.instance.SpawnPlayer();
            }
        }
    }

}
