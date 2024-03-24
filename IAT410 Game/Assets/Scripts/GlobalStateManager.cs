using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    private static bool skunkDugHole = false;

    // Event that other scripts can subscribe to
    public static event System.Action OnSkunkDugHole;

    public static bool SkunkDugHole
    {
        get { return skunkDugHole; }
        set
        {
            if (value && !skunkDugHole)
            {
                skunkDugHole = true;
                // Trigger the event when the skunk digs a hole for the first time
                OnSkunkDugHole?.Invoke();
            }
        }
    }
}
