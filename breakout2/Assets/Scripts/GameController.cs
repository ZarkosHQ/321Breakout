using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    [SerializeField]private PlayerInput playerInput;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;
    private InputAction launch;

    private bool isPaddleMoving;
    [SerializeField] private GameObject paddle;
    [SerializeField]private float paddleSpeed;

    private float moveDirection;

    [SerializeField] private GameObject brick;

    [SerializeField] private TMP_Text scoretext;
    [SerializeField] private int score;

    [SerializeField] private TMP_Text endGameText;

    private BallController ball;

    [SerializeField] private TMP_Text livesText;
    private int lives;

    [SerializeField] private TMP_Text restartText;
    [SerializeField] private TMP_Text launchText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerSetup();
        BrickCreate();

        endGameText.gameObject.SetActive(false);

        ball = GameObject.FindObjectOfType<BallController>();

        lives = 3;

        scoretext.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();

        restartText.gameObject.SetActive(false);
        launchText.gameObject.SetActive(true);
    }
    public void LoseALife()
    {
        lives--;
        livesText.text = "Lives: " + lives.ToString();
        launchText.gameObject.SetActive(true);

        if (lives == 0)
        {
            endGameText.text = "YOU HAVE FAILED!";
            endGameText.gameObject.SetActive(true);
            ball.StopBall();
            paddle.SetActive(false);
            restartText.gameObject.SetActive(true);
            launchText.gameObject.SetActive(false);
        }
    }

    public void UpdateScore()
    {
        score += 100;
        scoretext.text = "Score: " + score.ToString();
        if (score >= 4000)
        {
            endGameText.text = "You Win!!!!";
            endGameText.gameObject.SetActive(true);
            ball.StopBall();
            restartText.gameObject.SetActive(true);
            paddle.SetActive(false);
        }
    }

    private void BrickCreate()
    {
        Vector2 brickPos = new Vector2(-11, 0);
        for (int j = 0; j < 4; j++)
        {
            brickPos.y += 1;

            for (int i = 0; i < 10; i++)
            {
                brickPos.x += 2;
                Instantiate(brick, brickPos, Quaternion.identity);
            }
            brickPos.x = -11;
        }
    }

    void PlayerControllerSetup()
    {
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("MovePaddle");
        restart = playerInput.currentActionMap.FindAction("RestartGame");
        quit = playerInput.currentActionMap.FindAction("QuitGame");
        launch = playerInput.currentActionMap.FindAction("LaunchBall");

        move.started += Move_started;
        move.canceled += Move_canceled;
        restart.started += Restart_started;
        quit.started += Quit_started;
        launch.started += Launch_started;

        isPaddleMoving = false;

    }

    private void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        restart.started -= Restart_started;
        quit.started -= Quit_started;
        launch.started -= Launch_started;
    }

    private void Launch_started(InputAction.CallbackContext obj)
    {
        ball.LaunchTheBall();
        launchText.gameObject.SetActive(false);
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isPaddleMoving = false;
    }
    private void Move_started(InputAction.CallbackContext obj)
    {
        isPaddleMoving = true;
    }
    private void Quit_started(InputAction.CallbackContext obj)
    {

    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(1);

        restartText.gameObject.SetActive(false);
    }



    private void FixedUpdate()
    {
        if (isPaddleMoving)
        {
            //move paddle
            paddle.GetComponent<Rigidbody2D>().velocity = new Vector2(paddleSpeed * moveDirection, 0);
        }
        else
        {
            //stop paddle
            paddle.GetComponent<Rigidbody2D>().velocity =  Vector2.zero;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaddleMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }
}
