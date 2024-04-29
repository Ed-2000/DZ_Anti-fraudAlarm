using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Signaling : MonoBehaviour
{
    public event Action RogueCameIn;
    public event Action RogueIsOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rogue>() != null)
            RogueCameIn?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rogue>() != null)
            RogueIsOut?.Invoke();
    }
}