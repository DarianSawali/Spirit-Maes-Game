using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSwitch : MonoBehaviour
{
    public Tilemap tilemap1;
    public Tilemap tilemap2;

    public void SwitchTilemaps()
    {
        tilemap1.gameObject.SetActive(!tilemap1.gameObject.activeSelf);
        tilemap2.gameObject.SetActive(!tilemap2.gameObject.activeSelf);
    }
}
