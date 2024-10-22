using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float size = 1;

    protected virtual void Awake()
    {
        transform.localScale = new Vector3 (size, size, size);
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    protected virtual void ChangeWeapon()
    {

    }

    protected virtual void FindTagert()
    {

    }
}
