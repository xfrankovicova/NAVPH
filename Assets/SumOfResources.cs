using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SumOfResources : MonoBehaviour
{
    [SerializeField] private Image resourceImage;
    [SerializeField] private TMP_Text sum;

    // Start is called before the first frame update
    void Start()
    {
        Initialize("10");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize(string sum)
    {
        this.sum.text = sum;
    }
}
