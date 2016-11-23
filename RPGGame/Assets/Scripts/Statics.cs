using UnityEngine;
using System.Collections;

public class Statics : MonoBehaviour {

    public static string GetNumber(float number)
    {
        return number.ToString("0.00");
    }
}
