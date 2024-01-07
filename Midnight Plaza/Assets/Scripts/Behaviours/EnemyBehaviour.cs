using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IHittable
{
    public float hp = 10;
    public float speed = 1f;
    public float loadAtk = 0.5f;
    public float minChaseRange = 1f;
    public float rangeSee = 4f;

    [SerializeField] public Enemy enemyData;

    private Transform player;
    private Transform fred; // point in the object to be used as a destination
    private GameObject target;
    private Rigidbody rb;

    private float atk = 1f;
    private float rAtk = 0f;
    private bool attacking = false;
    private float friction = 10f;
    private float acel = 20f;

    public Enemy EnemyData{ 
        get { return enemyData; } 
        set{
            this.enemyData = value;
            this.speed = enemyData.speed;
            this.atk = enemyData.atk;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        this.EnemyData = this.enemyData; 
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
            target.TryGetComponent(out IHittable victim);
            var force = 2f * (target.transform.position - transform.position).normalized;
            victim.TakeDmg(atk, force, this.gameObject);
        }

        rAtk += Time.deltaTime;
    }

    public void TakeDmg(float dmg, Vector3 force, GameObject source) {
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
        if (Vector3.Distance(transform.position, player.position) > minChaseRange) {
            MoveTo(player.position);
        }
        
        var dir = player.position - transform.position;
        fred.localPosition = new Vector3 (dir.x, 0, dir.z);
    }

    //
    void MoveTo(Vector3 pos) {
        var dif = pos - transform.position;
        Vector3 dir = (new Vector3(dif.x, 0, dif.z)).normalized;

        rb.velocity += dir * acel * Time.deltaTime;

        AdjustSpeed();
        Turn();
    }

    // makes the enemy face the moving direction
    void Turn() {
        Vector3 v = Quaternion.Euler(0, -90, 0) * (new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized);
        transform.LookAt(transform.position + v);
    }

    // applies friction and limits speed
    void AdjustSpeed() {
        Vector3 dir = rb.velocity.normalized;

        // Friction
        rb.velocity -= dir * (rb.velocity.magnitude > friction ? friction : 0) * Time.deltaTime;

        // Capping Speed
        if (rb.velocity.magnitude > speed) {
            rb.velocity = dir * speed;
        }
    }

    // By Matias
    public void Wander()
    {
        Vector3 dir = fred.localPosition;
        MoveTo(transform.position + dir);
        
        if(Random.value < 0.995f) {return;} 
        if(dir != Vector3.zero){
            fred.localPosition = Vector3.zero;
            return; 
        }

        Vector3 d = Random.onUnitSphere;
        fred.localPosition = new Vector3 (d.x, 0, d.z);
    }
}
