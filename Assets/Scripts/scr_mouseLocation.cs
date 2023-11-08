using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_mouseLocation : MonoBehaviour
{
    public TMP_Text mouseTextObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Debug.Log(mousePos.x);
        Debug.Log(mousePos.y);

        mouseTextObj.text = mousePos.x.ToString("F2") + ":" + mousePos.y.ToString("F2");
    }
}
