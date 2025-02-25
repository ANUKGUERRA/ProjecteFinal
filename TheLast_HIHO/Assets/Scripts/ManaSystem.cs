using UnityEngine;
using System;

public class ManaSystem : MonoBehaviour
{
    public event Action<float> OnManaChanged;
    public event Action OnManaDepleted;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float regenRate = 5f;

    private float currentMana;

    private void Awake()
    {
        currentMana = maxMana;
        InvokeRepeating(nameof(RegenerateMana), 1f, 1f);
    }

    public bool TryConsumeMana(float amount)
    {
        if (currentMana < amount) return false;

        currentMana -= amount;
        OnManaChanged?.Invoke(currentMana / maxMana);

        return true;
    }

    public void TakeDamage(float amount)
    {
        currentMana -= amount;

        if (currentMana <= 0)
        {
            currentMana = 0;
            OnManaDepleted?.Invoke();
        }

        OnManaChanged?.Invoke(currentMana / maxMana);
    }

    public void ResetMana()
    {
        currentMana = maxMana;
        OnManaChanged?.Invoke(currentMana / maxMana);
    }

    public bool TryConsumeManaOverTime(float baseDrainRate, float exponentialFactor, float elapsedTime)
    {
        float drainAmount = baseDrainRate * Mathf.Pow(1 + exponentialFactor, elapsedTime);
        return TryConsumeMana(drainAmount * Time.deltaTime);
    }

    private void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana = Mathf.Min(currentMana + regenRate, maxMana);
            OnManaChanged?.Invoke(currentMana / maxMana);
        }
    }

    public float GetManaRatio() => currentMana / maxMana;
}
