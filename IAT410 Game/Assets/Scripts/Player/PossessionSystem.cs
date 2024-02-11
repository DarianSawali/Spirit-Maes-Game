// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PossessionSystem : MonoBehaviour
// {
//     public GameObject currentHost; // The current body the player's soul is inhabiting
//     private GameObject playerSoul; // Reference to the player's soul

//     void Awake()
//     {
//         playerSoul = this.gameObject; // Assuming this script is attached to the player's soul
//     }

//     public void Possess(GameObject target)
//     {
//         if (currentHost != null)
//         {
//             Debug.LogError("Already possessing an entity.");
//             return;
//         }

//         // Example: Turn off player soul visuals and disable its control
//         playerSoul.SetActive(false);
//         currentHost = target;

//         // Enable the animal's control script
//         var control = target.GetComponent<AnimalControl>();
//         if (control != null)
//         {
//             control.enabled = true;
//         }
//     }

//     public void Dispossess()
//     {
//         if (currentHost == null)
//         {
//             Debug.LogError("Not possessing any entity.");
//             return;
//         }

//         // Example: Reactivate player soul at the host's position and re-enable its control
//         playerSoul.transform.position = currentHost.transform.position;
//         playerSoul.SetActive(true);

//         // Disable the animal's control script
//         var control = currentHost.GetComponent<AnimalControl>();
//         if (control != null)
//         {
//             control.enabled = false;
//         }

//         currentHost = null;
//     }
// }
