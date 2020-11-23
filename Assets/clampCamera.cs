using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clampCamera : MonoBehaviour
{
    public Text text;

    private void Update()
    {
        Vector3 v = Camera.main.WorldToScreenPoint(this.transform.position);
        float distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        text.gameObject.SetActive(distance < 20f);
        text.transform.position = v;
    }
}
