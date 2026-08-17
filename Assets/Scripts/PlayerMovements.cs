using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private int moveForce;
    [SerializeField] private int rotateForce;
    [SerializeField] private GameObject energyRay;
    private bool isFiringAllowed = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = playerInput.Player.Move.ReadValue<float>();
        transform.Translate(moveDirection * Time.deltaTime * moveForce, 0, 0, Space.World);
        transform.Rotate(0, 0, -moveDirection * Time.deltaTime * rotateForce);
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -3.8f, 3.8f);
        transform.position = position;

        if (playerInput.Player.Shoot.IsPressed() && isFiringAllowed)
        {
            Instantiate(energyRay, transform.position, Quaternion.identity);
            StartCoroutine(ShootingDelay());
        }
    }

    IEnumerator ShootingDelay()
    {
        isFiringAllowed = false;
        yield return new WaitForSeconds(0.2f);
        isFiringAllowed = true;

    }
}
