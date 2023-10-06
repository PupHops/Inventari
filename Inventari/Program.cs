using Inventari.Classes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace Inventari
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            ListItem(player);
            ListItem(player);


            //SortingName(player);
            //DisplayInventory(player);
            //SearchItem(player);

            //ChangeEquipment(player);
            //DisplayInventory(player);
            //ChangeEquipment(player);
            //DisplayInventory(player);
            Switch(player);
            Console.ReadKey();




        }

        static public void AddItem(Player player)
        {

            if (player.Inventory.Count < 10)
            {
                Item newItem = new Item();
                Console.WriteLine("Что вы хотите добваить?");
                Console.WriteLine("1. Оружие");
                Console.WriteLine("2. Броню");
                Console.WriteLine("3. Другое");
                int i;

                while (true)
                {
                    i = GetInput();
                    if (i >= 1 && i <= 3)
                        break;

                    Console.WriteLine("Неправильный выбор. Пожалуйста, выберите 1, 2 или 3.");
                }
                if (i == 1)
                {
                    Console.WriteLine("Введите название:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Введите урон:");
                    int damage = GetInput();

                    Console.WriteLine("Введите цену:");
                    int price = GetInput();

                    Console.WriteLine("Введите вес:");
                    double weight = GetDoubleInput();

                    newItem = new Weapon { Name = name, Price = price, Weitght = weight, Damage = damage };


                }
                else if (i == 2)
                {
                    Console.WriteLine("Введите название:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Введите защиту:");
                    int defence = GetInput();

                    Console.WriteLine("Введите цену:");
                    int price = GetInput();

                    Console.WriteLine("Введите вес:");
                    double weight = GetDoubleInput();

                    newItem = new Armor { Name = name, Price = price, Weitght = weight, Defence = defence };

                }
                else if (i == 3)
                {
                    Console.WriteLine("Введите название:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Введите цену:");
                    int price = GetInput();

                    Console.WriteLine("Введите вес:");
                    double weight = GetDoubleInput();
                    newItem = new Item { Name = name, Price = price, Weitght = weight };

                }
                player.Inventory.Add(newItem);
            }
            else { Console.WriteLine("Инвентарь переполнен"); }
        }
        static public void ListItem(Player player)
        {
            Item newItem;
            newItem = new Weapon { Name = "СасайКудсай", Price = 228, Weitght = 2.5, Damage = 9999 };
            player.Inventory.Add(newItem);

            newItem = new Weapon { Name = "Ядумаю(меч)", Price = 228, Weitght = 2.5, Damage = 9999 };
            player.Inventory.Add(newItem);

            newItem = new Armor { Name = "Ведро(наголову)", Price = 1, Weitght = 5, Defence = 1 };
            player.Inventory.Add(newItem);

            newItem = new Item { Name = "Пробка", Price = 1000, Weitght = 0.10 };
            player.Inventory.Add(newItem);
        }

        static public void SearchItem(Player player)
        {

            Console.WriteLine("Введите искомое слово:");
            string searchItem = Console.ReadLine();
            Player player1 = new Player();

            player1.Inventory = player.Inventory.FindAll(x => x.Name.Contains(searchItem));
            player1.EquipedWeapon = player.EquipedWeapon;
            player1.EquipedArmor = player.EquipedArmor;

            DisplayInventory(player1);
        }

        static public void ChangeEquipment(Player player)
        {
            DisplayInventory(player);
            Console.WriteLine("Выберите номер экиперовки:");

            int selectedNumber = GetInput();
            if (selectedNumber >= 0 && selectedNumber <= player.Inventory.Count)
            {
                Item selected = player.Inventory[selectedNumber - 1];
                if (selected is Weapon)

                {
                    if (player.EquipedWeapon != null)
                    {
                        Console.WriteLine($"Вы сняли экиперованное оружие: {player.EquipedWeapon.Name}");
                        player.Inventory.Add(player.EquipedWeapon);
                    }
                    player.EquipedWeapon = selected as Weapon;
                    player.Inventory.Remove(selected);
                    Console.WriteLine($"Вы экиперовали: {player.EquipedWeapon.Name}");
                }
                else if (selected is Armor)
                {
                    if (player.EquipedArmor != null)
                    {
                        Console.WriteLine($"Вы сняли экиперованную броню: {player.EquipedArmor.Name}");
                        player.Inventory.Add(player.EquipedArmor);
                    }
                    player.EquipedArmor = selected as Armor;
                    player.Inventory.Remove(selected);
                    Console.WriteLine($"Вы экиперовали: {player.EquipedArmor.Name}");
                }
                else { Console.WriteLine("Это не может быть экиперованно."); }
            }
            else
            { Console.WriteLine("Ошибка."); }

        }

        static public void DisplayInventory(Player player)
        {
            Console.WriteLine("Инвентарь:");
            for (int i = 0; i < 10; i++)
            {
                string item = "Пусто";

                if (i < player.Inventory.Count)
                {
                    Item item1 = player.Inventory[i];
                    item = $"Название - {item1.Name,-10}| Вес - {item1.Weitght,-10}| Цена - {item1.Price,-10}";
                    if (item1 is Weapon weapon)
                    {
                        item += $"| Урон - {weapon.Damage,-10}";
                    }
                    else if (item1 is Armor armor)
                    {
                        item += $"| Зашита - {armor.Defence,-10}";
                    }
                }

                Console.WriteLine($" {i + 1,5} | {item,-10}");
            }
            string Armor = "Броня: Отсутсвует";
            string Weapon = "Оружие: Отсутсвует";
            if (player.EquipedWeapon != null)
            {
                Weapon itemWeapon = player.EquipedWeapon;
                Weapon = $"Оружие:Название - {itemWeapon.Name,-10}| Вес - {itemWeapon.Weitght,-10}| Цена - {itemWeapon.Price,-10}| Урон - {itemWeapon.Damage,-10}";
            }
            if (player.EquipedArmor != null)
            {
                Armor itemArmor = player.EquipedArmor;
                Weapon = $"Броня:Название - {itemArmor.Name,-10}| Вес - {itemArmor.Weitght,-10}| Цена - {itemArmor.Price,-10}| Защита - {itemArmor.Defence,-10}";
            }
            Console.WriteLine($"Экиперованно: \n {Weapon} \n {Armor} ");

        }



        static int GetInput()
        {
            int result;

            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    // Если введенная строка может быть преобразована в int, вернуть значение.
                    return result;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                }
            }
        }

        static double GetDoubleInput()
        {
            double result;

            while (true)
            {
                string input = Console.ReadLine();

                if (double.TryParse(input, out result))
                {
                    // Если введенная строка может быть преобразована в double, вернуть значение.
                    return result;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число с плавающей точкой.");
                }
            }
        }
        static public void Switch(Player player)
        {
            while (true)
            {

                Console.WriteLine("1: Инвентарь. \n2: Изменить снаряжение \n3: Поиск предмета \n4: Добавить предмет \n 5: Соритровать по типу");
                switch (GetInput())
                {

                    case 1: Console.Clear(); DisplayInventory(player); break;
                    case 2: Console.Clear(); ChangeEquipment(player); break;
                    case 3: Console.Clear(); SearchItem(player); break;
                    case 4: Console.Clear(); AddItem(player); break;
                    case 5: Console.Clear(); SortingType(player); break;
                    case 6: Console.Clear(); SortingName(player); break;
                    default: break;
                }


            }
        }
        static public void SortingType(Player player)
        {
            Console.WriteLine("Введите тип искомого предмета:");
            Console.WriteLine("1. Оружие");
            Console.WriteLine("2. Броню");
            Console.WriteLine("3. Другое");
            int searchItem;

            while (true)
            {
                searchItem = GetInput();
                if (searchItem >= 1 && searchItem <= 3)
                    break;

                Console.WriteLine("Неправильный выбор. Пожалуйста, выберите 1, 2 или 3.");
            }
            Player player1 = new Player();

            if (searchItem == 1)
            {
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    if (player.Inventory[i] is Weapon)
                    {
                        player1.Inventory.Add(player.Inventory[i]);
                    }

                }
            }
            else if (searchItem == 2)
            {
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    if (player.Inventory[i] is Armor)
                    {
                        player1.Inventory.Add(player.Inventory[i]);

                    }

                }
            }
            else if (searchItem == 2)
            {
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    if (player.Inventory[i] is Item)
                    {
                        player1.Inventory.Add(player.Inventory[i]);
                    }
                }
            }


            DisplayInventory(player1);
        }
        static public void SortingName(Player player)
        {



            Console.WriteLine("Выебите тип сортировки");
            Console.WriteLine("1. Имя");
            Console.WriteLine("2. Цена");
            Console.WriteLine("3. Вес");
            int searchItem;

            while (true)
            {
                searchItem = GetInput();
                if (searchItem >= 1 && searchItem <= 3)
                    break;

                Console.WriteLine("Неправильный выбор. Пожалуйста, выберите 1, 2 или 3.");
            }
            Player player1 = new Player();



            if (searchItem == 1)
            {
                var sortedInventory = player.Inventory.OrderBy(item => item.Name).ToList();
                player1.Inventory = sortedInventory;
                DisplayInventory(player1);
            }
            else if (searchItem == 2)
            {
                var sortedInventory = player.Inventory.OrderBy(item => item.Price).ToList();
                player1.Inventory = sortedInventory;
                DisplayInventory(player1);
            }
            else if (searchItem == 3)
            {
                var sortedInventory = player.Inventory.OrderBy(item => item.Weitght).ToList();
                player1.Inventory = sortedInventory;
                DisplayInventory(player1);
            }

        }

    }
}
