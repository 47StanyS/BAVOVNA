
using UnityEngine;

public class OpenBarrier : MonoBehaviour
{
    private bool _open = false;

    private Quaternion _initialRotation;
    private Quaternion _targetRotation;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _openAngle;

    public bool Open { get => _open; set => _open = value; }
    //I don't know what and how it works I copied this from the material about triggers that was provided
    //Сyrillic still didn't work

    private void Start()
    {
        _initialRotation = transform.rotation;

        _targetRotation =  Quaternion.Euler(transform.eulerAngles + new Vector3(0,0, _openAngle));
    }
    private void Update()
    {
        if (_open == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

}
