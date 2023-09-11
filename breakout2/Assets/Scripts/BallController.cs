using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject paddle;
    private bool isBallInPlay;
    private GameController gameController;

    void Start()
    {
        ResetBall();
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    public void ResetBall()
    {
        gameObject.transform.position = paddle.transform.position + new Vector3(0, 0.5f);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isBallInPlay = false;
    }

    public void LaunchTheBall()
    {
        if (!isBallInPlay)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
            isBallInPlay = true;
        }
    }

    public void StopBall()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBallInPlay)
        {
            gameObject.transform.position = paddle.transform.position + new Vector3(0, 0.5f);
        }
        else if (gameObject.transform.position.y < -6)
        {
            //lose a life
            gameController.LoseALife();
            //reset ball
            ResetBall();
        }
    }
}
