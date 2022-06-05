using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float cost;
    [SerializeField]
    private UnityEvent DoorBuyEvent;
    [SerializeField]
    private int blockadeNum;
    [SerializeField]
    private string displayString;
    [SerializeField]
    private GameObject lightObj;
    [SerializeField]
    private Material offMaterial;

    public AudioSource chaChing;

    public float Cost { get => cost; set => cost = value; }
    public string DisplayString { get => displayString; set => displayString = value; }

    public void Buy()
    {
        OnDoorBuy();
        if (animator)
        {
            animator.SetInteger("num", blockadeNum);
            animator.SetTrigger("Open");
        }
        chaChing.Play();
    }

    public void OnDoorBuy()
    {
        DoorBuyEvent.Invoke();
    }

    public void TurnOffLight()
    {
        lightObj.GetComponentInChildren<Renderer>().material = offMaterial;
    }
}
