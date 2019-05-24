using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Workshop
{
    [RequireComponent(typeof(AudioSource))]
    public class Coin : MonoBehaviour
    {

        //Rotation value of each axis
        public float RotX, RotY, RotZ;

        //Speed at which the coin rotates
        public float RotationSpeed = 5;
        //Audio source for when the coin is picked up
        private AudioSource audioSource;
        //Point value for the coin
        public int value = 1;

        //Called whenever the object is activated
        public void OnEnable()
        {
            //Finds the reference to the audiousource component on the gameobject
            audioSource = GetComponent<AudioSource>();
        }

        //Rotates the cube every frame
        public void Update()
        {
            transform.Rotate(new Vector3(RotX, RotY, RotZ) * RotationSpeed * Time.deltaTime);
        }

        //Called whenever an object enters the trigger
        private void OnTriggerEnter(Collider other)
        {
            //Checks if the object that entered the trigger has the tag player
            if (other.tag == "Player")
            {
                //Collects the coin
                Collected();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Collected()
        {
            //Modifies the player's score based off of the value of the coin
            GameManager.instance.ModifyScore(value);
            //Plays collection sound
            audioSource.Play();
            //Destroys the object after it is collected
            GameObject.Destroy(gameObject);
        }
    }

}

