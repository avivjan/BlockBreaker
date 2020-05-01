using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config Params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject visualEffect;
    [SerializeField] int timeOfEffectDisplay = 500;
    [SerializeField] Sprite[] hitsSprites;
    private int scoreForBreak;

    //cached references
    Level level;
    GameSession gameStatus;
    SpriteRenderer spriteRenderer;
    Ball ball;
    Paddle paddle;

    // State vars.
    [SerializeField] int timesHit = 0; //Serialized for debugging porpuses.

    private void Start()
    {
        scoreForBreak = (hitsSprites.Length + 1) * 10;
        ball = FindObjectOfType<Ball>();
        gameStatus = FindObjectOfType<GameSession>();
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        paddle = FindObjectOfType<Paddle>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level.AddBreakableBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable" && collision.gameObject.tag.Equals("Ball"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitsSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlockAsync();
        }
        else
        {
            ShowNextHitsSprite();
        }
    }

    private void ShowNextHitsSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitsSprites[spriteIndex] != null)
        {
            spriteRenderer.sprite = hitsSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array " + gameObject.name);
        }
    }

    public void DestroyBlockAsync()
    {
        TriggerVisualEffect();
        PlayBlockDestroySound();
        level.RemoveBreakableObject();
        gameStatus.AddPointsToScore(scoreForBreak);
        Destroy(gameObject);
    }

    private void PlayBlockDestroySound()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerVisualEffect()
    {
        GameObject sparkles = Instantiate(visualEffect, transform.position, transform.rotation);
        Destroy(sparkles,timeOfEffectDisplay);
    }
}
