using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    public string winScreen;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene("Win Screen");
        }
    }
}
