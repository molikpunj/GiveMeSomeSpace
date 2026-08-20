using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private int moveForce;
    [SerializeField] private int rotateForce;
    [SerializeField] private GameObject energyRay;
    [SerializeField] private ParticleSystem shootingLight;
    public GameManager GameManager;
    private bool isFiringAllowed = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = GameObject.FindAnyObjectByType<GameManager>();
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
            Instantiate(energyRay, new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), Quaternion.identity);
            StartCoroutine(ShootingDelay());
            shootingLight.Play();
            StartCoroutine(Recoil());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            GameManager.gameOver = true;
            Instantiate(GameManager.explosion, transform.position, Quaternion.identity);
            Instantiate(GameManager.explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator ShootingDelay()
    {
        isFiringAllowed = false;
        yield return new WaitForSeconds(0.2f);
        isFiringAllowed = true;
    }

    IEnumerator Recoil()
    {
        for(int i = 1; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.005f, transform.position.z);
            yield return new WaitForSeconds(0.005f);
        }
        for (int i = 1; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.005f, transform.position.z);
            yield return new WaitForSeconds(0.005f);
        }
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
}