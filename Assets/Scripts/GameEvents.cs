using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public float powerUpDuration = 5f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public AudioSource powerUpAudio;

    public Observer observer;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    bool poweredUp;
    float m_Timer;
    bool m_HasAudioPlayed;


    public GameObject? directionalLightObject;
    private Light directionalLight;

    void Start()
    {
        if (directionalLightObject != null)
        {
            directionalLight = directionalLightObject.GetComponent<Light>();
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    public void PlayerGotPowerUp()
    {

        poweredUp = true;
        // player.Speed = 5;

        // loop for a certain time
        // trigger audio
        // make player fast
        // change light colour

    }

    void PowerUp(AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
            directionalLight.color = Color.green;
        }

        m_Timer += Time.deltaTime;

        if (m_Timer > powerUpDuration)
        {
            // restore setting after powerup is done
            poweredUp = false;
            audioSource.Stop();
            m_HasAudioPlayed = false;
            observer.PowerUpDone();

            m_Timer = 0f;
            directionalLight.color = Color.red;

        }
    }

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
        else if (poweredUp)
        {
            PowerUp(powerUpAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}