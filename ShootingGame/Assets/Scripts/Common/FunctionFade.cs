using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FunctionFade : MonoBehaviour
{
    public static FunctionFade Instance;


     Image imgFade;
    [SerializeField] float fadeTime = 1.0f;
    bool fade = false; // true가 되면 페이드 아웃 , false가 되면 페이드 인
    UnityAction action = null; // 어떤기능이 동작 완료후 실행


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad 씬을 에디티브로 생성후 거기에 넣어둠
        }
        else
        {
            Destroy(gameObject); // 예약됨
            return;
        }
        //imgFade = GetComponent<Image>(); //이러면 못찾음
        //imgFade = transform.GetChild(0).GetComponent<Image>(); //자식중 첫번째 자식에게서 컴포넌트를 찾아줌
        imgFade = GetComponentInChildren<Image>(); // 내위치로부터 자식중 이미지 컴포넌트가 있는 오브젝트를 찾아 등록해줌

        
    }
  
    void Start()
    {
        
    }


    void Update()
    {
        if(fade == true && imgFade.color.a < 1) // true가되면 페이드아웃
        {
            Color color = imgFade.color;
            color.a += Time.deltaTime / fadeTime;
            if(color.a > 1.0f)
            {
                color.a = 1.0f;

                if (action != null) //널이 아니라면 실행
                {
                    action.Invoke();
                    action = null;
                }
            }
            imgFade.color = color;
        }
        else if (fade == false && imgFade.color.a > 0)//false가되면 페이드 인
        {
            Color color = imgFade.color;
            color.a -= Time.deltaTime / fadeTime;
            if (color.a < 0.0f)
            {
                color.a = 0.0f;
            }
            imgFade.color = color;
        }

        imgFade.raycastTarget = imgFade.color.a != 0.0f; //투명화가 조금이라도 되있으면 안됨
    }

    public void ActiveFade(bool _fade , UnityAction _action = null)
    {
        fade = _fade;
        action = _action;
    }
}
