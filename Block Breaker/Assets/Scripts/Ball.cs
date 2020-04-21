using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //configuration params
    [SerializeField] Paddle paddle;
    [SerializeField] float XPush = 0;
    [SerializeField] float YPush = 15f;
    [SerializeField] AudioClip[] BallSounds;
    [SerializeField] float randomFactorOfVelocityTweark = 0.2f;
    [SerializeField] float FactorAttacment = 1;
    [SerializeField] GameObject visualEffectBallDestroy;
    [SerializeField] int timeOfEffectDisplay = 500;
    [SerializeField] AudioClip breakSound;

    //states
    Vector2 paddleToBallDis;
    public bool hasStarted { get; private set; }

    //cached component refrences
    AudioSource myAudioSource;
    Rigidbody2D thisRigidBody2D;


    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody2D = GetComponent<Rigidbody2D>();
        paddle = FindObjectOfType<Paddle>();
        paddleToBallDis = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            thisRigidBody2D.velocity = new Vector2(XPush, YPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallDis;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweark = new Vector2(UnityEngine.Random.Range(0, randomFactorOfVelocityTweark),
                                             UnityEngine.Random.Range(0, randomFactorOfVelocityTweark));
        if (hasStarted)
        {
            GetPaddleVelocity(collision);
            AudioClip clip = BallSounds[UnityEngine.Random.Range(0, BallSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            thisRigidBody2D.velocity += velocityTweark;
        }
    }

    private void GetPaddleVelocity(Collision2D collision)
    {
        string paddleTag = "Paddle";
        if (collision.gameObject.tag.Equals(paddleTag))
        {
            float ballYVelocity = thisRigidBody2D.velocity.y;
            float oldBallXVelocity = thisRigidBody2D.velocity.x;
            float paddleXVelocity = paddle.velocity;
            float newBallXVelocity = oldBallXVelocity + FactorAttacment * paddleXVelocity;
            thisRigidBody2D.velocity = new Vector2(newBallXVelocity, ballYVelocity);
        }
    }

    public void DestoryBall()
    {
        TriggerVisualEffect();
        PlayBallDestroySound();
        gameObject.SetActive(false);
    }

    private void PlayBallDestroySound()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerVisualEffect()
    {
        GameObject sparkles = Instantiate(visualEffectBallDestroy, transform.position, transform.rotation);
        Destroy(sparkles, timeOfEffectDisplay);
    }
}
