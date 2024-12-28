using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DorController : MonoBehaviour
{
    private bool _isOpen = false;

    private Quaternion _initialRotation;
    private Quaternion _targetRotation;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _openAngle;

    public bool Open { get => _isOpen; set => _isOpen = value; }
    //I don't know what and how it works I copied this from the material about triggers that was provided
    //Сyrillic still didn't work

    private void Start()
    {
        _initialRotation = transform.rotation;

        _targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, _openAngle));
    }
    private void Update()
    {
        if (_isOpen == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
