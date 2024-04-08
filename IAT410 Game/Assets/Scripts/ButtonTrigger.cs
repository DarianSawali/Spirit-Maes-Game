using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    public Door door; // Reference to the door script
    private TilemapSwitch tilemapSwitch;
    public bool isPressed = false;

    public CameraFollowVertical cameraScript; // call camera

    public AudioManager audioManager;
    public AudioClip click;
    public AudioClip ping;

    private int panCounter = 0; // only pan to gate when first button is pressed
    // used for levels with multiple buttons

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal") || other.CompareTag("Player") || other.CompareTag("Skunk") || other.CompareTag("Pigeon") || other.CompareTag("Fish"))
        {
            if (!isPressed)
            {
                isPressed = true;
                audioManager.PlaySoundEffect(click);
                door.ButtonPressed();
                TileSwitch();

                audioManager.PlaySoundEffect(ping);

                if (panCounter < 1) ;
                {
                    cameraScript.PanToGate();
                    panCounter++;
                }

                Debug.Log("Button Pressed");
            }
        }
    }

    public void resetPanCounter()
    {
        panCounter = 0;
        Debug.Log(panCounter);
    }

    public void TileSwitch()
    {
        TilemapSwitch tilemapSwitch = GetComponent<TilemapSwitch>();
        tilemapSwitch.SwitchTilemaps();
    }

}

// private bool isPressed = false;
// public bool IsPressed { get { return isPressed; } }

// public Door door;

// private void OnTriggerEnter(Collider other)
// {
//     if (other.CompareTag("Animal") || other.CompareTag("Player") || other.CompareTag("Skunk") || other.CompareTag("Pidgeon") || other.CompareTag("Fish"))
//     {
//         TilemapSwitch tilemapSwitch = GetComponent<TilemapSwitch>();
//         tilemapSwitch.SwitchTilemaps();
//         isPressed = true;
//         Debug.Log("Button Pressed");
//     }

// }

