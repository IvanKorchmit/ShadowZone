using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoW;
public class Player : Character
{
    [SerializeField] private GameObject corpse;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform sight;
    [SerializeField] private Transform arm;
    [SerializeField] private SpriteRenderer weaponVisuals;
    [SerializeField] private Inventory inventory;
    private Vector2 direction;
    private float angle;
    public Animator WeaponAnimator { get; set; }
    [field: SerializeField] public Transform ShootPoint;
    public Weapon WeaponInfo => weapon;
    public Inventory Backpack => inventory;
    public static Player Singleton { get; private set; }
    private static Camera mainCamera;
    public override void Damage(float damage)
    {
        Stats.Health -= damage;
    }
    private void Awake()
    {
        Singleton = this;

    }
    protected override void Start()
    {
        base.Start();
        weaponVisuals.sprite = weapon.WeaponBase.Sprite;
        mainCamera = Camera.main;
        weaponVisuals = arm.GetComponentInChildren<SpriteRenderer>();
        WeaponAnimator = arm.GetComponentInChildren<Animator>();
        WeaponAnimator.runtimeAnimatorController = weapon.WeaponBase.Controller;
    }
    private void FixedUpdate()
    {
        Move(direction.normalized);

    }
    // Update is called once per frame
    void Update()
    {
        if (Stats.Health <= 0)
        {
            Instantiate(corpse, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }



        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));


        Vector2 mousePos = Flip();
        Vector2 aimDirection = (mousePos - (Vector2)transform.position).normalized;


        SetAngleAndRotate(aimDirection);


        weaponVisuals.flipY = Visuals.flipX;

        // Player input
        if ((Input.GetKey(KeyCode.Mouse0) && weapon.WeaponBase.Cooldown != 0f)
            || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (weapon.WeaponBase.Cooldown != 0f)
            {
                if (TimerUtils.AddTimer(weapon.WeaponBase.Cooldown, Attack, false))
                {
                    Attack();
                }
            }
            else
            {
                Attack();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            weapon.Pull(this);
            WeaponAnimator.SetTrigger("Pump");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.Insert(inventory, this);
        }

    }

    private Vector2 Flip()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Visuals.flipX = transform.position.x > mousePos.x;
        return mousePos;
    }

    private void SetAngleAndRotate(Vector2 aimDirection)
    {
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        sight.rotation = Quaternion.Euler(0, 0, -90 + angle);
        arm.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Attack()
    {
            weapon.Shoot(this, angle);

    }
    
}
