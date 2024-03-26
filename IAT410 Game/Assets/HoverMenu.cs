using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverMenuPrefab;
    private GameObject hoverMenuInstance;

    public List<GameObject> hoverPreventers; 
    public float hoverExitDelay = 3.5f;
    private Coroutine hoverExitCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverMenuPrefab != null && hoverMenuInstance == null)
        {
            Vector3 position = transform.position + new Vector3(0f, 0f, 0f); 
            hoverMenuInstance = Instantiate(hoverMenuPrefab, position, Quaternion.identity);
            hoverMenuInstance.transform.SetParent(transform.parent); 
            Debug.Log("Pointer enter: " + eventData.pointerEnter);

            if (hoverExitCoroutine != null)
            {
                StopCoroutine(hoverExitCoroutine);
                hoverExitCoroutine = null;
            }
        }
    }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     if (hoverMenuInstance != null && !IsHoveringOverPreventer(eventData))
    //     {
    //         Destroy(hoverMenuInstance);
    //         hoverMenuInstance = null;
    //         Debug.Log("Pointer exit: ");

    //     }
    // }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsHoveringOverPreventer(eventData))
        {
            hoverExitCoroutine = StartCoroutine(StartHoverExitTimer());
        }
    }

    private bool IsHoveringOverPreventer(PointerEventData eventData)
    {
        foreach (GameObject preventer in hoverPreventers)
        {
            if (eventData.pointerEnter != null && eventData.pointerEnter == preventer)
            {
                return true;
                
            }
        }
        return false;
    }

    private IEnumerator StartHoverExitTimer()
    {
        yield return new WaitForSeconds(hoverExitDelay);

        if (hoverMenuInstance != null)
        {
            Destroy(hoverMenuInstance);
            hoverMenuInstance = null;

        }
    }
}