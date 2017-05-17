using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyLevel : MonoBehaviour {
    public void SetLevel(string name)
    {
        GetComponent<Text>().text = name;
    }   
}
