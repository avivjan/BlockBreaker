using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Config Params
    [SerializeField] float ScreenWidthInUnits = 16f;
    [SerializeField] float MinXPos = 1f;
    [SerializeField] float MaxXPos = 15f;
    public float velocity;
    

    //cached refferences
    GameSession gameSession;
    Ball ball;
    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        float oldXPos = transform.position.x;
        if (tag == "Second Paddle")
        {
            transform.position = new Vector2(GetNewPaddleXPos() + ScreenWidthInUnits, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(GetNewPaddleXPos(), transform.position.y);
        }
        float newPos = transform.position.x;
        velocity = newPos - oldXPos;
    }

    private float GetNewPaddleXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return Mathf.Clamp(ball.transform.position.x, MinXPos, MaxXPos);
        }
        var newXPos = transform.position.x + (Input.GetAxis("Mouse X") * Time.deltaTime);
        return Mathf.Clamp(newXPos, MinXPos, MaxXPos);
    }
}
