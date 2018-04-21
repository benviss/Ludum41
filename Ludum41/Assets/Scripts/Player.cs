using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity
{

  public float moveSpeed = 5;
  public float attackSpeed = 2;

  public GameObject attackConeObject;
  private AttackCone attackCone;
  public float lastAttack;

  Camera viewCamera;
  PlayerController controller;

  protected override void Start()
  {
    base.Start();
    attackCone = attackConeObject.GetComponent<AttackCone>();
  }

  void Awake()
  {
    controller = GetComponent<PlayerController>();
    viewCamera = Camera.main;
  }

  void Update()
  {
    // Movement input
    Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
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
      if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1) {
        //gunController.Aim(point);
      }
    }

    //Attacks
    if (Input.GetMouseButton(0)) {
      if (Time.time - lastAttack > attackSpeed) {
        Attack();
      }
    }
  }

  public override void Die()
  {
    base.Die();
  }

  public void Attack()
  {
    Debug.Log("Attack");
    lastAttack = Time.time;
    List<GameObject> toAttack = attackCone.GetCollided();
    foreach (var item in toAttack) {
      item.GetComponent<LivingEntity>().TakeDamage(1);
    }
  }

}
