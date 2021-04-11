using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRegister : MonoBehaviour
{

	public static KeyRegister Instance { get; private set; }

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public bool ShiftActive() {
		return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
	}
}
