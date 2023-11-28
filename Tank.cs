using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    public int maxHP = 100; // Maximum HP of the tank.
    public int currentHP;   // Current HP of the tank.
    public float movementSpeed = 5f; // Adjust the movement speed as needed.
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 10f; // Fuel consumption rate per second.
    private float currentFuel;
    public int speedPriority = 1;  // Speed priority of the tank.

    public HealthBar healthBar;
    public FuelBar fuelBar;

    public GameObject refuelAreaPrefab;
    private Collider2D refuelArea;
    public List<Collider2D> refuelAreas = new List<Collider2D>();

    private enum TankState
    {
        Moving,
        Shooting
    }

    private TankState currentState = TankState.Moving;
    private Vector3 targetAimPoint; // Point to aim in the moving state
    private bool canShoot = true;

    private float shootingAngle;

    //public AudioSource explosionSound; // Reference to the AudioSource for the explosion sound.

    // Initialize the tank's properties.
    void Start()
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the tank entered any refueling area.
        foreach (Collider2D refuelArea in refuelAreas)
        {
            if (other == refuelArea && refuelArea.gameObject.activeSelf)
            {
                Refuel();

                // Deactivate the refuel area.
                refuelArea.gameObject.SetActive(false);
            }
        }
    }

    // Method to reduce the tank's HP when it's hit.
    public void TakeDamage(int damage)
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

    private void DestroyTank()
    {
        // Destroy GameObject.
        Destroy(gameObject);
    }

    // Method to increase the tank's HP when a medic shoots it.
    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }


    /*private void Move(Vector3 direction)
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
        fuelBar.SetFuel(currentFuel);
    }*/

    private void Move(Vector3 direction)
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


    // Update is called once per frame.
    void Update()
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

    private void HandleMovingState()
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

            // Check if the left mouse button is pressed.
            if (Input.GetMouseButtonDown(0))
            {
                // Calculate the angle based on the mouse position and store it for shooting.
                Vector3 mouseDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(targetAimPoint)).normalized;
                shootingAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

                // Switch to shooting state.
                currentState = TankState.Shooting;
                canShoot = true; // Reset the shooting flag when entering the shooting state.

                // Print the shooting angle to the console.
                Debug.Log("Shooting Angle: " + shootingAngle);
            }
        }

        //cheat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        //cheat
        if (Input.GetKeyDown(KeyCode.R))
        {
            Refuel();
        }
    }


    private void HandleShootingState()
    {
        // Allow shooting only once in the shooting state.
        if (canShoot && Input.GetMouseButtonDown(0))
        {
            canShoot = false; // Disable shooting until the state changes again.
            Debug.Log("Shooting Angle: " + shootingAngle);
            Debug.Log("BOOM");
        }
    }
    private void Refuel()
    {
        //missing refuel after every turn reset
        currentFuel = maxFuel;
    }
}
