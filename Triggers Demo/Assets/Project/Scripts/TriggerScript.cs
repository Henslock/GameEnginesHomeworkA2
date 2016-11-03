using UnityEngine;
using System.Collections;

using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    public UnityEvent[] triggers;

    void OnTriggerEnter()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].Invoke();
        }
    }
}
