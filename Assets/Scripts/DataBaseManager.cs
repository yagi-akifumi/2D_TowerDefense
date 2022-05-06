using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public CharaDataSO charaDataSO;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}