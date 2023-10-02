using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{

    //events
    public UnityEvent OnDeath;

    [Tooltip("Callback parameters, current health")]
    public UnityEvent<float, float, float> OnHealthChanged;

    [Tooltip("Absolute value represnting total hit point pool.")]
    [SerializeField] private float maxHealth = 100.0f;

    [Tooltip("Aboslute value of hit points when behaviour is spawned.")]
    [SerializeField] private float initialHealth = 100.0f;

    //return percentage for user interface
    private float _healthPercentage;
    public float HealthPercentage
    {
        get { return _healthPercentage; }
    }

    //Health
    private float _currentHealth;
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            _healthPercentage = _currentHealth / maxHealth;



            if (_currentHealth < 0.0f)
            {
                OnDeath?.Invoke();
            }



        }
    }

    private void Awake()
    {
        CurrentHealth = initialHealth;
    }
}
