using System;
using System.Collections;
using UnityEngine;
namespace Project.Utils
{
    public class FadeObject2D : MonoBehaviour, IEquatable<FadeObject2D>
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        Color _originalColor;
        IEnumerator m_fadeInCoroutine;
        IEnumerator m_fadeOutCoroutine;

        void Awake()
        {
            if(_spriteRenderer == null){
                #if UNITY_EDITOR
                Debug.LogError("SpriteRenderer is null");
                #endif
                enabled = false;
                return;
            }
            _originalColor = _spriteRenderer.color;
        }

        private void SetAlpha(float alpha)
        {
            Color newColor = new(_originalColor.r, _originalColor.g, _originalColor.b, alpha);
            _spriteRenderer.color = newColor;
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        private void SetDefaultColor()
        {
            _spriteRenderer.color = _originalColor;
        }

        public bool Equals(FadeObject2D other)
        {
            return this.GetInstanceID() == other.GetInstanceID();
        }

        public IEnumerator FadeOut(Color toColor, bool force = false)
        {
            if (m_fadeOutCoroutine != null)
            {
                yield break;
            }

            m_fadeOutCoroutine = FadeOutAsync(toColor: toColor);

            if(this.m_fadeInCoroutine != null){
                if(force == false){
                    yield return m_fadeInCoroutine;
                }
                else{
                    StopCoroutine(this.m_fadeInCoroutine);
                    m_fadeInCoroutine = null;
                }
            }
            yield return StartCoroutine(m_fadeOutCoroutine);
        }
        private IEnumerator FadeOutAsync(Color toColor)
        {
            Color currentColor = this._originalColor;
            Color deltaColor = toColor - this._originalColor;

            while (currentColor.a > toColor.a)
            {
                currentColor += deltaColor * Time.deltaTime;
                SetColor(currentColor);
                yield return null;
            }

            SetColor(toColor);
            m_fadeOutCoroutine = null;
        }

        public IEnumerator FadeIn(Color fromColor, bool force = false)
        {
            if (m_fadeInCoroutine != null)
            {
                yield break;
            }

            m_fadeInCoroutine = FadeInAsync(fromColor);
            
            if(this.m_fadeOutCoroutine != null){

                if(force == false){
                    yield return m_fadeOutCoroutine;
                }
                else{
                    StopCoroutine(this.m_fadeOutCoroutine);
                    m_fadeOutCoroutine = null;
                }
            }
            yield return StartCoroutine(m_fadeInCoroutine);
        }

        public IEnumerator ForceFadeIn(){
            return FadeIn(fromColor: this._spriteRenderer.color, force: true);
        }
        public IEnumerator FadeInAsync(Color fromColor)
        {
            Color currentColor = fromColor;
            Color deltaColor = this._originalColor - fromColor;

            while (currentColor.a < this._originalColor.a)
            {
                currentColor += deltaColor * Time.deltaTime;
                SetColor(currentColor);
                yield return null;
            }

            SetDefaultColor();
            m_fadeInCoroutine = null;
        }
    }
}