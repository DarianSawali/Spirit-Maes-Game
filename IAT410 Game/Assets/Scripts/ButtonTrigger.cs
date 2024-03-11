using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    private TilemapSwitch tilemapSwitch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal") || other.CompareTag("Player"))
        {
            TilemapSwitch tilemapSwitch = GetComponent<TilemapSwitch>();
            tilemapSwitch.SwitchTilemaps();
            Debug.Log("Button");
        }

    }
}
