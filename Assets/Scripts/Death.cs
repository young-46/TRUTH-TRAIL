using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator animator;
    private float time;
    private void Start() {
        //animator = GetComponent<animator>();
    }
    private void Update() {
        time += Time.deltaTime;
        if(time > 2){
            LoadingScene.Instance.LoadScene("Death");
        }
    }
}
