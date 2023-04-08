using System.Collections;
using UnityEngine;

public class PlayerInput : SceneObjectSingleton<PlayerInput>
{
	const string JMP_BTN = "Jump";
	
	public static bool JumpButton() => Input.GetButton(JMP_BTN) || Instance._jmpBtn;
	public static bool JumpButtonDown() => Input.GetButtonDown(JMP_BTN) || Instance._jmpBtnD;
	public static bool JumpButtonUp() => Input.GetButtonUp(JMP_BTN) || Instance._jmpBtnU;
	
	bool _jmpBtn, _jmpBtnD, _jmpBtnU;
	
	public void OnJumpButton() => _jmpBtn = true;
	
	public void OnJumpButtonDown()
	{
		StartCoroutine(r());
		IEnumerator r()
		{
			_jmpBtnD = true;
			yield return new WaitForEndOfFrame();
			_jmpBtnD = false;
		}
	}
	
	public void OnJumpButtonUp()
	{
		_jmpBtn = false;
		
		StartCoroutine(r());
		IEnumerator r()
		{
			_jmpBtnU = true;
			yield return new WaitForEndOfFrame();
			_jmpBtnU = false;
		}
	}
}