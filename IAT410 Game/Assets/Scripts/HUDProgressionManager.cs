using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDProgressionManager : MonoBehaviour
{
    public Sprite[] levelProgressionSprites; // Assign all your level sprites in the inspector
    private Image progressBarImage;

    private void Start()
    {
        progressBarImage = GetComponent<Image>();
        if (progressBarImage == null)
        {
            Debug.LogError("HUDProgressionManager: No Image component found on this GameObject.");
        }
        UpdateLevelProgress();
    }

    void Update()
    {
        UpdateLevelProgress();
    }

    public void UpdateLevelProgress()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            progressBarImage.sprite = levelProgressionSprites[0];
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            progressBarImage.sprite = levelProgressionSprites[1];
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            progressBarImage.sprite = levelProgressionSprites[2];
        }

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            progressBarImage.sprite = levelProgressionSprites[3];
        }

        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            progressBarImage.sprite = levelProgressionSprites[4];
        }

        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            progressBarImage.sprite = levelProgressionSprites[5];
        }

        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            progressBarImage.sprite = levelProgressionSprites[6];
        }

        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            progressBarImage.sprite = levelProgressionSprites[7];
        }

        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            progressBarImage.sprite = levelProgressionSprites[8];
        }

        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            progressBarImage.sprite = levelProgressionSprites[9];
        }

        // if (SceneManager.GetActiveScene().buildIndex == 12)
        // {
        //     progressBarImage.sprite = levelProgressionSprites[10];
        // }
    }
}