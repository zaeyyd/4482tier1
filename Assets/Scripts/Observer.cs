using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEvents gameEvents;
    public bool powerUp;
    static bool playerPoweredUp;

    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }



    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    public void PowerUpDone()
    {
        playerPoweredUp = false;
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    if (powerUp)
                    {
                        gameEvents.PlayerGotPowerUp();
                        playerPoweredUp = true;

                    }
                    else
                    {
                        if (playerPoweredUp)
                        {
                            this.transform.parent.gameObject.SetActive(false);
                        }
                        else
                        {
                            gameEvents.CaughtPlayer();
                        }

                    }

                }
            }
        }
    }
}