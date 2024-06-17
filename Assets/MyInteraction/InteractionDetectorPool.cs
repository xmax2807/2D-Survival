using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyInteraction
{
    internal sealed class InteractionDetectorPool
    {
        private readonly Func<IInteractionDetector> m_factory;
        private List<IInteractionDetector> m_detectors = new List<IInteractionDetector>();
        private int m_currentActiveIndex = 0;

        public InteractionDetectorPool(Func<IInteractionDetector> factory){
            if(factory == null) throw new ArgumentNullException(nameof(factory));
            this.m_factory = factory;
        }


        public IInteractionDetector RentADetector(){
            if(m_currentActiveIndex >= m_detectors.Count){
                return m_factory.Invoke();
            }

            IInteractionDetector detector = m_detectors[m_currentActiveIndex];
            m_currentActiveIndex++;

            detector.SetEnable(true);
            return detector;
        }

        public void ReturnADetector(IInteractionDetector detector){
            if(detector == null) return;

            detector.SetEnable(false);
            if(m_currentActiveIndex >= m_detectors.Count){
                m_detectors.Add(detector);
            }
            else{
                --m_currentActiveIndex;
                m_detectors[m_currentActiveIndex] = detector;
            }
        }
    }
}