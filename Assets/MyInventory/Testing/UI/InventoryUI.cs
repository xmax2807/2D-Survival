using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyInventory.Testing
{
    public class InventoryUI : MonoBehaviour
    {
        public event Action<int, InventorySlotUI> BindItemEvent;
        public event Action<int, InventorySlotUI> UnbindItemEvent;

        [SerializeField] private InventoryUIPool m_pool;
        [SerializeField] private RectTransform m_slotHolder;
        private int m_slotCount;
        private List<InventorySlotUI> m_slots; // cache for later opening

        public void Open(int count)
        {
            UpdateSlot(count);
        }

        public void Close()
        {

            for (int i = 0; i < m_slotCount; i++)
                {
                    if (m_slots[i] != null)
                    {
                        UnbindItemEvent?.Invoke(i, m_slots[i]);
                    }
                }
        }

        private void UpdateSlot(int count)
        {
            m_slotCount = count;

            if (m_slots == null)
            {
                m_slots = new List<InventorySlotUI>(m_slotCount);
            }
            else if (m_slots.Count < m_slotCount)
            {
                for (int j = m_slots.Count; j < m_slotCount; ++j)
                {
                    //TODO: rent from pool
                    InventorySlotUI slot = m_pool.RentSlotUI();
                    slot.transform.SetParent(m_slotHolder, false);
                    slot.gameObject.SetActive(true);
                    m_slots.Add(slot);
                }
            }

            int i = 0;
            for (; i < m_slotCount; ++i)
            {
                InventorySlotUI slot;
                if (i < m_slots.Count){
                    slot = m_slots[i];
                }
                else{
                    slot = m_pool.RentSlotUI();
                    m_slots.Add(slot);
                }

                slot.transform.SetParent(m_slotHolder, false);
                slot.gameObject.SetActive(true);

                BindItemEvent?.Invoke(i, slot);
            }

            while (i < m_slots.Count)
            {
                InventorySlotUI slot = m_slots[i];
                if (slot != null)
                {
                    UnbindItemEvent?.Invoke(i, slot);
                    slot.gameObject.SetActive(false);
                }
                i++;
            }
        }
        public static long IPow(long baseValue, byte exp)
        {
            byte[] highestBitSet = new byte[]
            {
            0, 1, 2, 2, 3, 3, 3, 3,
        4, 4, 4, 4, 4, 4, 4, 4,
        5, 5, 5, 5, 5, 5, 5, 5,
        5, 5, 5, 5, 5, 5, 5, 5,
        6, 6, 6, 6, 6, 6, 6, 6,
        6, 6, 6, 6, 6, 6, 6, 6,
        6, 6, 6, 6, 6, 6, 6, 6,
        6, 6, 6, 6, 6, 6, 6, 255, // anything past 63 is a guaranteed overflow with base > 1
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255,
            };

            long result = 1;
            byte expByte = highestBitSet[exp];
            if(expByte == 255) // overflow marker
            {
                if (baseValue == 1)
                    return 1;

                if (baseValue == -1)
                    return 1 - 2 * (exp & 1);

                return 0;
            }
            switch (expByte)
            {
                case 6:
                    if ((exp & 1) == 1)
                        result *= baseValue;
                    exp >>= 1;
                    baseValue *= baseValue;
                    goto case 5;
                case 5:
                    if ((exp & 1) == 1)
                        result *= baseValue;
                    exp >>= 1;
                    baseValue *= baseValue;
                    goto case 4;
                case 4:
                    if ((exp & 1) == 1)
                        result *= baseValue;
                    exp >>= 1;
                    baseValue *= baseValue;
                    goto case 3;
                case 3:
                    if ((exp & 1) == 1)
                        result *= baseValue;
                    exp >>= 1;
                    baseValue *= baseValue;
                    goto case 2;
                case 2:
                    if ((exp & 1) == 1)
                        result *= baseValue;
                    exp >>= 1;
                    baseValue *= baseValue;
                    goto case 1;
                case 1:
                    if ((exp & 1) == 1)
                        result *= baseValue;
                    break;
                default:
                    return result;
            }

            return result;
        }
    }
}