using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoviment : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private int size;

    private Vector3 direction;
    private Vector3 Direction
    {
        get{ return this.direction; }
        set{ this.direction = value; if(value != Vector3.zero) { changeFacingDirection();  } }
    }

    private Rigidbody rigb;
    void Start()
    {
        rigb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rigb.velocity = Direction*speed*Time.fixedDeltaTime;
    }
    private void Update()
    {
        Direction = getAxisControl();
    }

    private Vector3 getAxisControl()
    {
        return (new Vector3(Input.GetAxisRaw("Horizontal"), 0 ,Input.GetAxisRaw("Vertical"))).normalized;
    }

    private void changeFacingDirection()
    {
        Quaternion lookDir = new Quaternion();
        lookDir.SetLookRotation(this.direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDir, 10f);
        // = lookDir;
    }
}
