using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour, IDoor
	{

		public Animator openandclose_leftdoor;
		public Animator openandclose_rightdoor;
		public bool open;

		void Start()
		{
			open = false;
		}

		public void OnMouseOver()
		{
			{
				if (Player.instance)
				{
					float dist = Vector3.Distance(Player.instance.transform.position, transform.position);
					if (dist < 15)
					{
						if (open == false)
						{
							if (Input.GetMouseButtonDown(0))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
							}

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
			openandclose_leftdoor.Play("Opening");
			openandclose_rightdoor.Play("Opening 1");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
            openandclose_leftdoor.Play("Closing");
			openandclose_rightdoor.Play("Closing 1");
            open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}