using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Curses : MonoBehaviour
{
    public bool activeCurse = false;
    public int curseKey = 0;
    public bool activeTimer = false;
    private Coroutine curseTimerCoroutine;
    public bool die = false;
    //단서 습득 시 저주일 때
    //curse.json파일에서 랜덤으로 저주 읽어오기
    private List<Curse> readCurse;
    private int CurseCount = 0;
    public int doorCurseCount = 0;
    [SerializeField] GameObject player;
    [SerializeField] Text curseText;
    private PlayerCrash playerCrash;
    private ChangeCamera changeCamera;
    private bool ending = false;
    public int countTimer;

    private void Start()
    {
        playerCrash = player.GetComponent<PlayerCrash>();
        changeCamera = player.transform.GetChild(1).GetComponent<ChangeCamera>();
    }
    public string ActiveCurse()
    {
        int RandomCurse;
        string retString;
        activeCurse = true;
        readCurse = MemoReader.ReadCurse();
        CurseCount = readCurse.Count;
        RandomCurse = Random.Range(0, CurseCount);
        //RandomCurse = 19; //Test용 json파일에 들어간 순서다.
        retString = readCurse[RandomCurse].GetCurseData();
        curseKey = readCurse[RandomCurse].GetKey();
        readCurse.RemoveAt(RandomCurse);
        return retString;
    }

    public void ClearCurse()
    {
        activeCurse = false;
        curseKey = -1;
        curseText.gameObject.SetActive(false);
        if(activeTimer){
            StopCoroutine(curseTimerCoroutine);
            activeTimer = false;
        }
    }
    void Update()
    {
        if(activeCurse)
        {
            if(curseKey>=0 && curseKey <= 9)
                BasicCurse();
            else if(curseKey==10)
                TimeCurse(120.0f);
            else if(curseKey==11)
                TimeCurse(210.0f);
            else if (curseKey==12)
                TimeCurse(60.0f);
            else if( curseKey==13)
                TimeCurse(300.0f);
            else if(curseKey==14)
                TimeCurse(60.0f);
            else if(curseKey==15)
                TimeCurse(150.0f);
            else if(curseKey==16)
                TimeCurse(180.0f);
            else if(curseKey==17)
                TimeCurse(180.0f);
            else if(curseKey==18)
                TimeCurse(120.0f);
            else if(curseKey==19)
                TimeCurse(120.0f);
        }
        /*if(!ending&&die){
            changeCamera.SwitchToVirtualCamera();
            ending=true;
        }*/
    }

    //die변수를 추가해서 각 저주마다 함수를 만드는 것이 아니라 기본, 타임 등으로만 나누어 함수 만드는 것이 좋겠다.
    //이 부분은 코드 정리하면서 수정해야겠다.
    //수정이 된다면 switch case로 일일히 지정할 필요 없이 if문으로 간단하게 나타낼 수 있다.
    //또한 기본 저주의 경우 die인 경우 사망, time 저주인 경우 변수를 받아 코루틴 실행 후 die인 경우 사망으로 두면 되겠다.
    //생각해보니 별도의 함수도 필요 없겠다. 그냥 if문으로 간단하게 할 수 있을 것 같다.

    private void BasicCurse()
    {
        if (die)
        {
            Debug.Log("사망2");
            GameObject.Find("Alley_close").GetComponent<AlleyNav>().DeadCurse();
            changeCamera.SwitchToVirtualCamera();
            ending=true;
            ClearCurse();
            StartCoroutine(Death());
        }
    }
    private void TimeCurse(float timer)
    {
        if (!activeTimer)
        {
            curseTimerCoroutine = StartCoroutine(CurseTimer(timer));
        }

        if (die)
        {
            Debug.Log("사망3");
            GameObject.Find("Alley_close").GetComponent<AlleyNav>().DeadCurse();
            ending=true;
            changeCamera.SwitchToVirtualCamera();
            ClearTimer();
           StartCoroutine(Death());

        }
    }



    IEnumerator CurseTimer(float curseTime)
    {
        activeTimer = true;
        float curTime = curseTime;
        while (curTime > 0)
        {
            curTime -= 1;
            countTimer = (int)curTime;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("사망4");
        GameObject.Find("Alley_close").GetComponent<AlleyNav>().DeadCurse();
        ending=true;
        changeCamera.SwitchToVirtualCamera();
        StartCoroutine(Death());
    }
    private void ClearTimer()
    {
        if (activeTimer)
        {
            StopCoroutine(curseTimerCoroutine);
            ClearCurse();
            activeTimer = false;
        }
    }
    IEnumerator Death(){
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        LoadingScene.Instance.LoadScene("Death");
    }
}
