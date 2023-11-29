using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Keto
{
    public class DissolveE : Dissolve
    {
        protected override IEnumerator DissolveCoroutine()
        {
            if (m_animationController != null)
            {
                m_animationController.SetTrigger(IDLE_TRIGGER);
            }

            yield return new WaitForSeconds(1f);

            foreach (Material matertial in m_materials)
            {
                matertial.SetFloat(DISSOVE_AMOUNT, m_dissolveStart);
            }

            if (m_animationController != null)
            {
                m_animationController.SetTrigger(DISSOLVE_TRIGGER);
            }

            if (Particle != null)
            {
                Particle.Play();
            }

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
