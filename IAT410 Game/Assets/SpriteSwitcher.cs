using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public GameObject objectToSwitch;

    public Sprite newSprite;

    public void SwitchSprite()
    {
        if (objectToSwitch != null)
        {
            SpriteRenderer spriteRenderer = objectToSwitch.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("Sprite Renderer component not found on the object.");
            }
        }
        else
        {
            Debug.LogError("Object to switch is not assigned.");
        }
    }
}
