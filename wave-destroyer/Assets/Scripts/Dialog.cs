using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            CheckDisable();
        }
    }

    private void CheckDisable()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            gameObject.SetActive(false);
            EventHandler.Instance.Invoke(EventType.DialogOff);
        }
    }
}
