
using UnityEngine;

public class BridgesDestroy : MonoBehaviour
{
    [SerializeField] private bool _destoy = false;
    public bool OnDestroy { get => _destoy; set => _destoy = value; }

    private Rigidbody2D _body;
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(_destoy == true)
        {
            Destroy(gameObject);
        }
    }
}
