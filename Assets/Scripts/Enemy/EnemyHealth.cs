using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public EnemyBrain enemyBrain;

    [SerializeField] GameObject _healtBarPrefab;
    [SerializeField] GameObject _healthBarPoints;
    HealthBarManager _healthBarManagerComponent;

    [Space]
    [Header("Health")]

    public float _enemyCurrentHealth;
    public float _enemyMaxHealth;

    private void Awake()
    {
        CreateHealtBar();
    }
    private void CreateHealtBar()
    {
        GameObject _healthBarObject = Instantiate(_healtBarPrefab, _healthBarPoints.transform.position,Quaternion.identity,this.transform);
        _healthBarManagerComponent = _healthBarObject.GetComponent<HealthBarManager>();

        _healthBarManagerComponent.SetHealth(_enemyCurrentHealth,_enemyMaxHealth);
    }

    public void UpdateHealth(float _number)
    {
        _enemyCurrentHealth = _enemyCurrentHealth - _number / 2;

        if(_enemyCurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        _healthBarManagerComponent.SetHealth(_enemyCurrentHealth, _enemyMaxHealth);
    }
}
