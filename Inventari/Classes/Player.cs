using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventari.Classes
{
    internal class Player
    {
        public List<Item> Inventory { get; set; } = new List<Item>();
        public Weapon EquipedWeapon { get; set; }
        public Armor EquipedArmor { get; set; }
    }
}
