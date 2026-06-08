using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{

	public class Drawer_Pull_X : MonoBehaviour, IDoor
	{

		public Animator pull_01;
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
					if (dist < 10)
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

		public bool IsOpen() { return open; }

		IEnumerator opening()
		{
			print("you are opening the door");
			pull_01.Play("openpull_01");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			pull_01.Play("closepush_01");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}