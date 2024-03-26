using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Sea : MonoBehaviour
{

    [SerializeField] ParticleSystem splashParticle, brightSplashParticle, bigSplashParticle;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] public static Animator scoreBar, backBar, frontBar;
    [SerializeField] GameObject backSB, frontSB, scoreSB;
    [SerializeField] Material stage1, stage2, stage3, stage4;
    public static int score;
    int radioactiveSquidCount=0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = stage1;
        scoreBar = scoreSB.GetComponent<Animator>();
        backBar = backSB.GetComponent<Animator>();
        frontBar = frontSB.GetComponent<Animator>();
        scoreText.text = "00000";
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 100)
        {
            scoreText.text = "00" + score;
        }
        if (score >= 1000)
        {
            scoreText.text = "0" + score;
        }
        if (score >= 10000)
        {
            scoreText.text = "" + score;
            
        }
        if (radioactiveSquidCount >= 15)
        {
            gameObject.GetComponent<Renderer>().material = stage2;
        }
        if (radioactiveSquidCount >= 30)
        {
            gameObject.GetComponent<Renderer>().material = stage3;
        }
        if (radioactiveSquidCount >= 60)
        {
            gameObject.GetComponent<Renderer>().material = stage4;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.Euler(270,0,0);
        Vector3 pos = contact.point;

        if (collision.gameObject.CompareTag("Fish"))
        {
            
            score += 50;
            //backBar.SetTrigger("Score");
            //frontBar.SetTrigger("Score");
            scoreBar.SetTrigger("Score");
            ParticleSystem lilSplash = Instantiate(splashParticle, pos, rot);
            Debug.Log(contact);
            Destroy(collision.gameObject);
            Destroy(lilSplash, 2f);
        }
        if (collision.gameObject.CompareTag("NeonSquid"))
        {
            radioactiveSquidCount++;
            score += 200;
            //backBar.SetTrigger("Score");
            //frontBar.SetTrigger("Score");
            scoreBar.SetTrigger("AtomScore");
            ParticleSystem lilSplash = Instantiate(brightSplashParticle, pos, rot);
            Debug.Log(contact);
            Destroy(collision.gameObject);
            Destroy(lilSplash, 150f* Time.deltaTime);
        }
        if (collision.gameObject.CompareTag("BigSquid"))
        {
            ParticleSystem bigSlpash = Instantiate(bigSplashParticle, pos, rot);
            Debug.Log(contact);
            GameManager.squidCount--;
            Destroy(collision.gameObject);
            Destroy(bigSlpash, 150f*Time.deltaTime);
        }

    }

}
