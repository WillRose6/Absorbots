using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Player heldByPlayer;

    public Player HeldByPlayer { get => heldByPlayer; set => heldByPlayer = value; }

    public bool checkBeingUsed()
    {
        return heldByPlayer;
    }
}
