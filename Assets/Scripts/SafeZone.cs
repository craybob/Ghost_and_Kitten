using System;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public event Action OnCatEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CatView>(out _))
            OnCatEntered?.Invoke();
    }
}
