using UnityEngine;
using System.Collections;

public class TrackCenter : MonoBehaviour 
{
	void OnTriggerEnter(Collider hit)
	{
          print("hits stuff");
             if(hit.GetComponent<Collider>().CompareTag("Player"))
             {
                 var dis = Vector3.Distance(transform.position, hit.transform.position);
             
                 if(dis <=1)
                 {
                     print("place player in the middle");
            var pos = hit.transform.position;
		        var pos2 = transform.position;
		        pos.x = pos2.x;
                pos.z = pos2.z;
		        hit.transform.position = pos;
                 }
	
             }
	
	}
}
