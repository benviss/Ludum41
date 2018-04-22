﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity
{
    public float baseMoveSpeed = 7;
    public float moveSpeed = 7;
    public float baseAttackRange = 1;
    public float attackRange = 1;
    public float attackSpeed = .75f;
    public float size = 1;
    Rigidbody rb;

    public GameObject attackConeObject;
    private AttackCone attackCone;
    public Transform[] arms;
    public float lastAttack;

    CameraFollow cameraFollow;
    Camera viewCamera;
    PlayerController controller;


    protected override void Start()
    {
        base.Start();
        attackCone = attackConeObject.GetComponent<AttackCone>();
        rb = GetComponent<Rigidbody>();
        rb.mass = 7f;
    }


    void Awake()
  {
    controller = GetComponent<PlayerController>();
    viewCamera = Camera.main;
    cameraFollow = viewCamera.GetComponent<CameraFollow>();
  }

  void Update()
  {
    // Movement inputd
    Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    float cameraRotate = 0;
    if (Input.GetKey(KeyCode.Q)) {
      cameraRotate = -1;
    }
    else if (Input.GetKey(KeyCode.E)) {
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
    Vector3 moveVelocity = moveInput.normalized * moveSpeed;
    controller.Move(moveVelocity);

    // Look input
    Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
    Plane groundPlane = new Plane(Vector3.up, Vector3.up/* * gunController.GunHeight*/);
    float rayDistance;

    if (groundPlane.Raycast(ray, out rayDistance)) {
      Vector3 point = ray.GetPoint(rayDistance);
      Debug.DrawLine(ray.origin,point,Color.red);
      controller.LookAt(point);
    }

    //Attacks
    if (Input.GetMouseButton(0)) {
      if (Time.time - lastAttack > attackSpeed) {
        Attack();
      }
    }
        if (health < rb.mass * .8f)
        {
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
        StartCoroutine(AnimateAttack());
        //AudioManager.instance.PlaySound(attackAudio, transform.position);


        lastAttack = Time.time;
        List<GameObject> toAttack = attackCone.GetCollided();
        foreach (var item in toAttack)
        {
            LivingEntity entity = item.GetComponent<LivingEntity>();
            entity.OnDeath += AddMass;
            entity.TakeDamage(10);
        }
    }

    void AddMass()
    {
        health++;
        if (health > .9f * rb.mass)
        {
            rb.mass++;
            CalcShizzle();
        }
    }

    void CalcShizzle()
    {
        size = Mathf.Pow(rb.mass, .5f) * .4f;
        moveSpeed = baseMoveSpeed * Mathf.Pow(size, .5f);
        attackRange = baseAttackRange * Mathf.Pow(size, .5f);
        transform.localScale = Vector3.one * size;
    }

    IEnumerator AnimateAttack()
    {
        yield return new WaitForSeconds(.2f);

        float reloadSpeed = 1f / attackSpeed;
        float percent = 0;
        Vector3 initialRot = Vector3.zero;
        float maxAttackAngle = -40;

        while (percent < 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float reloadAngle = Mathf.Lerp(0, maxAttackAngle, interpolation);

            foreach (Transform t in arms)
            {
                t.localEulerAngles = initialRot + Vector3.left * reloadAngle;
            }

            yield return null;
        }
    }
}
