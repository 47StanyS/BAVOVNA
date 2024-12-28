using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerOpenDoor : MonoBehaviour
{
    public DorController _dorController;
    
    public EnemyHealth _enemyHealth;

    void Update()
    {
        OpenDoor();
    }
    private void OpenDoor()
    {
        if (this.gameObject == null) 
        {
            _dorController.Open = true;
        }
    }

}
