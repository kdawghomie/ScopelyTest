using UnityEngine;
using System.Collections;

public class PressQToQuit : MonoBehaviour {
	void Update () {

		if (Input.GetKey (KeyCode.Q)) {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			return;
#endif
			Application.Quit();
		}
	}
}
