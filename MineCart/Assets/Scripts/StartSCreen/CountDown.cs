using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private int time = 5;
    public Text timer1;
    private Movement move;

    void Start()
    {
        move =GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();      
        
        move.enabled = false;
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        while (time > 0)
        {
            timer1.text = "" + time;
            yield return new WaitForSeconds(1);
            move.enabled = false;           

            time -= 1;
        }
        move.enabled = true;
        timer1.text = "Blast Off!";
        Destroy(timer1, 2);
        gameObject.GetComponent<ScoreManager>().scoreSwitch = true;
        gameObject.GetComponent<CountDown>().enabled = false;
        gameObject.GetComponent<ScoreManager>().vrMode = GameObject.FindGameObjectWithTag("Canvases").GetComponent<VRToggle>()._toggle;
        GameObject.FindGameObjectWithTag("Canvases").GetComponent<VRToggle>().enabled = false;
    }
}