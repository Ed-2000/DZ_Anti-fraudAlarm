using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Signaling : MonoBehaviour
{
    public event Action RogueCameIn;
    public event Action RogueIsOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rogue>(out Rogue rogue))
            RogueCameIn?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Rogue>(out Rogue rogue))
            RogueIsOut?.Invoke();
    }
}