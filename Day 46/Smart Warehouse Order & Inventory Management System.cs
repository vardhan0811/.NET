using System;
using System.Collections.Generic;
using System.Text;

namespace Day_46
{
    internal class Smart_Warehouse_Order___Inventory_Management_System
    {
        public interface IEntity
        {
            string Id { get; }
            void Validate();
        }
    }
} 
