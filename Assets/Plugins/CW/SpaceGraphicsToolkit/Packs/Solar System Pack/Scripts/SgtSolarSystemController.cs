using UnityEngine;
using UnityEngine.Events;

namespace SpaceGraphicsToolkit
{
	public class SgtSolarSystemController : MonoBehaviour
	{
		//public bool Focused { set { focused = value; } get { return focused; } } [SerializeField] protected bool focused;

		public UnityEvent OnOrbit { get { if (onOrbit == null) onOrbit = new UnityEvent(); return onOrbit; } } [SerializeField] private UnityEvent onOrbit;

		public UnityEvent OnFocus { get { if (onFocus == null) onFocus = new UnityEvent(); return onFocus; } } [SerializeField] private UnityEvent onFocus;

		public void Orbit()
		{
			if (onOrbit != null)
			{
				onOrbit.Invoke();
			}
		}

		public void Focus()
		{
			if (onFocus != null)
			{
				onFocus.Invoke();
			}
		}
	}
}