using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class posterMouse : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter() {
        Debug.Log("?");
        text.transform.gameObject.SetActive(true);
    }
    private void OnMouseExit() {
        text.transform.gameObject.SetActive(false);
    }
}
