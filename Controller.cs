﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {

    public GameObject[] balls;
    public GameObject background;
    public GameObject GameOver;
    
    public GameObject badView;
    public GameObject tutoriel;

    public Movement m;
    public Text timer;
    public Camera cam1;

    float maxWidth;
    public float timeLeft;
    public bool startedPlaying;

    
    // Use this for initialization
    void Start()
    {
        startedPlaying = false;
        ResizeBackground(background);

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam1.ScreenToWorldPoint(upperCorner);
        float ballWidth = (balls[0].GetComponent<Renderer>().bounds.extents.x) / 2f;
        maxWidth = targetWidth.x - ballWidth;

        timeLeft = 60;
       
    }
    void ResizeBackground(GameObject background)
    {
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();

        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.1f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = background.transform.localScale;
        xWidth.x = worldScreenWidth / width;
        background.transform.localScale = xWidth;

        Vector3 yHeight = background.transform.localScale;
        yHeight.y = worldScreenHeight / height;
        background.transform.localScale = yHeight;

    }

    void FixedUpdate()
    {
        if (startedPlaying)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timer.text = ": 0";
            }
            else timer.text = ": " + Mathf.RoundToInt(timeLeft);
        }

    }

    public IEnumerator Spawn()
    {
        while (timeLeft > 0)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0.0f);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject ball = balls[Random.Range(0, balls.Length)];
            Instantiate(ball, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
        }

    }

   
}
