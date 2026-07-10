using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseWindowApt : MonoBehaviour, IDoor
	{

		[Header("References")]
		[SerializeField]
		private GameObject windowsCam;
		[SerializeField]
		private GameObject mainCam;

		public Animator openandclosewindow;
		public bool open;

		void Start()
		{
			open = false;
		}

		public void Interact()
		{
			{
				if (Player.instance)
				{
					float dist = Vector3.Distance(Player.instance.transform.position, transform.position);
					if (dist < 15)
					{
                        if (!open)
                        {
                            StartCoroutine(opening());
                        }
                        else
                        {
                            StartCoroutine(closing());
                        }
                    }
				}

			}

		}

		public bool IsOpen()
		{
			return open;
		}

		IEnumerator opening()
		{
			openandclosewindow.Play("Openingwindow");
			open = true;
            yield return new WaitForSeconds(.5f);

            if (ObjectiveManager.instance.GetCurrentState() == ObjectiveManager.State.LookOutTheWindows)
            {
				GameManager.instance.SetGameState(GameManager.State.Dialogue);

				Objective currentObjective = ObjectiveManager.instance.GetCurrentObjective();
                currentObjective.SetCompleted(true);
                currentObjective.SetIsActive(false);
            }
        }

		IEnumerator closing()
		{
			openandclosewindow.Play("Closingwindow");
			open = false;
			yield return new WaitForSeconds(.5f);
		}
	}
}