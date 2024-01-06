using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITakeDmg
{
    public float hp = 10;
    public float spd = 1f;
    public float loadAtk = 30f;
    public float minChaseRange = 1f;

    public Transform destination;

    private GameObject target;
    private Rigidbody rb;

    private float rAtk = 0f;
    private bool attacking = false;
    private float friction = 0.1f;
    private float acel = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination.position) > minChaseRange) {
            MoveTo(destination.position);
        }

        if (attacking) {
            Attack();
        }

        if (hp <= 0) {
            Destroy(this.gameObject);
        }
    }


    // Combat

    void Attack() {

        if (rAtk >= loadAtk) {
            
            TryGetComponent(out ITakeDmg victim);
            victim.TakeDmg(1f, Vector3.zero);
            rAtk = 0;
        }

        rAtk += Time.deltaTime;
    }

    public void TakeDmg(float dmg, Vector3 force) {
        hp -= dmg;
        rb.velocity += force;
    }


    // Movement

    void MoveTo(Vector3 pos) {
        var dif = pos - transform.position;
        Vector3 dir = new Vector3(dif.x, 0, dif.z);

        rb.velocity += dir * acel;

        AdjustSpd();
        FaceCorrectDir();
    }

    void FaceCorrectDir() {
        transform.eulerAngles = new Vector3(0, 
        Vector3.Angle(new Vector3(1, 0, 0), rb.velocity.normalized),
        0);
    }

    void AdjustSpd() {
        Vector3 dir = rb.velocity.normalized;

        // Friction
        rb.velocity -= dir * (rb.velocity.magnitude > friction ? friction : 0);

        // Capping spd
        if (rb.velocity.magnitude > spd) {
            rb.velocity = dir * spd;
        }
    }
}
