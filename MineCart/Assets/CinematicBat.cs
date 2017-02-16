using UnityEngine;
using System.Collections;

public class CinematicBat : MonoBehaviour {

    public bool leftOrRight;//left is false right is true

    void OnTriggerEnter(Collider Hit)
    {
        if (Hit.gameObject.CompareTag("Player"))
        {
            if (leftOrRight)
            {
                //hor ==-5
                Hit.GetComponentInChildren<HealthCart>().hor = -5;
            }
            else
            {
                //hor ==5
                Hit.GetComponentInChildren<HealthCart>().hor = 5;
            }
        }
    }
    
}
