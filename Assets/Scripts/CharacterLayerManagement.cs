using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLayerManagement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
