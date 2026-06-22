using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class OvenFlip: MonoBehaviour
	{

		public Animator openandcloseoven;
		public bool open;

		void Start()
		{
			open = false;
		}

		void OnMouseOver()
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

		IEnumerator opening()
		{
			openandcloseoven.Play("OpenOven");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			openandcloseoven.Play("ClosingOven");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}