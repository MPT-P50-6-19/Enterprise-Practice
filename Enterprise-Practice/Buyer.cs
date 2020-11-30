using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static Enterprise_Practice.DataBase;
using System.Threading;
using static Enterprise_Practice.Program;

namespace Enterprise_Practice
{
    class BuyerSave
    {
        public string login { get; set; }
        
        public string password { get; set; }
        
        public string email { get; set; }
    }

    class ProducCart
    {
        public string category { get; set; }
        
        public string name { get; set; }
        
        public string price { get; set; }
        
        public string email { get; set; }
    }
    public class Buyer
    {
        private static List<ProducCart> ProducCarts = new List<ProducCart>();
        public static string email = "";
        private static void pushCart()
        {
            if (ProducCarts.Count>0)
            {
                var json = JsonSerializer.Serialize(ProducCarts,ProducCarts.GetType());
                using (BinaryWriter writer = new BinaryWriter(File.Open(CartList, FileMode.OpenOrCreate)))
                {
                    writer.Write(json);
                }
            }
        }
        
        
        private static void Cart()
        {
            var all = ProducCarts;
            var button = new string[all.Count + 3];
            var i2 = 0;
            var totalPrice = 0;
            foreach (var vari in all)
            {
                button[i2] = $"{i2 + 1}. {vari.name} | Цена: {vari.price}";
                totalPrice += Convert.ToInt32(vari.price);
                ++i2;
            }
            button[all.Count] = "-Сделать заказ";
            button[all.Count+1] = "-Назад";
            var but = new Button(button);
            
            but.text = (all.Count()>0)?$"Общая цена: {totalPrice}":"Корзина пуста";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == all.Count+1)
                    {
                        return;
                    }
                    else if (but.ClickButton == all.Count)
                    {
                        pushCart(); return;
                    }
                    else
                    {
                        var id = but.ClickButton;
                        delCart(all[id].category,all[id].name);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        
        private static void delCart(string category,string name)
        {
            var vrem = new List<ProducCart>();
            foreach (var vari in ProducCarts)
            {
                if (vari.category!=category && vari.name!=name)
                {
                    vrem.Add(vari);
                }
            }
            ProducCarts = vrem;
        }
        private static void addCart(string category,string name)
        {
            var vrem = new ProducCart();
            var data = DataBase.infoProduct(category, name);
            var price = (Convert.ToInt32(data.shelfLife)<=14)? Convert.ToString(Convert.ToInt32(data.price)/2):data.price;
            vrem.category = category;
            vrem.name = name;
            vrem.price = price;
            vrem.email = email;
            ProducCarts.Add(vrem);
        }
        private static void redProduct(string category,string name)
        {
            var button = new string[3];
            button[0] = "-Добавить в корзину";
            button[1] = "-Назад";
            var but = new Button(button);
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == 1)
                    {
                        return;
                    }
                    else if (but.ClickButton == 0)
                    {
                        addCart(category,name);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        private static void redCategory(string category)
        {
            var all = infoCategory(category).products;
            var button = new string[all.Count + 3];
            var i2 = 0;
            foreach (var vari in all)
            {
                if (Convert.ToInt32(vari.shelfLife)<=14)
                {
                    var price = Convert.ToInt32(vari.price)/2;
                    button[i2] = $"{i2 + 1}. {vari.name} | СКИДКА 50% Цена: {price}";
                }
                else
                {
                    button[i2] = $"{i2 + 1}. {vari.name} | Цена: {vari.price}";
                }
                ++i2;
            }
            button[all.Count] = "-Назад";
            var but = new Button(button);
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == all.Count)
                    {
                        return;
                    }
                    else
                    {
                        redProduct(category,button[but.ClickButton].Split(' ')[1]);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        
        private static void all_сategory()
        {
            var all = all_category();
            var button = new string[all.Count + 2];
            var i2 = 0;
            foreach (var vari in all)
            {
                button[i2] = $"{i2 + 1}. {vari.name}";
                ++i2;
            }
            button[all.Count] = "-Назад";
            var but = new Button(button);
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == all.Count)
                    {
                        return;
                    }
                    else
                    {
                        redCategory(button[but.ClickButton].Split(' ')[1]);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }

        public static void Allout()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathBuyer, FileMode.OpenOrCreate)))
            {
                writer.Write("");
            }
        }
        public static void buyer_menu(string emailP) //Меню для Склад
        {
            email = emailP;
            var but = new Button(new[] {"Категории", "Корзина","Выйти из аккаунта","Выйти"});
            but.text = "Покупатель";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            all_сategory();
                            break;
                        case 1:
                            Cart();
                            break;
                        case 2:
                            Allout();return;
                        case 3: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
    }
}