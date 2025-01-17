using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public Text text;
    public string[] m_text;
    private int j;
    ParticleTrigger particleTrigger;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.transform.name == "Panel")
            m_text[2] = m_text[2].Replace("\\n", "\n");
        j = 0;
        StartCoroutine("_typing", j);
        particleTrigger = GameObject.Find("p_spot_4").GetComponent<ParticleTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(j);
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
            StopCoroutine("_typing");
            if(j == m_text.Length-1 && text.text == m_text[j])
                StartCoroutine(SkipFadeIN());
            else if(text.text == m_text[j]){
                j++;
                text.text = "";
                StartCoroutine("_typing", j);
            }
            else
                text.text = m_text[j];
        }
    }

    IEnumerator _typing(int j){
        for(int i = 0; i <= m_text[j].Length; i++){
            text.text = m_text[j].Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
        if(j == m_text.Length - 1){
            StartCoroutine(SkipFadeIN());
        }
       // yield return new WaitForSeconds(2f);
    }

    public void SkipButton(){
        StopCoroutine("_typing");
        StartCoroutine(SkipFadeIN());
    }

    IEnumerator SkipFadeIN(){
        for(int i = 10; i >= 0; i--){
            float f = i / 10.0f;
            Color c = text.color;
            c.a = f;
            text.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        //if(gameObject.name == "Panel")
        if(gameObject.name == "A_Panel"){
            StartCoroutine(FadeIn());
        }
        else{
            gameObject.SetActive(false);
        }
    }
    float fadeCount = 1.0f;
    IEnumerator FadeIn(){
        while(fadeCount > 0){ // && player.transform.rotation.z >= 0){
//            Debug.Log(fadeCount);
            fadeCount -= 0.01f;
            //player.transform.Rotate(new Vector3(0, 0, 0.9f));
            yield return new WaitForSeconds(0.01f);
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, fadeCount);
        }
        particleTrigger.check = true;
        gameObject.SetActive(false);
    }
}
