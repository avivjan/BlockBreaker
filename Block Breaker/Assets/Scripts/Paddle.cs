using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Config Params
    [SerializeField] float ScreenWidthInUnits = 16f;
    [SerializeField] float MinXPos = 1f;
    [SerializeField] float MaxXPos = 15f;
    [SerializeField] public float velocity;

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
        transform.position = new Vector2(GetNewPaddleXPos(), transform.position.y);
        float newPos = transform.position.x;
        velocity = newPos - oldXPos;
    }

    private float GetNewPaddleXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return Mathf.Clamp(ball.transform.position.x, MinXPos, MaxXPos);
        }
        float mouseXPosInUnits = Input.mousePosition.x / Screen.width * ScreenWidthInUnits;
        return Mathf.Clamp(mouseXPosInUnits, MinXPos, MaxXPos);
    }
}
