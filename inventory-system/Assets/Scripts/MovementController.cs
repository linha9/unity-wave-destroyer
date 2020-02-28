using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 300f;
    [SerializeField]
    private Inventory inventory;

    private float xInput;
    private bool isInvOpen;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Inventory"))
        {
            isInvOpen = !isInvOpen;
            inventory.gameObject.SetActive(isInvOpen);
        }
    }

    private void FixedUpdate()
    {
        if (!isInvOpen)
        {
            rb2d.velocity = new Vector2(xInput * speed * Time.deltaTime, rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            Item item = collider.gameObject.GetComponent<Item>();

            if (inventory.AddItem(item))
            {
                Destroy(collider.gameObject);
            }
            
        }    
    }
}
