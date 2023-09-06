using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject paddle;
    private bool isBallInPlay;

    void Start()
    {
        ResetBall();
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

    // Update is called once per frame
    void Update()
    {

    }
}
