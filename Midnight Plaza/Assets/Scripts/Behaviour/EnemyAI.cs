using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITakeDmg
{
    public float hp = 10;
    public float spd = 1f;
    public float loadAtk = 0.5f;
    public float minChaseRange = 1f;
    public float rangeSee = 4f;

    public Transform destination;

    private Transform player;
    private Transform fred; // point in the object to be used as a destination
    private GameObject target;
    private Rigidbody rb;

    private float rAtk = 0f;
    private bool attacking = false;
    private float friction = 1f;
    private float acel = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        fred = transform.Find("fred");
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking) {
            Attack();
        }
        else {
            Move();
            WalkingPastBuildings();
        }

        if (hp <= 0) {
            Destroy(this.gameObject);
        }
    }


    // Combat

    void Attack() {

        // WARNING: MAGIC NUMBERS WOWOWOWOWOW
        if (target == null || Vector3.Distance(transform.position, target.transform.position) > 2f) {
            rAtk = 0f;
            attacking = false;
            return;
        }

        // Loading attack
        if (rAtk >= loadAtk) {
            rAtk = 0f;

            // Hitting target
            target.TryGetComponent(out ITakeDmg victim);
            var force = 2f * (target.transform.position - transform.position).normalized;
            victim.TakeDmg(1f, force);
        }

        rAtk += Time.deltaTime;
    }

    public void TakeDmg(float dmg, Vector3 force) {
        hp -= dmg;
        rb.velocity += force;
    }


    // Movement

    void Move() {
        if (Vector3.Distance(transform.position, player.position) <= rangeSee) {
            ChasePlayer();
        }
        else {
            Wander();
        }
    }

    // deals with pesky buildings in the way
    void WalkingPastBuildings() {
        GameObject building = GetBuildingInTheWay();
        if (building != null) {
            target = building;
            attacking = true;
        }
    }

    //
    GameObject GetBuildingInTheWay() {
        // WARNING: MAGIC NUMBERS WOWOWOWOWOW
        Collider[] colls = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Buildings"));
        if (colls.Length > 0) {return colls[0].gameObject;}
        return null;
    }
    

    void ChasePlayer() {
        if (Vector3.Distance(transform.position, destination.position) > minChaseRange) {
            MoveTo(destination.position);
        }
    }

    //
    void MoveTo(Vector3 pos) {
        var dif = pos - transform.position;
        Vector3 dir = new Vector3(dif.x, 0, dif.z);

        rb.velocity += dir * acel * Time.deltaTime;

        AdjustSpd();
        FaceCorrectDir();
    }

    // makes the enemy face the moving direction
    void FaceCorrectDir() {
        transform.eulerAngles = new Vector3(
            0, 
            Vector3.Angle(new Vector3(1, 0, 0), rb.velocity.normalized),
            0
        );
    }

    // applies friction and limits speed
    void AdjustSpd() {
        Vector3 dir = rb.velocity.normalized;

        // Friction
        rb.velocity -= dir * (rb.velocity.magnitude > friction ? friction : 0) * Time.deltaTime;

        // Capping spd
        if (rb.velocity.magnitude > spd) {
            rb.velocity = dir * spd;
        }
    }

    // By Matias
    public void Wander()
    {
        Vector3 dir = fred.localPosition;
        MoveTo(transform.position + dir);
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, 
            //speed * Time.deltaTime);
        
        if(Random.value < 0.995f) {return;} 
        if(dir != Vector3.zero){
            fred.localPosition = Vector3.zero;
            return; 
        }

        Vector3 d = Random.onUnitSphere;
        fred.localPosition = new Vector3 (d.x, 0, d.z);
    }
}
