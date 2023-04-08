using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player1Controller : MonoBehaviour
{
	public Animator anim;
	public bool facingRight;
	
	static int
		_jump = Animator.StringToHash("Jump"),
		_dead = Animator.StringToHash("Dead"),
		_walk = Animator.StringToHash("Walk"),
		_run = Animator.StringToHash("Run"),
		_attack = Animator.StringToHash("Attack");
	
	public void Jump() => anim.SetBool(_jump, true);
	public void JumpOff() => anim.SetBool(_jump, false);

	public void Dead() => anim.SetBool(_dead, true);
	public void DeadOff() => anim.SetBool(_dead, false);
	
	public void Walk() => anim.SetBool(_walk, true);
	public void WalkOff() => anim.SetBool(_walk, false);
	
	public void Run() => anim.SetBool(_run, true);
	public void RunOff() => anim.SetBool(_run, false);
	
	public void Attack() => anim.SetBool(_attack, true);
	public void AttackOff() => anim.SetBool(_attack, false);
}



