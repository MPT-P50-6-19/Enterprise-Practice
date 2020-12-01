using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Enterprise_Practice.DataBase;
using System.Threading;
using static Enterprise_Practice.Program;

namespace Enterprise_Practice
{
    public class BuyerSave
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
    }

    class CartClass
    {
        public string nameShop { get; set; }
        
        public string email { get; set; }
        
        public List<ProducCart> products { get; set; }
    }
    public class Buyer
    {
        private static List<ProducCart> ProducCarts = new List<ProducCart>();
        public static string email = "";
        public static string nameShop = "";
        
        private static List<CartClass> Getorders() //Возврашет заказы
        {
            string text = "";
            bool auth = false;
            using (BinaryReader reader = new BinaryReader(File.Open(CartList, FileMode.Open)))
            {
                if (reader.BaseStream.Length==0)
                {
                    //ig
                }
                else
                {
                    text += reader.ReadString();
                    auth = true;
                }
            }

            if (auth)
            {
                return JsonSerializer.Deserialize<List<CartClass>>(text);
            }
            return new List<CartClass>();
            
        }
        private static void pushCart()
        {
            if (ProducCarts.Count>0)
            {
                var all = Getorders();
                var arr = new CartClass();
                arr.email = email;
                arr.nameShop = nameShop;
                arr.products = ProducCarts;
                all.Add(arr);
                var json = JsonSerializer.Serialize(all,all.GetType());
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
        private static void addCart(string category,string name,string shop1)
        {
            nameShop = shop1;
            var vrem = new ProducCart();
            var data = DataBase.infoProduct(category, name);
            var time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var srok = (Convert.ToInt32(data.shelfLife) - time)*60*60*24;
            var price = (srok<=14)? Convert.ToString(Convert.ToInt32(data.price)/2):data.price;
            vrem.category = category;
            vrem.name = name;
            vrem.price = price;
            ProducCarts.Add(vrem);
        }
        private static void redProduct(string category,string name, string shop1)
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
                        addCart(category,name,shop1);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        private static void redCategory(string category,string shop1)
        {
            var all = infoCategory(category).products;
            var button = new string[all.Count + 3];
            var i2 = 0;
            var time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            foreach (var vari in all)
            {
                var srok = (Convert.ToInt32(vari.shelfLife) - time)*60*60*24;
                if (srok<=14)
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
                        redProduct(category,button[but.ClickButton].Split(' ')[1],shop1);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        
        private static void all_сategory(string shop1)
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
                        redCategory(button[but.ClickButton].Split(' ')[1],shop1);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }

        public static void Allout()
        {
            File.Delete(pathBuyerSave);
        }
        
        public static void Allset(BuyerSave data)
        {
            var json = JsonSerializer.Serialize(data,data.GetType());
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathBuyerSave, FileMode.OpenOrCreate)))
            {
                writer.Write(json);
            }
        }
        public static void buyer_menu(string emailP)
        {
            var all = getAllShop();
            var button = new string[all.Count + 3];
            var i2 = 0;
            foreach (var vari in all)
            {
                button[i2] = $"{i2 + 1}. {vari}";
                ++i2;
            }
            button[all.Count] = "-Корзина";
            button[all.Count+1] = "-Выйти из аккаунта";
            button[all.Count+2] = "-Выыйти";
            var but = new Button(button);
            email = emailP;
            but.text = "Покупатель";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == all.Count)
                    {
                        Cart();
                    }
                    else if (but.ClickButton == all.Count+1)
                    {
                        Allout();return;
                    }
                    else if (but.ClickButton == all.Count+2)
                    {
                        return;
                    }
                    else
                    {
                        all_сategory(button[but.ClickButton].Split(' ')[1]);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }

        private static List<BuyerSave> getBuy()
        {
            var text = "";
            using (BinaryReader reader = new BinaryReader(File.Open(pathBuyer, FileMode.Open)))
            {
                text += reader.ReadString();
            }
            return JsonSerializer.Deserialize<List<BuyerSave>>(text);
        }
        
        private static void setBuy(List<BuyerSave> buyer)
        {
            var json = JsonSerializer.Serialize(buyer,buyer.GetType());
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathBuyer, FileMode.OpenOrCreate)))
            {
                writer.Write(json);
            }
        }

        private static void sigin()
        {
            Console.Clear();
            Console.WriteLine("Вход покупателя!!!");
            Console.Write("Введите логин: ");
            var login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            var key = 'g';
            var password = "";
            while (key != '\r')
            {
                key = Console.ReadKey().KeyChar;
                if (key != '\r' && key != '\b')
                {
                    password += key.ToString();
                }

                if (key == '\b' && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                }

                Console.Clear();
                Console.WriteLine("Вход покупателя!!!");
                Console.WriteLine("Введите логин: " + login);
                Console.Write("Введите пароль: ");
                foreach (var vari in password)
                {
                    Console.Write("*");
                }
            }

            var all = getBuy();
            foreach (var vari in all)
            {
                if (vari.login==login && vari.password==password)
                {
                    Allset(vari);
                    buyer_menu(vari.email);
                }
            }
            return;
        }

        private static void regist()
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            var all = getBuy();
            var buy = new BuyerSave();
            Console.Clear();
            Console.WriteLine("Регестрация!!!");
            Console.Write("Логин: ");
            var login = Console.ReadLine();
            buy.login = login;
            Console.Write("Введите пароль: ");
            var key = 'g';
            var password = "";
            while (!DataBase.checkPassword(password))
            {
                while (key!='\r')
                {
                    key = Console.ReadKey().KeyChar;
                    if (key!='\r' && key!='\b')
                    {
                        password += key.ToString();
                    }

                    if (key=='\b' && password.Length>0)
                    {
                        password = password.Substring(0,password.Length-1);
                    }
                    Console.Clear();
                    Console.WriteLine("Регестрация покупателя!!!");
                    Console.WriteLine("Введите логин: "+login);
                    Console.Write("Введите пароль: ");
                    foreach (var vari in password)
                    {
                        Console.Write("*");
                    }
                }
            }
            buy.password = password;
            Console.WriteLine();
            Console.Write("Почта: ");
            buy.email = Console.ReadLine();
            while (!Regex.IsMatch(buy.email, pattern, RegexOptions.IgnoreCase))
            {
                Console.Clear();
                Console.WriteLine("Регестрация покупателя!!!");
                Console.WriteLine("Введите логин: "+login);
                Console.WriteLine("Введите пароль: " +password);
                Console.Write("Почта: ");
                buy.email = Console.ReadLine();
            }
            all.Add(buy);
            setBuy(all);
        }

        private static bool check()
        {
            string text = "";
            bool auth = false;
            using (BinaryReader reader = new BinaryReader(File.Open(pathBuyerSave, FileMode.Open)))
            {
                if (reader.BaseStream.Length==0)
                {
                    //ig
                }
                else
                {
                    text += reader.ReadString();
                    auth = true;
                }
            }

            if (auth)
            {
                var json = JsonSerializer.Deserialize<BuyerSave>(text);
                email = json.email;
                return true;
            }
            return false;
        }
        public static void POLIS()
        {
            if (check())
            {
                buyer_menu(email);
            }
            var but = new Button(new[] {"Регестраци", "Вход"});
            but.text = "Склад";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            regist();
                            break;
                        case 1: 
                            sigin();
                            break;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
    }
}