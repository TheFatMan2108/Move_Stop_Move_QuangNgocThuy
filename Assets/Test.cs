using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject[] shapes;
    public GameObject curent;
    public int index = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(curent==null)curent = Instantiate(shapes[index]);
            else curent = Instantiate(shapes[index]);
            index++;
            if(index >= shapes.Length) { index = 0; }
        }
    }
}
