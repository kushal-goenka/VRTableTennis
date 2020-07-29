using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {

            SceneManager.LoadScene("game__room");

        }

    }
}
