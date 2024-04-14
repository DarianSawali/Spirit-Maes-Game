using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class DigTrigger : MonoBehaviour
{
    public DigLocation digLocation;
    public Transform teleportLocation;
    public Tilemap tilemap;
    public TileBase animatedTile;
    public Vector3Int animatedTilePosition;
    
    protected void start(){
        //animatedTilemap = FindObjectOfType<Tilemap>();
    }

    // public void TriggerAnimation()
    // {
    //     if (animatedTilemap != null)
    //     {
    //         // Get the tile at the specified position
    //         TileBase tile = animatedTilemap.GetTile(animatedTilePosition);

    //         // Check if the tile exists and is an animated tile
    //         if (tile != null && tile.GetType() == typeof(Tile))
    //         {
    //             // Toggle the tile's animation state
    //             Tile animatedTile = (Tile)tile;
    //             animatedTilemap.SetTile(animatedTilePosition, animatedTile);
    //         }
    //     }
    // }

    // public void TriggerAnimation()
    // {
    //     Vector3Int tilePosition = new Vector3Int(0, 0, 0); 

    //     tilemap.SetTile(tilePosition, animatedTile);
    // }

//     public void TriggerAnimation()
// {
//     if (animatedTilemap != null)
//     {
//         TileBase tile = animatedTilemap.GetTile(animatedTilePosition);

//         if (tile != null && tile is AnimatedTile animatedTile)
//         {
//             // Set the tile flags to Loop Once to trigger the animation
//             animatedTilemap.SetTileFlags(animatedTilePosition, TileFlags.None);
//             animatedTilemap.SetTileFlags(animatedTilePosition, TileFlags.Animation);
//             animatedTilemap.RefreshTile(animatedTilePosition);
//         }
//     }
// }

    
}

