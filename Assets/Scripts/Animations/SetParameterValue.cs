using UnityEngine;

public class SetParameterValue : StateMachineBehaviour
{
	[SerializeField] Condition[] _onEnter, _onUpdate, _onExit;
	
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		foreach(var condition in _onEnter)
			condition.Invoke(animator);
	}
	
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		foreach(var condition in _onUpdate)
			condition.Invoke(animator);
	}
	
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		foreach(var condition in _onExit)
			condition.Invoke(animator);
	}
	
	[System.Serializable]
	public class Condition
	{
		public string parameter;
		
		public enum Type{ Floating, Integer, Boolean, Trigger }
		public Type type;
		
		[Space]
		public float floatValue;
		public int intValue;
		public bool boolValue;
		
		public void Invoke(Animator animator)
		{
			switch(type)
			{
				case Type.Floating: animator.SetFloat(parameter, floatValue); break;
				case Type.Integer: animator.SetInteger(parameter, intValue); break;
				case Type.Boolean: animator.SetBool(parameter, boolValue); break;
				case Type.Trigger: animator.SetTrigger(parameter); break;
			}
		}
	}
}
