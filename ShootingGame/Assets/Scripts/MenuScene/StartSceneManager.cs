using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.IO; // input output c#�������ϴ±��
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


public class StartSceneManager : MonoBehaviour
{
    [SerializeField] Button btnStart;
    [SerializeField] Button btnRanking;
    [SerializeField] Button btnExitRanking;
    [SerializeField] Button btnExit;
    [SerializeField] GameObject viewRank;

    [Header("��ũ ������")]
    [SerializeField] GameObject fabRank;
    [SerializeField] Transform contents;



    private void Awake()
    {

        Tool.isStartMainScene = true;
        #region ��������
        // btnStart.onClick.AddListener(function);

        //btnStart.onClick.AddListener(() =>
        //{
        //    gameStart(1, 2); 
        //});

        //UnityAction<float> action = (float _value) => { };
        ////() => { }; 
        //// ���ٽ�  , �̸����� �Լ�
        //action.Invoke(); //���ٽ��� Ư�� �̺�Ʈ��  invoke�� ���ؼ� ���డ��
        #endregion
        btnStart.onClick.AddListener(gameStart);
        btnRanking.onClick.AddListener(showRanking);
        btnExitRanking.onClick.AddListener(() => { viewRank.SetActive(false); });
        btnExit.onClick.AddListener(GameExit);
        #region ��������
        //json
        //string ���ڿ� , Ű�� ���
        //{key value};

        //save��� , �������� �̵��Ҷ� ������ �����ϴ� �����Ͱ� �ִٸ�

        //�÷��̾��������� �̿��� ����Ƽ�� �����ϴ� ���
        //PlayerPrefs // ����Ƽ�� ������ �����͸� �����ϵ��� ����Ƽ ���ο� ����

        //PlayerPrefs.SetInt("test", 999); ���� ������ 1���� ���� setint setfloat
        //�����͸� �������� �ʴ� ��  test 999�� ���� ������ �����ϸ� �̵����ʹ� �����ǰ� �ҷ��� �� ����
        //���� 
        //int value = PlayerPrefs.GetInt("test");
        //Debug.Log(value); //int�� ����Ʈ 0���

        //PlayerPrefs.HasKey
        //PlayerPrefs.DeleteKey("test");

        //string path = Application.streamingAssetsPath; // os(�ü��)�� ���� �б��������� ���� 
        //My project/Assets/StreamingAssets
        //File.WriteAllText(path + "/abc.json" , "test2" );
        ////File.Delete(path + "/abc.json");
        //string result = File.ReadAllText(path + "/abc.json");
        //Debug.Log(result);

        // string path = Application.persistentDataPath + "/Jsons"; //�б�� ���Ⱑ ������ ������ġ
        ////AppData/LocalLow/DefaultCompany/My project   +  /Jsons
        ////if(Directory.Exists(path) == false) // directory = folder
        ////{
        ////    Directory.CreateDirectory(path);
        ////}
        //if(File.Exists("Test/abc.json") == true)
        //{
        //    string result = File.ReadAllText(path + "Test/abc.json");
        //}
        //else// ������ ������ �������� ����
        //{
        //    //���ο� ������ġ�� �����͸� ����� �����

        //    File.Create(path + "/Test"); //������ �������
        //}


        //cUserData cUserData = new cUserData();
        //cUserData.Name = "������";
        //cUserData.Score = 100;

        //cUserData cUserData2 = new cUserData();
        //cUserData.Name = "�󸶹�";
        //cUserData.Score = 200;

        //List<cUserData> listUserData = new List<cUserData>();
        //listUserData.Add(cUserData);
        //listUserData.Add(cUserData2);

        //string jsonData = JsonUtility.ToJson(cUserData); // jsonutility�� �ӵ��� ���� , �ϳ��� ���� ��� ����
        // { "Name":"������","Score":100}

        //cUserData user2 =  new cUserData();
        //user2 = JsonUtility.FromJson<cUserData>(jsonData);

        //string jsonData = JsonUtility.ToJson(listUserData);
        //jsonUtility�� list�� json���� �����ϸ� Ʈ������ ������

        //string jsonData = JsonConvert.SerializeObject(listUserData);
        //List<cUserData> afterData = JsonConvert.DeserializeObject<List<cUserData>>(jsonData);
        #endregion

        initRankView();
        viewRank.SetActive(false);


    }

    /// <summary>
    /// ��ũ�� ����Ǿ� �ֵ��� ����� ��ũ �����͸� �̿��ؼ� ��ũ�並 ������ְ�
    /// ��ũ�� ����Ǿ� ���� �ʴٸ� ����ִ� ��ũ�� ����� ��ũ�並 �������
    /// </summary>
    private void initRankView()
    {
        List<cUserData> listUserData = null;
        clearRankView();
        if (PlayerPrefs.HasKey(Tool.rankKey) == true) //��ũ�����Ͱ� ����Ǿ��־��ٸ�
        {
            listUserData = JsonConvert.DeserializeObject<List<cUserData>>(PlayerPrefs.GetString(Tool.rankKey));
        }
        else // ��ũ�����Ͱ� ����Ǿ����� �ʴٸ�
        {
            listUserData = new List<cUserData>();
            int rankCount = Tool.rankCount;
            for (int iNum = 0; iNum < rankCount; ++iNum)
            {
                listUserData.Add(new cUserData() { Name = " None", Score = 0 });
            }

            string value = JsonConvert.SerializeObject(listUserData);
            PlayerPrefs.SetString(Tool.rankKey, value);
        }

        int count = listUserData.Count;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            cUserData data = listUserData[iNum];

            GameObject go = Instantiate(fabRank, contents);
            FabRanking goSc = go.GetComponent<FabRanking>();
            goSc.SetData((iNum + 1).ToString(), data.Name, data.Score);
        }
    }

    /// <summary>
    /// Ȥ�ó� �������ȿ� ���� ��츦 ����ؼ� �����ϴ� ���
    /// </summary>
    private void clearRankView()
    {
        int count = contents.childCount;
        for (int iNum = count - 1; iNum > 0; --iNum)
        {
            Destroy(contents.GetChild(iNum).gameObject);
        }
    }

    private void gameStart()
    {
        FunctionFade.Instance.ActiveFade(true, () =>
        {
            SceneManager.LoadScene(1); //���ν� 0 -> 0
            FunctionFade.Instance.ActiveFade(false);
        });

    }

    private void showRanking()
    {
        viewRank.SetActive(true);
    }

    private void GameExit()
    {
        //�����Ϳ��� �÷��̸� ���� ��� , ������ ������
        //���带 ���ؼ� ������ ������ ������ �ȵ�
        //��ó�� �ڵ尡 ���ǿ� ���ؼ� ������ ���°�ó�� Ȥ�� �ִ°�ó�� �����ϰ�����

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else //����Ƽ �����Ϳ��� �������� �ʾ�����
        //���������� ���� ����
        Application.Quit();
#endif
    }


}