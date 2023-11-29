using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Keto
{
    public class DissolveB : Dissolve
    {
        protected override IEnumerator DissolveCoroutine()
        {
            yield return new WaitForSeconds(1f);

            foreach (Material matertial in m_materials)
            {
                matertial.SetFloat(DISSOVE_AMOUNT, m_dissolveStart);
            }

            if (Particle != null)
            {
                Particle.Play();
            }

            yield return new WaitForSeconds(0.5f);

            if (m_materials.Count > 0)
            {
                float dissovleAmount = m_dissolveStart;
                float speedMulti = 1f;
                while (dissovleAmount < m_dissolveEnd)
                {
                    dissovleAmount += DissolveSpeed * speedMulti;
                    speedMulti += 0.1f;
                    foreach (Material matertial in m_materials)
                    {
                        matertial.SetFloat(DISSOVE_AMOUNT, dissovleAmount);
                    }
                    yield return new WaitForSeconds(DissolveYield);
                }
            }
        }
    }
}
