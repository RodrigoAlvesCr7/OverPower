using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTank : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D body;
    public int maxHP = 100;
    protected int currentHP;
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 10f;
    protected float currentFuel;
    public HealthBar healthBar;
    public FuelBar fuelBar;

    protected Collider2D refuelArea;
    public List<Collider2D> refuelAreas = new List<Collider2D>();

    protected enum TankState
    {
        Moving,
        // Shooting
    }

    protected TankState currentState = TankState.Moving;
    protected Vector3 targetAimPoint;
    // protected bool canShoot = true;

    // protected float shootingAngle;

    private Rigidbody2D tankBody;

    protected virtual void Start()
    {
        currentHP = maxHP;
        currentFuel = maxFuel;
        healthBar.SetMaxHealth(maxHP);
        fuelBar.SetMaxFuel(maxFuel);

        targetAimPoint = new Vector3(0, 0, 0);

        foreach (Collider2D refuelArea in refuelAreas)
        {
            if (refuelArea == null)
            {
                Debug.LogError("One or more refuel area colliders are not assigned in the Unity Editor.");
            }
            else
            {
                refuelArea.isTrigger = true; // Enable trigger mode.
            }
        }

        tankBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered: " + other.gameObject.name);

        // Check if the tank entered any refueling area.
        if (refuelAreas.Contains(other) && other.gameObject.activeSelf)
        {
            Debug.Log("Refueling!");

            Refuel();

            // Deactivate the refuel area.
            other.gameObject.SetActive(false);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHealth(currentHP);

        if (currentHP <= 0)
        {
            currentHP = 0;
            DestroyTank();
        }
    }

    protected virtual void DestroyTank()
    {
        Destroy(gameObject);
    }

    public virtual void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    protected virtual void HandleMovingState()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Refuel();
        }
    }


    public virtual void Refuel()
    {
        currentFuel = maxFuel;
    }

    protected virtual void Update()
    {
        body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, body.velocity.y);
        switch (currentState)
        {
            case TankState.Moving:
                HandleMovingState();
                break;
        }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
}