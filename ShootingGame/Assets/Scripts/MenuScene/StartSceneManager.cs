using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.IO; // input output c#이제공하는기능
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

    [Header("랭크 프리팹")]
    [SerializeField] GameObject fabRank;
    [SerializeField] Transform contents;



    private void Awake()
    {

        Tool.isStartMainScene = true;
        #region 수업내용
        // btnStart.onClick.AddListener(function);

        //btnStart.onClick.AddListener(() =>
        //{
        //    gameStart(1, 2); 
        //});

        //UnityAction<float> action = (float _value) => { };
        ////() => { }; 
        //// 람다식  , 이름없는 함수
        //action.Invoke(); //람다식은 특정 이벤트나  invoke를 통해서 실행가능
        #endregion
        btnStart.onClick.AddListener(gameStart);
        btnRanking.onClick.AddListener(showRanking);
        btnExitRanking.onClick.AddListener(() => { viewRank.SetActive(false); });
        btnExit.onClick.AddListener(GameExit);
        #region 수업내용
        //json
        //string 문자열 , 키와 밸류
        //{key value};

        //save기능 , 씬과씬을 이동할때 가지고 가야하는 데이터가 있다면

        //플레이어프렙스를 이용해 유니티에 저장하는 방법
        //PlayerPrefs // 유니티가 꺼져도 데이터를 보관하도록 유니티 내부에 저장

        //PlayerPrefs.SetInt("test", 999); 숫자 데이터 1개만 저장 setint setfloat
        //데이터를 삭제하지 않는 한  test 999가 저장 게임을 삭제하면 이데이터는 삭제되고 불러올 수 없음
        //스팀 
        //int value = PlayerPrefs.GetInt("test");
        //Debug.Log(value); //int의 디폴트 0출력

        //PlayerPrefs.HasKey
        //PlayerPrefs.DeleteKey("test");

        //string path = Application.streamingAssetsPath; // os(운영체제)에 따라 읽기전용으로 사용됨 
        //My project/Assets/StreamingAssets
        //File.WriteAllText(path + "/abc.json" , "test2" );
        ////File.Delete(path + "/abc.json");
        //string result = File.ReadAllText(path + "/abc.json");
        //Debug.Log(result);

        // string path = Application.persistentDataPath + "/Jsons"; //읽기와 쓰기가 가능한 폴더위치
        ////AppData/LocalLow/DefaultCompany/My project   +  /Jsons
        ////if(Directory.Exists(path) == false) // directory = folder
        ////{
        ////    Directory.CreateDirectory(path);
        ////}
        //if(File.Exists("Test/abc.json") == true)
        //{
        //    string result = File.ReadAllText(path + "Test/abc.json");
        //}
        //else// 저장한 파일이 존재하지 않음
        //{
        //    //새로운 저장위치와 데이터를 만들어 줘야함

        //    File.Create(path + "/Test"); //폴더를 만들어줌
        //}


        //cUserData cUserData = new cUserData();
        //cUserData.Name = "가나다";
        //cUserData.Score = 100;

        //cUserData cUserData2 = new cUserData();
        //cUserData.Name = "라마바";
        //cUserData.Score = 200;

        //List<cUserData> listUserData = new List<cUserData>();
        //listUserData.Add(cUserData);
        //listUserData.Add(cUserData2);

        //string jsonData = JsonUtility.ToJson(cUserData); // jsonutility는 속도가 빠름 , 하나의 값만 사용 가능
        // { "Name":"가나다","Score":100}

        //cUserData user2 =  new cUserData();
        //user2 = JsonUtility.FromJson<cUserData>(jsonData);

        //string jsonData = JsonUtility.ToJson(listUserData);
        //jsonUtility는 list를 json으로 변경하면 트러블이 존재함

        //string jsonData = JsonConvert.SerializeObject(listUserData);
        //List<cUserData> afterData = JsonConvert.DeserializeObject<List<cUserData>>(jsonData);
        #endregion

        initRankView();
        viewRank.SetActive(false);


    }

    /// <summary>
    /// 랭크가 저장되어 있따면 저장된 랭크 데이터를 이용해서 랭크뷰를 만들어주고
    /// 랭크가 저장되어 있지 않다면 비어있는 랭크를 만들어 랭크뷰를 만들어줌
    /// </summary>
    private void initRankView()
    {
        List<cUserData> listUserData = null;
        clearRankView();
        if (PlayerPrefs.HasKey(Tool.rankKey) == true) //랭크데이터가 저장되어있었다면
        {
            listUserData = JsonConvert.DeserializeObject<List<cUserData>>(PlayerPrefs.GetString(Tool.rankKey));
        }
        else // 랭크데이터가 저장되어있지 않다면
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
    /// 혹시나 컨텐츠안에 있을 경우를 대비해서 삭제하는 기능
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
            SceneManager.LoadScene(1); //메인신 0 -> 0
            FunctionFade.Instance.ActiveFade(false);
        });

    }

    private void showRanking()
    {
        viewRank.SetActive(true);
    }

    private void GameExit()
    {
        //에디터에서 플레이를 끄는 방법 , 에디터 전용기능
        //빌드를 통해서 밖으로 가지고 나가면 안됨
        //전처리 코드가 조건에 의해서 본인이 없는것처럼 혹은 있는것처럼 동작하게해줌

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else //유니티 에디터에서 실행하지 않았을때
        //빌드했을때 게임 종료
        Application.Quit();
#endif
    }


}