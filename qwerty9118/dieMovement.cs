using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class dieMovement : MonoBehaviour
{
    public bool rolling;
    public bool startRoll;
    private DieLauncher dieLauncher;
    private int diceSound;

    void Update()
    {
        if(startRoll == false) {
            rolling = true;
            startRoll = true;
        }
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.1 && rolling)
        {
            int closestIndex = 0;
            float minZ = 100;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).position.z < minZ)
                {
                    minZ = transform.GetChild(i).position.z;
                    closestIndex = i;
                }
            }

            Debug.Log(transform.GetChild(closestIndex).name);

            dieLauncher.diceOutput.Add(int.Parse(Regex.Match(transform.GetChild(closestIndex).name, @"\d+").Value));

            rolling = false;

            Destroy(gameObject, 5.0f);

        }
    }

    private void Awake()
    {
        dieLauncher = GameObject.Find("Dice Gun").GetComponent<DieLauncher>();
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 9.85f) / 50;
    }
}
