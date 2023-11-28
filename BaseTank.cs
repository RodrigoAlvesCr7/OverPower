using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTank : MonoBehaviour
{
    public int maxHP = 100; // Maximum HP of the tank.
    protected int currentHP; // Current HP of the tank.
    public float movementSpeed = 5f; // Adjust the movement speed as needed.
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 10f; // Fuel consumption rate per second.
    protected float currentFuel;
    public HealthBar healthBar;
    public FuelBar fuelBar;

    protected Collider2D refuelArea;
    public List<Collider2D> refuelAreas = new List<Collider2D>();

    protected enum TankState
    {
        Moving,
        Shooting
    }

    protected TankState currentState = TankState.Moving;
    protected Vector3 targetAimPoint; // Point to aim in the moving state
    protected bool canShoot = true;

    protected float shootingAngle;

    // Initialize the tank's properties.
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
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered: " + other.gameObject.name);

        // Check if the tank entered any refueling area.
        foreach (Collider2D refuelArea in refuelAreas)
        {
            Debug.Log("Refuel area: " + refuelArea.gameObject.name);

            if (other == refuelArea && refuelArea.gameObject.activeSelf)
            {
                Debug.Log("Refueling!");

                Refuel();

                // Deactivate the refuel area.
                refuelArea.gameObject.SetActive(false);
            }
        }
    }



    // Method to reduce the tank's HP when it's hit.
    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHealth(currentHP);

        if (currentHP <= 0)
        {
            currentHP = 0;

            // Play explosion sound.
            /*if (explosionSound != null)
            {
                explosionSound.Play();
            }*/

            // Call Destroy GameObject.
            DestroyTank();
        }
    }

    protected virtual void DestroyTank()
    {
        // Destroy GameObject.
        Destroy(gameObject);
    }

    // Method to increase the tank's HP when a medic shoots it.
    public virtual void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public virtual void Move(Vector3 direction)
    {
        // Calculate the distance to move based on the frame time and speed.
        float distance = movementSpeed * Time.deltaTime;

        // Reduce fuel based on the fuel consumption rate.
        float fuelConsumed = fuelConsumptionRate * Time.deltaTime;
        currentFuel -= fuelConsumed;

        if (currentFuel < 0)
        {
            currentFuel = 0;
        }

        // Move the tank in the specified direction.
        transform.Translate(direction * distance);

        // Set the rotation to zero to avoid unwanted rotation.
        transform.rotation = Quaternion.Euler(0, 0, 0);

        fuelBar.SetFuel(currentFuel);
    }

    protected virtual void HandleMovingState()
    {
        if (currentFuel > 0)
        {
            // Move right when the 'D' key is pressed.
            if (Input.GetKey(KeyCode.D))
            {
                Move(Vector3.right);
            }

            // Move left when the 'A' key is pressed.
            if (Input.GetKey(KeyCode.A))
            {
                Move(Vector3.left);
            }
        }

            // Check if the left mouse button is pressed.
            if (Input.GetMouseButtonDown(0))
            {
                // Calculate the angle based on the mouse position and store it for shooting.
                Vector3 mouseDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(targetAimPoint)).normalized;
                shootingAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

                // Switch to shooting state.
                currentState = TankState.Shooting;
                canShoot = true;

                Debug.Log("Shooting Angle: " + shootingAngle);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Refuel();
            }
    }

    // Method to handle the tank's shooting state.
    protected abstract void HandleShootingState();

    // Method to refuel the tank.
    public virtual void Refuel()
    {
        // Missing refuel after every turn reset.
        currentFuel = maxFuel;
    }

    // Update is called once per frame.
    protected virtual void Update()
    {
        switch (currentState)
        {
            case TankState.Moving:
                HandleMovingState();
                break;
            case TankState.Shooting:
                HandleShootingState();
                break;
        }
    }
}
