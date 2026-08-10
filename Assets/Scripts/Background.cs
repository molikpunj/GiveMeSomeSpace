using UnityEngine;

public class Background : MonoBehaviour
{
    private BoxCollider boxCollider;
    private float repetitionPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        repetitionPoint = boxCollider.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -repetitionPoint)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
    }
}
