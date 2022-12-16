using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float risingRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RaiseShredder());
    }

    private IEnumerator RaiseShredder()
    {
        
        transform.position = new Vector3(transform.position.x, transform.position.y + risingRate, transform.position.z);
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(RaiseShredder());
    }
}
