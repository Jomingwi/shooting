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
    bool fade = false; // true�� �Ǹ� ���̵� �ƿ� , false�� �Ǹ� ���̵� ��
    UnityAction action = null; // ������ ���� �Ϸ��� ����


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad ���� ����Ƽ��� ������ �ű⿡ �־��
        }
        else
        {
            Destroy(gameObject); // �����
            return;
        }
        //imgFade = GetComponent<Image>(); //�̷��� ��ã��
        //imgFade = transform.GetChild(0).GetComponent<Image>(); //�ڽ��� ù��° �ڽĿ��Լ� ������Ʈ�� ã����
        imgFade = GetComponentInChildren<Image>(); // ����ġ�κ��� �ڽ��� �̹��� ������Ʈ�� �ִ� ������Ʈ�� ã�� �������

        
    }
  
    void Start()
    {
        
    }


    void Update()
    {
        if(fade == true && imgFade.color.a < 1) // true���Ǹ� ���̵�ƿ�
        {
            Color color = imgFade.color;
            color.a += Time.deltaTime / fadeTime;
            if(color.a > 1.0f)
            {
                color.a = 1.0f;

                if (action != null) //���� �ƴ϶�� ����
                {
                    action.Invoke();
                    action = null;
                }
            }
            imgFade.color = color;
        }
        else if (fade == false && imgFade.color.a > 0)//false���Ǹ� ���̵� ��
        {
            Color color = imgFade.color;
            color.a -= Time.deltaTime / fadeTime;
            if (color.a < 0.0f)
            {
                color.a = 0.0f;
            }
            imgFade.color = color;
        }

        imgFade.raycastTarget = imgFade.color.a != 0.0f; //����ȭ�� �����̶� �������� �ȵ�
    }

    public void ActiveFade(bool _fade , UnityAction _action = null)
    {
        fade = _fade;
        action = _action;
    }
}
