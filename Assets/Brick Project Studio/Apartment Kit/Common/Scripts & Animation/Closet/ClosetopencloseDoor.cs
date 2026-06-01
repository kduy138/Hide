using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class ClosetopencloseDoor : MonoBehaviour, IDoor
	{
		public Animator Closetopenandclose;
		public bool open;

        private void Start()
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
			Closetopenandclose.Play("ClosetOpening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			Closetopenandclose.Play("ClosetClosing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}