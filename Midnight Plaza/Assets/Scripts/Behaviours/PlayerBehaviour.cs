using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour, IHittable
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private int maxHP;
    [SerializeField] private int size;
    [SerializeField] private Vector3 direction;

    private bool freeze = false;

    private Animator anim;
    private Vector3 Direction
    {
        get{ return this.direction; }
        set{ this.direction = value; if(value != Vector3.zero) { changeFacingDirection();  } }
    }

    private Rigidbody rigb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (!freeze) rigb.velocity = Direction*speed*Time.fixedDeltaTime;
    }
    private void Update()
    {
        if (!freeze) Direction = getAxisControl();
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
    }


    public void TakeDmg(float dmg, Vector3 force, GameObject source)
    {
        hp -= (int)dmg;
        GameDataManager.Instance.onTakeDamage?.Invoke(hp, maxHP);
        if(hp <= 0){Die();}
    }

    private void Die()
    {
        GameDataManager.Instance.onPlayerDie?.Invoke();
    }

    public Vector3 GetVelocity() {
        return rigb.velocity;
    }

    public void ToggleFreeze() {
        freeze = !freeze;
    }
}
