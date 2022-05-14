using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaDataSO", menuName = "Create CharaDataSO")]
public class CharaDataSO : ScriptableObject
{
    public List<CharaData> charaDatasList = new List<CharaData>();
}