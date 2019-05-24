using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Workshop
{
    public class GameManager : MonoBehaviour
    {
        //An enum is is a set of named constants which increase readibility of code
        //Gamestate are all possible the states for our game manager
        public enum GameState
        {
            Start,
            Main,
            GameOver
        }

        //Keeps track of the current game state
        public GameState CurrentGameState;
        //References to the gameobjects that contain the menus so we can easily enable/disable
        public GameObject StartMenu, VictoryMenu;
        //References to the gameobjects that contain the Game UI so we can easily enable/disable
        public GameObject GameUI;

        //UI element for the health
        public Text HealthText;
        //UI element for the score
        public Text ScoreText;

        //Player's score
        public int Score = 0;
        //Total number of collectables at the start of the game
        public int NumberOfCoins = 0;

        //Reference to the player for convenience
        public Player Player;
        //Health that the player starts with at the start of the game and when they respawn
        public int InitialHealth = 3;
        //Point of origin for the player to appear at when the game starts/when they respawn
        public Transform SpawnPoint;

        //Container for all the coins
        public GameObject Coins;
        //Container for all the enemies
        public GameObject Enemies;

        //Static instance of GameManager which allows it to be accessed by any other script.
        public static GameManager instance = null;

        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

        }

        //Main game loop
        public void Update()
        {
            //A switch case is a selection statement that chooses a single switch section to execute
            //Could be written as a chain of if else statements

            switch (CurrentGameState)
            {
                case GameState.Start:
                    //Ensures that the score is zero when the game starts
                    Score = 0;
                    //Checks for keyboard input of the spacebar
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        //Starts the game
                        StartGame();
                    }
                    break;

                case GameState.Main:

                    break;

                case GameState.GameOver:
                    //Checks for keyboard input of the spacebar
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //Restarts the game
                        RestartGame();
                    }
                    break;
            }

            //Debug.Log("This is a log");
            //Debug.LogError("This is an error");
            //Debug.LogWarning("This is a warning");
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            //Sets the gamestate into the main loop
            CurrentGameState = GameState.Main;
            //Turns off the start menu
            StartMenu.SetActive(false);
            //Turns on the Score and health UI
            GameUI.SetActive(true);
            //Resets the player to the starting position
            SpawnPlayer();
            //Turns on the coins
            Coins.SetActive(true);
            //Turns on the enemies
            Enemies.SetActive(true);
            //Updates health UI to show the correct health
            UpdateHealthText();
            //Determines how many coin objects exist in the scene
            CountCoins();
        }

        /// <summary>
        /// Game is complete
        /// </summary>
        public void GameOver()
        {
            CurrentGameState = GameState.GameOver;
            VictoryMenu.SetActive(true);
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame()
        {
            //Reloads the scene
            SceneManager.LoadScene(0);
            //Deletes this object
            Destroy(gameObject);
        }

        /// <summary>
        /// Determines how many coins there are in the scene
        /// </summary>
        public void CountCoins()
        {
            //Finds all the objects in the scene with a coin component attached to it and creates a list
            //Gets the length of the list
            NumberOfCoins = GameObject.FindObjectsOfType<Coin>().Length;
            //Updates the score text
            ScoreText.text = "Coins: " + Score + "/" + NumberOfCoins;
        }

        /// <summary>
        /// Sets the player's location to the spawnpoint and initializ
        /// </summary>
        public void SpawnPlayer()
        {
            //Sets the player's position and rotation to the spawn point
            Player.transform.SetPositionAndRotation(SpawnPoint.localPosition, SpawnPoint.localRotation);
            //Sets player's health to initial value
            Player.ModifyHealth(InitialHealth);
            //Ensures that the player doesn't have an old destination for pathfinding
            Player.SetDestination(SpawnPoint.position);
        }

        /// <summary>
        /// Modifies the score.
        /// </summary>
        /// <param name="amount">amount to increase the score by</param>
        public void ModifyScore(int amount)
        {
            Score += amount;
            ScoreText.text = "Coins: " + Score + "/" + NumberOfCoins;
            if (Score >= NumberOfCoins)
            {
                GameOver();
            }
        }

        public void UpdateHealthText()
        {
            HealthText.text = "Health: " + Player.Health;
        }

    }
}


