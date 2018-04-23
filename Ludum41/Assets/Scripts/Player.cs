using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity
{
    public float TargetMass = 100;
    public float baseMoveSpeed = 7;
    public float moveSpeed = 7;
    public float baseAttackRange = 1;
    public float attackRange = 1;
    public float attackSpeed = .5f;
    public float size = 1;
    Rigidbody rb;

    public GameObject attackConeObject;
    private AttackCone attackCone;
    public GameObject[] arms;
    public GameObject[] legs;
    public float lastAttack;

    CameraFollow cameraFollow;
    Camera viewCamera;
    PlayerController controller;
    public HealthBar healthBar;

    protected override void Start()
    {
        base.Start();
        attackCone = attackConeObject.GetComponent<AttackCone>();
        rb = GetComponent<Rigidbody>();
        rb.mass = 7f;
        OnHit += GameManager.Instance.UpdateUI;
        GameManager.Instance.UpdateUI();
    }


    void Awake()
    {
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
        cameraFollow = viewCamera.GetComponent<CameraFollow>();
    }

    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        AnimateLegs(moveInput);

        //Camera Tracking Input
        float cameraRotate = 0;
        if (Input.GetKey(KeyCode.Q)) {
            cameraRotate = -1;
        } else if (Input.GetKey(KeyCode.E)) {
            cameraRotate = 1;
        } else {
            cameraRotate = 0;
        }
        if (cameraRotate != 0) {
            cameraFollow.Rotate(cameraRotate);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) {
            cameraFollow.Scroll(scroll);
        }

        // Look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up/* * gunController.GunHeight*/);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance)) {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }

        //Attacks
        if (Input.GetMouseButton(0)) {
            if (Time.time - lastAttack > attackSpeed) {
                Attack();
            }
        }
        if (health < rb.mass * .8f) {
            rb.mass -= size;
            CalcShizzle();
        }
    }

    public override void Die()
    {
        base.Die();
    }

    public void Attack()
    {
        //FindObjectOfType<NewAudioManager>().Play("monstercri");
        NewAudioManager.instance.Play("monstercri");
        
        //StartCoroutine(AnimateAttack());
        //AudioManager.instance.PlaySound(attackAudio, transform.position);
        foreach (var item in arms) {
            item.GetComponent<Animation>().Play();
        }
        lastAttack = Time.time;
        List<GameObject> toAttack = attackCone.GetCollided();
        foreach (var item in toAttack) {
            IDamageable damageableObject = item.GetComponent<IDamageable>();
            if (damageableObject != null) {
                item.GetComponent<LivingEntity>().OnDeath += AddMass;
                damageableObject.TakeHit(8*size, item.GetComponent<LivingEntity>().transform.position, transform.forward);
            }
        }
    }

    void AddMass()
    {
        health += 3;
        if (health > .9f * rb.mass) {
            rb.mass += 3;
            CalcShizzle();
            GameManager.Instance.UpdateUI();
        }
    }

    void CalcShizzle()
    {
        size = Mathf.Pow(rb.mass, .5f) * .4f;
        moveSpeed = baseMoveSpeed * Mathf.Pow(size, .5f);
        attackRange = baseAttackRange * Mathf.Pow(size, .5f);
        transform.localScale = Vector3.one * size;
    }

    void AnimateLegs(Vector3 _moveInput)
    {
        bool animOff = _moveInput != Vector3.zero;
        foreach (var item in legs) {
            item.GetComponent<Animator>().enabled = animOff;
        }
    }

    //IEnumerator AnimateAttack()
    //{
    //    //yield return new WaitForSeconds(.2f);

    //    //float reloadSpeed = 1f / attackSpeed;
    //    //float percent = 0;
    //    //Vector3 initialRot = Vector3.zero;
    //    //float maxAttackAngle = -40;

    //    //while (percent < 1) {
    //    //    percent += Time.deltaTime * reloadSpeed;
    //    //    float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
    //    //    float reloadAngle = Mathf.Lerp(0, maxAttackAngle, interpolation);

    //    //    foreach (Transform t in arms) {
    //    //        t.localEulerAngles = initialRot + Vector3.left * reloadAngle;
    //    //    }

    //    //    yield return null;
    //    //}
    //}
}
