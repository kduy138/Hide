using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class BRGlassDoor : MonoBehaviour, IDoor
	{

		public Animator openandclose;
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
			print("you are opening");
			openandclose.Play("BRGlassDoorOpen");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing");
			openandclose.Play("BRGlassDoorClose");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}