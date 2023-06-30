using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTest
{
	public static class TestComands
	{
		public static IEnumerator WaitUntilObjectNotPresent(string path, bool optional = false, float secondsDelay = 0f, float secondsTimeout = 10f)
		{
			if (secondsDelay > 0f)
				yield return new WaitForSeconds(secondsDelay);

			float elapsed = 0f;
			GameObject go = null;
			do
			{
				go = GameObject.Find(path);
				yield return new WaitForSeconds(0.1f);
				elapsed += .1f;
			} while (go != null && go.activeInHierarchy && elapsed < secondsTimeout);

			Debug.Assert(go == null || !go.activeInHierarchy || optional, "WaitUntilObjectNotPresent(" + path + ")");
		}

		public static IEnumerator WaitForObject(string path, bool active = true, bool optional = false, float secondsDelay = 1f, float secondsTimeout = 10f)
		{
			yield return GetGameObject(path, active, optional, secondsDelay, secondsTimeout);
		}

		private static IEnumerator GetGameObject(string path, bool active, bool optional,
												 float secondsDelay, float secondsTimeout, Action<GameObject> callback = null)
		{
			if (secondsDelay > 0f)
				yield return new WaitForSeconds(secondsDelay);

			float elapsed = 0f;
			GameObject go = null;
			do
			{
				go = GameObject.Find(path);
				yield return new WaitForSeconds(0.1f);
				elapsed += .1f;
			} while (elapsed < secondsTimeout && go == null);

			if (go == null)
			{
				Debug.Assert(optional, "GetGameObject(" + path + ")");
				yield break;
			}

			while (elapsed < secondsTimeout && go.activeInHierarchy != active)
			{
				yield return new WaitForSeconds(0.1f);
				elapsed += .1f;
			}

			Debug.Assert(go.activeInHierarchy || !active, "GetGameObjectActive(" + path + ")");
			callback?.Invoke(go);
		}

		public static IEnumerator Tap(string path, bool optional = false, float secondsDelay = 1f, float secondsTimeout = 10f)
		{
			yield return GetGameObject(path, true, optional, secondsDelay, secondsTimeout,
									   go =>
									   {
										   if (go == null)
											   return;
										   bool clicked = false;
										   if (go.TryGetComponent(out IPointerClickHandler pointerClick))
										   {
											   pointerClick.OnPointerClick(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
											   clicked = true;
										   }

										   if (go.TryGetComponent(out IPointerUpHandler pointerUp))
										   {
											   pointerUp.OnPointerUp(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
											   clicked = true;
										   }

										   if (!clicked)
										   {
											   Debug.Assert(false, "Tried to tap " + path + " which is not tappable");
										   }
									   });
		}
	}
}
