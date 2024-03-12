using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    Animator animator;
    public GameObject Player;

    PlayerMovement player;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    public int health;
    public int maxHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        int health = maxHealth;
        Animator animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            health--;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            health++;
        }

        switch (health)
        {
            case 0:
                {
                    Heart1.gameObject.SetActive(false);
                    Heart2.gameObject.SetActive(false);
                    Heart3.gameObject.SetActive(false);
                    break;
                }
            case 1:
                {
                    Heart1.gameObject.SetActive(true);
                    Heart2.gameObject.SetActive(false);
                    Heart3.gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    Heart1.gameObject.SetActive(true);
                    Heart2.gameObject.SetActive(true);
                    Heart3.gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    Heart1.gameObject.SetActive(true);
                    Heart2.gameObject.SetActive(true);
                    Heart3.gameObject.SetActive(true);
                    break;
                }
        }
    }

}


