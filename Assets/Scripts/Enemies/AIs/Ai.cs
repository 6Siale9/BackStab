using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ai : MonoBehaviour
{
    public abstract void Attack(PlayerInput player);
}
