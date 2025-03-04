using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FabRanking : MonoBehaviour
{
    //text에서 레퍼런스를 받아옴
    [SerializeField] TMP_Text textRank;
    [SerializeField] TMP_Text textName;
    [SerializeField] TMP_Text textScore;

    public void SetData(string _rank, string _name , int _score)
    {
        textRank.text = _rank;
        textName.text = _name;
        textScore.text = _score.ToString("d8");
    }
}
