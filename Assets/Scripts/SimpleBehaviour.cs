using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public enum EType
{
    Float,
    Integer,
    String,
    IntegerList
}

[System.Serializable]
public class Config
{
    [SerializeField] string ID;
    [SerializeField] EType ValueType;
    [SerializeField] float FloatValue;
    [SerializeField] int IntValue;
    [SerializeField] string StringValue;
    [SerializeField] List<int> IntegerValues;
}

public class SimpleBehaviour : MonoBehaviour
{
    [SerializeField] float BouncePeriod = 2f;
    [SerializeField] float BounceHeight = 1f;
    [SerializeField] List<Config> Configurations;

    Vector3 InitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = Mathf.PingPong(Time.time, BouncePeriod) / BouncePeriod;

        transform.position = InitialPosition + Vector3.up * Mathf.Lerp(-BounceHeight, BounceHeight, progress);
    }

    #if UNITY_EDITOR
    public void RandomiseValues()
    {
        Undo.RecordObject(this, "Randomising values");

        BounceHeight = Random.Range(0.5f, 5f);
        BouncePeriod = Random.Range(1f, 10f);        
    }
    #endif // UNITY_EDITOR
}
