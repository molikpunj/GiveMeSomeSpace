using System.Collections;
using UnityEngine;

public class RemoveEmptyEffects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(autoDelete());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator autoDelete()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
