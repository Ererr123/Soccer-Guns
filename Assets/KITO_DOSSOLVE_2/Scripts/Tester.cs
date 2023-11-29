using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keto
{
    public class Tester : MonoBehaviour
    {
        public GameObject SampleA = null;
        public GameObject SampleB = null;
        public GameObject SampleC = null;
        public GameObject SampleD = null;
        public GameObject SampleE = null;

        void OnGUI()
        {
            if (GUI.Button(new Rect(100, 100, 100, 80), "SampleA"))
            {
                SampleA.SetActive(true);
                SampleB.SetActive(false);
                SampleC.SetActive(false);
                SampleD.SetActive(false);
                SampleE.SetActive(false);
                SampleA.GetComponent<Dissolve>().StartDissolve();
            }

            if (GUI.Button(new Rect(300, 100, 100, 80), "SampleB"))
            {
                SampleA.SetActive(false);
                SampleB.SetActive(true);
                SampleC.SetActive(false);
                SampleD.SetActive(false);
                SampleE.SetActive(false);
                SampleB.GetComponent<Dissolve>().StartDissolve();
            }

            if (GUI.Button(new Rect(500, 100, 100, 80), "SampleC"))
            {
                SampleA.SetActive(false);
                SampleB.SetActive(false);
                SampleC.SetActive(true);
                SampleD.SetActive(false);
                SampleE.SetActive(false);
                SampleC.GetComponent<Dissolve>().StartDissolve();
            }

            if (GUI.Button(new Rect(700, 100, 100, 80), "SampleD"))
            {
                SampleA.SetActive(false);
                SampleB.SetActive(false);
                SampleC.SetActive(false);
                SampleD.SetActive(true);
                SampleE.SetActive(false);
                SampleD.GetComponent<Dissolve>().StartDissolve();
            }

            if (GUI.Button(new Rect(900, 100, 100, 80), "SampleE"))
            {
                SampleA.SetActive(false);
                SampleB.SetActive(false);
                SampleC.SetActive(false);
                SampleD.SetActive(false);
                SampleE.SetActive(true);
                SampleE.GetComponent<Dissolve>().StartDissolve();
            }
        }
    }
}
