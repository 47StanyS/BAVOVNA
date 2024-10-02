
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class OffCollider : MonoBehaviour
{
    [SerializeField] BoxCollider2D _gameObject;
    [SerializeField] private bool _off = false;
    public bool Off { get => _off; set => _off = value; }

    private void Start()
    {
        _gameObject = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(_off == true)
        {
            _gameObject.enabled = false;
        }
    }

}
