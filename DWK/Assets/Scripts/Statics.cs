using UnityEngine;
using System.Collections;

public class Statics : MonoBehaviour {

    public static string GetNumber(float number)
    {
        number = number < 0 ? number * -1 : number * 1;
        return number.ToString("0");
    }
}
