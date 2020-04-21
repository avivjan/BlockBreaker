using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class 
    WhiteBaloon : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    SceneLoader sceneLoader;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(UnityEngine.Random.Range(3, 10), UnityEngine.Random.Range(3, 10));
    }

    // Update is called once per frame
    void Update()
    {

    }
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            ball.DestoryBall();
            GetComponent<SpriteRenderer>().enabled = false;
            await DestroyAllBlocksAsync();
        }
    }

    private async Task DestroyAllBlocksAsync()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        foreach (var block in blocks)
        {
            block?.DestroyBlockAsync();
            await Task.Run(() => {Thread.Sleep(100);});
        }
    }
}
