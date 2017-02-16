using UnityEngine;
using System.Collections;

public class VRToggle : MonoBehaviour {

    public bool _toggle = false;
    public GameObject _camera;
    public GameObject _canvas1;
    public GameObject _canvas2;
    public GameObject button1;
    public GameObject button2;

    void Start()
    {
        _canvas1 = gameObject.transform.GetChild(0).gameObject;
        _canvas2 = gameObject.transform.GetChild(1).gameObject;
        Destroy(button1, 5);
        Destroy(button2, 5);
    }

    public void VRToggler()
    {
        _canvas1.gameObject.SetActive(_toggle);
        _canvas2.gameObject.SetActive(!_toggle);
        _camera.gameObject.SetActive(_toggle);
        _toggle = !_toggle;
    }
}
