using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private BoxCollider2D bc;
    private LayerMask tLayer;
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        this.tLayer = LayerMask.NameToLayer("Ground");
        this.bc = this.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Color rayColor;

        if (collision.CompareTag("Ground"))
        {
            rayColor = Color.red;
            IsGrounded = true;
        } else
        {
            rayColor = Color.green;
        }



        /*Debug.DrawLine(new Vector2(bc.bounds.min.x, bc.bounds.max.y), bc.bounds.min, rayColor);
        Debug.DrawLine(bc.bounds.min, new Vector2(bc.bounds.max.x, bc.bounds.min.y), rayColor);
        Debug.DrawLine(bc.bounds.max, new Vector2(bc.bounds.max.x, bc.bounds.min.y), rayColor);*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.IsGrounded = false;
    }

    /*[SerializeField]
    private LayerMask groundLayer;

    public bool IsGrounded { get; set; }
    private BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void CheckGround()
    {
        ContactFilter2D c = new ContactFilter2D();
        List<Collider2D> ovColliders = new List<Collider2D>();
        col.OverlapCollider(c, ovColliders);
        IsGrounded = false;

        ovColliders.ForEach(collider => {
            Debug.Log(collider.gameObject.layer);
            Debug.Log(groundLayer.value );
            if (collider.gameObject.layer == groundLayer.value)
            {
                IsGrounded = true;
                return;
            }
        });
    }*/

    /*private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            //Debug.Log("Checagem de is grounded = true");
            IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        IsGrounded = false;
        //Debug.Log("Checagem de is grounded = false");
    }*/
}
