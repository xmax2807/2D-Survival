using System;
using System.Collections;
using System.Collections.Generic;

namespace MyInventory.Testing
{
    public class InventoryUIController : IInventoryUI
    {
        private readonly InventoryUI m_ui;
        private InventorySlotItem[] m_items;
        private InventoryContext _context;
        public InventoryUIController(InventoryUI ui){
            m_ui = ui;
        }
        public void Open(InventoryContext context){
            _context = context;
            m_ui.BindItemEvent += BindItemElement;
            m_ui.UnbindItemEvent += UnbindItemElement;
            m_items = context.GetAllItems();
            m_ui.Open(m_items.Length);
        }

        public void Close(){
            m_ui.Close();
            m_ui.BindItemEvent -= BindItemElement;
            m_ui.UnbindItemEvent -= UnbindItemElement;
        }

        private void BindItemElement(int index, InventorySlotUI slot)
        {
            slot.SetAmount(m_items[index].Count);
            slot.SetIcon(_context.GetItemIcon(m_items[index].IconId));
        }

        private void UnbindItemElement(int index, InventorySlotUI slot){
            slot.SetAmount(0);
            slot.SetIcon(null);
        }
    }
}
