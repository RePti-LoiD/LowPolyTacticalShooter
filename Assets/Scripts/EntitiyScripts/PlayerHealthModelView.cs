using UnityEngine;

public class PlayerHealthModelView : MonoBehaviour, IDamagable
{
    [SerializeField] private Health health;
    [SerializeField] private HealthEvent healthEvent;

    private GameObject lastSender;

    private void Start()
    {
        OnNotifyChanges(health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetDamage(gameObject, 20);
        }
    }

    private void OnEnable()
    {
        health.NotifyChanges += OnNotifyChanges;
    }

    private void OnDisable()
    {
        health.NotifyChanges -= OnNotifyChanges;
    }

    private void OnNotifyChanges(Health health)
    {
        healthEvent?.Invoke(new HealthEventArgs
        {
            EventSender = lastSender,
            CurrentHealth = health.CurrentHealth,
            PercentOfHealth = health.CurrentHealth / (float)health.MaxHealth
        });
    }

    public void SetDamage(GameObject sender, int damage)
    {
        health.SetDamage(damage);
    }
}