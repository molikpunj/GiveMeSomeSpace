using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private int moveForce;
    [SerializeField] private int rotateForce;
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
    }
}
