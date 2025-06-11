using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter: ");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit: ");
    }
}

