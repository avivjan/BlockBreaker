using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackBaloon : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
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
            FindObjectOfType<Ball>().DestoryBall();
            GetComponent<SpriteRenderer>().enabled = false;
            await Task.Run(() => { Thread.Sleep(150); });
            SceneManager.LoadScene("Game Over");
        }
    }
}
