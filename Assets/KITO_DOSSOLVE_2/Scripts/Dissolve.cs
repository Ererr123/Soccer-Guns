
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keto
{
    public class Dissolve : MonoBehaviour
    {
        public float DissolveSpeed = 0.01f;
        public float DissolveYield = 0.1f;

        public ParticleSystem Particle = null;
        

        protected const string DISSOVE_AMOUNT = "_DissolveAmount";
        protected const string IDLE_TRIGGER = "Idle";
        protected const string DISSOLVE_TRIGGER = "Death";

        protected SkinnedMeshRenderer[] m_skinnedMeshRenderers = null;
        protected List<Material> m_materials = new List<Material>();
        protected Animator m_animationController = null;

        protected float m_dissolveStart = -0.2f;
        protected float m_dissolveEnd = 1.2f;

        private void Awake()
        {
            m_animationController = this.GetComponent<Animator>();
            m_skinnedMeshRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < m_skinnedMeshRenderers.Length; i++)
            {
                for (int j = 0; j < m_skinnedMeshRenderers.Length; j++)
                {
                    m_materials.Add(m_skinnedMeshRenderers[j].material);
                }
            }
        }

        public void StartDissolve()
        {
            StopAllCoroutines();
            StartCoroutine(DissolveCoroutine());
        }

        protected virtual IEnumerator DissolveCoroutine()
        {
            yield return null;
        }
    }
}
