using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StimController : MonoBehaviour
{
    
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] BoxCollider2D _boxCollider;
    [Space]
    [Header("activeDuration")]
    [SerializeField] private float _minActiveDuration;
    [SerializeField] private float _maxActiveDuration;
    [Space]
    [Header("inactiveDuration")]
    [SerializeField] private float _minInactiveDuration;
    [SerializeField] private float _maxInactiveDuration;

    private float _currentActiveDuration;
    private float _currentInactiveDuration;

    private float _timer = 0;

    private bool _isActive = false;

   private void Awake()
   {
       _particleSystem = GetComponent<ParticleSystem>();
       _boxCollider = GetComponent<BoxCollider2D>();
   }
    void Start()
    {
       
        if (_particleSystem == null)
        {
            Debug.LogError("ParticleSystem" + gameObject.name);
        }
      
        
        _boxCollider = GetComponent<BoxCollider2D>();
        if (_boxCollider == null)
        {
            Debug.LogError("Collider2D" + gameObject.name);
        }

       
        OffTrap();
    }
    void Update()
    {
      
        _timer += Time.deltaTime;
        if (_isActive && _timer >= _currentActiveDuration)
        {
            OffTrap();
            
        }else if (!_isActive && _timer >= _currentInactiveDuration)
        {
            OnTrap();
        }


    }
   private void OnTrap()
   {
       _timer = 0;
       _isActive = true;
        _currentActiveDuration = Random.Range(_minActiveDuration, _maxActiveDuration);
       _particleSystem.Play();
       _boxCollider.enabled = true;
   }
   private void OffTrap()
   {
        _timer = 0;
       _isActive = false;
        _currentInactiveDuration = Random.Range(_minInactiveDuration, _maxInactiveDuration);
       _particleSystem.Stop();
       _boxCollider.enabled = false;
   }
}
