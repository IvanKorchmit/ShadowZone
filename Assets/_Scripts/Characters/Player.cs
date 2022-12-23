using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private AudioEvent stepSound;
    [SerializeField] private AudioEvent waterStepSound;
    public Weapon WeaponInfo => weapon;
    public Inventory Backpack => inventory;
    public static Player Singleton { get; private set; }
    public static GameObject Backup { get; private set; }
    private static Camera mainCamera;
    public override void Damage(float damage)
    {
        Stats.Health -= damage;
    }
    private void Awake()
    {
        inventory.Keys.Clear();
        mainCamera = Camera.main;
        if (Singleton == null && Backup == null)
        {
            Singleton = this;
            SceneManager.activeSceneChanged += OnSceneChanged;
            DontDestroyOnLoad(Singleton.gameObject);
        }
    }

    private void OnSceneChanged(Scene scene, Scene next)
    {
        if (Backup != null && Singleton != null)
        {
            Destroy(Backup);
            Backup = Instantiate(Singleton.gameObject);
            Backup.SetActive(false);
            DontDestroyOnLoad(Backup);
        }
    }

    public void PlayStep()
    {
        if (Stats.InitSpeed > Stats.Speed)
        {
            waterStepSound.Play(Audio);
            return;
        }
        stepSound.Play(Audio);
    }
    protected override void Start()
    {
        base.Start();

        if (Singleton == this)
        {
            if (Backup == null)
            {
                Backup = Instantiate(gameObject);
                Backup.SetActive(false);
                DontDestroyOnLoad(Backup);
            }

        }
        else if (Singleton != null)
        {
            Singleton.transform.position = transform.position;
            Destroy(gameObject);
            return;
        }
        else if (Backup != null)
        {
            Singleton = Backup.GetComponent<Player>();
            Singleton.gameObject.SetActive(true);
            Backup = null;
            if (Singleton != this)
            {
                Destroy(gameObject);
                return;
            }

        }


        weaponVisuals.sprite = weapon.WeaponBase.Sprite;
        weaponVisuals = arm.GetComponentInChildren<SpriteRenderer>();
        WeaponAnimator = arm.GetComponentInChildren<Animator>();
        WeaponAnimator.runtimeAnimatorController = weapon.WeaponBase.Controller;
        weapon = Backpack.GetWeapon(1, this);
    }
    private void FixedUpdate()
    {
        Move(direction.normalized * (Stats.Speed / Stats.InitSpeed));

    }

    private void OnGUI()
    {
        if (Event.current.isKey)
        {
            if (Event.current.keyCode >= KeyCode.Alpha0 && Event.current.keyCode <= KeyCode.Alpha9)
            {
                int index = (Event.current.keyCode - KeyCode.Alpha0 + 10) % 10;
                weapon = Backpack.GetWeapon(index, this);
                WeaponAnimator.runtimeAnimatorController = weapon.WeaponBase.Controller;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Stats.Health <= 0)
        {
            Instantiate(corpse, transform.position, Quaternion.identity);
            SceneManager.activeSceneChanged -= OnSceneChanged;
            Destroy(gameObject);
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            var colls = Physics2D.OverlapCircleAll(transform.position, 1);
            foreach (var item in colls)
            {
                if (item.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
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
