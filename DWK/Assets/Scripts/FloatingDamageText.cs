using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(Animator))]
public class FloatingDamageText : MonoBehaviour {
    private Animator _Anim;
    private Text _Text;
    private void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Text = GetComponent<Text>();
    }

    internal void PlayDMG(string amount)
    {
        _Text.text = amount;
        _Anim.SetFloat("Type", Random.Range(0, 3));
        _Anim.SetTrigger("Play");
    }
}
