using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class TestScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Sprite[] s;

    [SerializeField]
    private int number;

    private int controlnumber;

    private void Start()
    {
        number = 1;
        controlnumber = 1;
     //   sr.sprite = s[Random.Range(0, s.Length - 1)];
        Debug.Log( gameObject.name);
    }

    private void Update()
    {
        if (number != controlnumber)
        {
            Debug.Log("nubmer chnged from: " + controlnumber + " to: " + number );
            controlnumber = number;
        }
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
   //     Debug.Log("Mouse is over GameObject." + gameObject.name);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject." + gameObject.name);
    }

    private void OnMouseEnter()
    {
        Debug.Log("entered." + gameObject.name);
    }

    private void OnMouseDown()
    {
        Debug.Log("clicked." + gameObject.name);
    }
}
