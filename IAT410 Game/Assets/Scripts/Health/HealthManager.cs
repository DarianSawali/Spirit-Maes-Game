using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    Animator animator;
    public GameObject Player;

    Player player;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject Heart4;

    public int health;
    public int maxHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        Animator animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //testing purposes
        if (Input.GetKeyDown(KeyCode.U))
        {
            decreaseHealth();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            addHealth();
        }

        switch (health)
        {
            case 0:
                {
                    Heart1.gameObject.SetActive(false);
                    Heart2.gameObject.SetActive(false);
                    Heart3.gameObject.SetActive(false);
                    Heart4.gameObject.SetActive(false);
                    break;
                }
            case 1:
                {
                    Heart1.gameObject.SetActive(true);
                    Heart2.gameObject.SetActive(false);
                    Heart3.gameObject.SetActive(false);
                    Heart4.gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    Heart1.gameObject.SetActive(true);
                    Heart2.gameObject.SetActive(true);
                    Heart3.gameObject.SetActive(false);
                    Heart4.gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    Heart1.gameObject.SetActive(true);
                    Heart2.gameObject.SetActive(true);
                    Heart3.gameObject.SetActive(true);
                    Heart4.gameObject.SetActive(false);
                    break;
                }
            case 4:
                {
                    Heart1.gameObject.SetActive(true);
                    Heart2.gameObject.SetActive(true);
                    Heart3.gameObject.SetActive(true);
                    Heart4.gameObject.SetActive(true);
                    break;
                }
        }
    }

    public void decreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void addHealth()
    {
        if (health <= maxHealth)
        {
            health++;
        }
    }

}


