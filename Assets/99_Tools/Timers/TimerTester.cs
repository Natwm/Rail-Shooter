﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerTester : MonoBehaviour {
	public Text m_text;
	Timer timerTest;

	void Start () {
		timerTest = new Timer (2.0f, methodeDeTest);
		timerTest.Play ();
	}

	void Update() {
		m_text.text = timerTest.Time.ToString();
	}

	void methodeDeTest(){
	}
}
