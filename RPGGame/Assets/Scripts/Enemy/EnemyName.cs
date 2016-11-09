using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyName : MonoBehaviour {
    public void SetName(string name)
    {
        GetComponent<Text>().text = name;
    }   
}
