using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Volplane;
using Volplane.AirConsole;

public class GameController_Cars : VolplaneBehaviour
{
    // Public variables
    public Text pauseText;              // UI text object -> displays that not enough players have connected yet
    public Text scoreBoard;             // UI text object -> displays the score of the two players
    public Rigidbody2D car;            // Balls rigidbody
    public float carSpeed = 3f;        // Speed variable for the ball movement

    // Private variables
    private bool gameStarted = false;   // Flag -> indicator for game state
    private float initialCarSpeed;     // Initial ball speed (for resetting speed)


    /// <summary>
    /// 'MonoBehaviour.Start()' method from Unity
    /// Start is called on the frame when a script is enabled just before any of the Update methods
    /// is called the first time.
    /// </summary>
    private void Start()
    {
        SetStandardView("waiting");
        pauseText.text = "0 Players connected\nWaiting for more players...";
        initialCarSpeed = carSpeed;
    }

    /// <summary>
    /// 'Volplane.OnConnect()' method from the Volplane framework
    /// OnConnect is called when a new AirConsole player joins the session.
    /// </summary>
    /// <param name="player">The player object of the connected device.</param>
    private void OnConnect(VPlayer player)
    {
        // Set player inactive if it is active and game not started yet
        if(player.IsActive && !gameStarted)
            player.SetActive(false);

        // You will not receive any input from inactive players.
        // By default every new connected player will be set as inactive, with the exception
        // of the game master. The game master (AirConsoles master device) is the one who is able
        // to navigate on the AirConsole platform.

        // Display a text on the controller indicating which racket this player will play
        if(player.PlayerId == 0)
            player.ChangeElementText("infoText", player.Nickname);
        else
            player.ChangeElementText("infoText", player.Nickname);

        // In this example, the player with the id 0 will play the left racket, the player
        // with the id 1 will play the right one.
        // Remember: The game master may not necessarily have an id of 0. The player id are ordered
        // by whichever device connects first. However player ids can be hardcoded. For example if
        // the game master has the player id 3 and suddenly looses connection, on a rejoin, the
        // controller will be reassigned to this id and player object.
            
        // Update pause text with the current player count
        pauseText.text = string.Format("{0} Players connected\nWaiting for more players...", PlayerCount);

        // When two players are connected and game has not started yet
        // -> let's go
        if((PlayerCount == 2) && !gameStarted)
            StartGame();
    }

    /// <summary>
    /// 'Volplane.OnDisconnect()' method from the Volplane framework
    /// OnDisconnect is called when an AirConsole player left the session.
    /// </summary>
    /// <param name="player">The player object of the disconnected device.</param>
    private void OnDisconnect(VPlayer player)
    {
        // If the player was active while connected -> stop this game.
        // Meaning this player was actually playing.
        if(player.IsActive)
            StopGame();
    }

    /// <summary>
    /// 'MonoBehaviour.FixedUpdate()' method from Unity
    /// FixedUpdate is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {

    }

    /// <summary>
    /// Starts the pong game.
    /// </summary>
    private void StartGame()
    {
        gameStarted = true;             // Set flag -> game started
        pauseText.text = "Welcome to my domain";      // Change pause text

        // Set two players active (starting from id = 0)
        // This will enable the first two connected players.
        SetPlayersActive(2);

        // Change the controller view for all active players (to the view named 'game')
        ChangeViewAllActive("game");
        ChangeView(GetPlayer(1), "player2");

        // Display a text on the controllers indicating which racket each player plays
        GetPlayer(0).ChangeElementText("infoText", GetPlayer(0).Nickname);
        GetPlayer(1).ChangeElementText("infoText", GetPlayer(1).Nickname);

        // Reset ball and start
        ResetBall(true);
    }

    /// <summary>
    /// Stops the game.
    /// </summary>
    private void StopGame()
    {
        // Set flag -> game stopped
        gameStarted = false;

        // Reset the displayed score
        scoreBoard.text = "0 | 0";

        // Show pause text again and updating it with the current player count
        pauseText.enabled = true;
        pauseText.text = string.Format("{0} Players connected\nWaiting for more players...", PlayerCount);

        // Set all players inactive
        // No controller inputs will be processed.
        SetAllPlayersInactive();

        // Change the view for all players
        ChangeViewAll("waiting");

        // Reset ball without restart
        ResetBall(false);
    }

    /// <summary>
    /// Resets the ball.
    /// </summary>
    /// <param name="restart">If set to <c>true</c> restart.</param>
    private void ResetBall(bool restart)
    {
        // Set ball position back to its origin and reset speed
        car.position = Vector2.zero;
        carSpeed = initialCarSpeed;

        // Restart the game?
        if(restart)
        {
            // Apply a random force with specified speed to the ball
            Vector2 startDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.2f));
            car.velocity = startDir.normalized * carSpeed;
        }
        else
        {
            // Remove force from ball
            car.velocity = Vector2.zero;
        }
    }
}