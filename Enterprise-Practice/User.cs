using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using static Enterprise_Practice.DataBase;
using static Enterprise_Practice.Program;

namespace Enterprise_Practice
{
    public class Admin //Интерфейс админа
    {
        
        
        private static void userADD()
        {
            Console.Clear();
            Console.Write("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): ");
            var type_user = Console.ReadLine();
            Console.Write("Введите логин: ");
            var login = Console.ReadLine();
            while (!DataBase.checkLogin(login))
            {
                Console.Clear();
                Console.WriteLine("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): " + type_user);
                Console.Write("Введите логин: ");
                login = Console.ReadLine();
            }
            Console.Write("Введите пароль: ");
            var password = Console.ReadLine();
            while (!DataBase.checkPassword(password))
            {
                Console.Clear();
                Console.WriteLine("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): "+type_user);
                Console.WriteLine("Введите логин: "+login);
                Console.Write("Введите пароль: ");
                password = Console.ReadLine();
            }
            Console.Write("Введите ФИО: ");
            var fio = Console.ReadLine();
            Console.Write("Введите возраст: ");
            var age = Console.ReadLine();
            Console.Write("Введите образование: ");
            var education = Console.ReadLine();
            Console.Write("Введите опыт работы: ");
            var work_experience = Console.ReadLine();
            var info = $"{fio}|{age}|{education}|{work_experience}";
            DataBase.add_user(login, password, type_user, info);
        }

        private static void updateUser(string login)
        {
            Console.Clear();
            Console.Write("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): ");
            var type_user = Console.ReadLine();
            Console.Write("Введите пароль: ");
            var password = Console.ReadLine();
            while (!DataBase.checkPassword(password))
            {
                Console.Clear();
                Console.WriteLine("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): "+type_user);
                Console.Write("Введите пароль: ");
                password = Console.ReadLine();
            }
            Console.Write("Введите ФИО: ");
            var fio = Console.ReadLine();
            Console.Write("Введите возраст: ");
            var age = Console.ReadLine();
            Console.Write("Введите образование: ");
            var education = Console.ReadLine();
            Console.Write("Введите опыт работы: ");
            var work_experience = Console.ReadLine();
            var info = $"{fio}|{age}|{education}|{work_experience}";
            DataBase.update_user(login, password, type_user, info);
        }

        private static void userRed(string login)
        {
            var but = new Button(new[] {"Изменить", "Удалить", "-Назад"});
            but.text = $"Редактировать пользователя {login}";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            updateUser(login);
                            break;
                        case 1:
                            del_user(login);
                            break;
                        case 2: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }


        private static void all_user_admin()
        {
            var all = all_user();
            var button = new string[all.Count + 1];
            var i2 = 0;
            foreach (var vari in all)
            {
                if (vari["type_user"] != "1" && vari["type_user"] != "6")
                {
                    button[i2] = $"{i2 + 1}. {vari["login"]}";
                    ++i2;
                }

            }

            button[all.Count - 1] = "-Добавить";
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
                    else if (but.ClickButton == all.Count - 1)
                    {
                        userADD();
                    }
                    else
                    {
                        userRed(button[but.ClickButton].Split(' ')[1]);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }

        public static void admin_menu() //Меню для админа
        {
            var but = new Button(new[] {"Список", "Выйти"});
            but.text = "Админ";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            all_user_admin();
                            break;
                        case 1: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
    }

    public class Personnel //Интерфейс Кадры
    {
        
        private static void userADD()
        {
            Console.Clear();
            Console.Write("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): ");
            var type_user = Console.ReadLine();
            Console.Write("Введите логин: ");
            var login = Console.ReadLine();
            while (!DataBase.checkLogin(login))
            {
                Console.Clear();
                Console.WriteLine("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): " + type_user);
                Console.Write("Введите логин: ");
                login = Console.ReadLine();
            }
            Console.Write("Введите пароль: ");
            var password = Console.ReadLine();
            while (!DataBase.checkPassword(password))
            {
                Console.Clear();
                Console.WriteLine("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель): "+type_user);
                Console.WriteLine("Введите логин: "+login);
                Console.Write("Введите пароль: ");
                password = Console.ReadLine();
            }
            Console.Write("Введите ФИО: ");
            var fio = Console.ReadLine();
            Console.Write("Введите возраст: ");
            var age = Console.ReadLine();
            Console.Write("Введите образование: ");
            var education = Console.ReadLine();
            Console.Write("Введите опыт работы: ");
            var work_experience = Console.ReadLine();
            var info = $"{fio}|{age}|{education}|{work_experience}";
            DataBase.add_user(login, password, type_user, info);
        }
        private static void updateUser(string login)
        {
            Console.Clear();
            Console.Write("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия): ");
            var type_user = Console.ReadLine();
            Console.Write("Введите ФИО: ");
            var fio = Console.ReadLine();
            Console.Write("Введите возраст: ");
            var age = Console.ReadLine();
            Console.Write("Введите образование: ");
            var education = Console.ReadLine();
            Console.Write("Введите опыт работы: ");
            var work_experience = Console.ReadLine();
            var info = $"{fio}|{age}|{education}|{work_experience}";
            DataBase.update_user(login, DataBase.info_user(login)["password"], type_user, info);
        }

        private static void userRed(string login)
        {
            var but = new Button(new[] {"Изменить", "-Назад"});
            but.text = $"Редактировать пользователя {login}";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            updateUser(login);
                            break;
                        case 1: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }


        private static void all_user_personnel()
        {
            var all = all_user();
            var button = new string[all.Count + 1];
            var i2 = 0;
            foreach (var vari in all)
            {
                if (vari["type_user"] != "1" && vari["type_user"] != "6")
                {
                    button[i2] = $"{i2 + 1}. {vari["login"]}";
                    ++i2;
                }

            }

            button[all.Count - 1] = "-Добавить";
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
                    else if (but.ClickButton == all.Count - 1)
                    {
                        userADD();
                    }
                    else
                    {
                        userRed(button[but.ClickButton].Split(' ')[1]);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        public static void personnel_menu() //Меню для Кадры
        {
            var but = new Button(new[] {"Список", "Выйти"});
            but.text = "Кадры";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            all_user_personnel();
                            break;
                        case 1: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
    }
    
    public class Warehouse //Интерфейс Склад
    {

        private static void addProduct(string category)
        {
            Console.Clear();
            Console.Write("Название: ");
            var nameProduct = Console.ReadLine();
            Console.Write("Цена: ");
            var price = Console.ReadLine();
            Console.Write("Срок годности: ");
            var shelfLife = Console.ReadLine();
            Console.Write("Количество: ");
            var count = Console.ReadLine();
            DataBase.addProduct(category,nameProduct,price,shelfLife,count);
        }
        
        private static void changeProduct(string category,string nameProduct)
        {
            Console.Clear();
            Console.Write("Цена: ");
            var price = Console.ReadLine();
            Console.Write("Срок годности: ");
            var shelfLife = Console.ReadLine();
            Console.Write("Количество: ");
            var count = Console.ReadLine();
            DataBase.updateProduct(category,nameProduct,price,shelfLife,count);
        }
        
        private static void redProduct(string category,string name)
        {
            var button = new string[3];
            button[0] = "-Изменить товар";
            button[1] = "-Удалить товар";
            button[2] = "-Назад";
            var but = new Button(button);
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == 2)
                    {
                        return;
                    }
                    else if (but.ClickButton == 1)
                    {
                        DataBase.delProduct(category,name);
                    }
                    else if (but.ClickButton == 0)
                    {
                        changeProduct(category,name);
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
                button[i2] = $"{i2 + 1}. {vari.name}";
                ++i2;
            }
            button[all.Count] = "-Добавить товар";
            button[all.Count+1] = "-Удалить категорию";
            button[all.Count+2] = "-Назад";
            var but = new Button(button);
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == all.Count+2)
                    {
                        return;
                    }
                    else if (but.ClickButton == all.Count+1)
                    {
                        DataBase.del_category(category);
                    }
                    else if (but.ClickButton == all.Count)
                    {
                        addProduct(category);
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
        private static void addCategory()
        {
            Console.Clear();
            Console.Write("Название: ");
            var name = Console.ReadLine();
            DataBase.addCategory(name);
        }

        private static void all_сategory_personnel()
        {
            var all = all_category();
            var button = new string[all.Count + 2];
            var i2 = 0;
            foreach (var vari in all)
            {
                button[i2] = $"{i2 + 1}. {vari.name}";
                ++i2;
            }
            button[all.Count] = "-Добавить";
            button[all.Count+1] = "-Назад";
            var but = new Button(button);
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
                        addCategory();
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
        public static void warehouse_menu() //Меню для Склад
        {
            var but = new Button(new[] {"Категории", "Выйти"});
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
                            all_сategory_personnel();
                            break;
                        case 1: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
    }

    public class Seller //Интерфейс Продовца
    {
        public static string CartList = "C:/Users/79096/RiderProjects/ElectronicJournal/CartList.dat";
        private static List<ProducCart> Getorders() //Возврашет заказы
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
                return JsonSerializer.Deserialize<List<ProducCart>>(text);
            }
            return new List<ProducCart>();
            
        }

        private static void PushOrders(List<ProducCart> all)
        {
            if (all.Count()>0)
            {
                var email = all[0].email;
                var text = "Чек.\n";
                var totalPrice = 0;
                foreach (var vari in all)
                {
                    text += $"---{vari.name} цена {vari.price} за 1шт.\n";
                    totalPrice += Convert.ToInt32(vari.price);
                    var prod = DataBase.infoProduct(vari.category,vari.name);
                    prod.count = Convert.ToString(Convert.ToInt32(prod.count)-1);
                    DataBase.updateProduct(vari.category,prod.name,prod.price,prod.shelfLife,prod.count);
                }
                text += $"\n Итоговая цена: {totalPrice}\nДата заказа:{DateTime.Now}";
                SendMessage(email,"Чек на заказ в программе",text);
                DataBase.addOperation(totalPrice);
                //Удаление заказа
                // using (BinaryWriter writer = new BinaryWriter(File.Open(CartList, FileMode.OpenOrCreate)))
                // {
                //     writer.Write("");
                // }
            }
        }
        
        
        private static void orders() // Заказы
        {
            var all = Getorders();
            var button = new string[all.Count + 2];
            var doptext = "";
            if (all.Count()>0)
            {
                var i2 = 0;
                var totalPrice = 0;
                foreach (var vari in all)
                {
                    var info = DataBase.infoProduct(vari.category,vari.name);
                    button[i2] = $"{i2 + 1}. {vari.name} | Цена: {vari.price} | На складе {info.shelfLife}";
                    ++i2;
                    totalPrice += Convert.ToInt32(vari.price);
                }

                doptext = $"Итоговая цена {totalPrice}";
            }
            else
            {
                doptext = "\n Заказов нет";
            }
            button[all.Count] = "-Отправить заказ";
            button[all.Count+1] = "-Назад";
            var but = new Button(button);
            but.text = "Заказ: "+doptext;
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
                        PushOrders(all);
                        return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        public static void saller_menu() //Меню для Продовца
        {
            var but = new Button(new[] {"Заказы", "Выйти"});
            but.text = "Продовец";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            orders();
                            break;
                        case 1: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
    }

    public class Accountant //Интерфейс Бухгалтера
    {

        private static double Payroll(int salary)
        {
            var salary2 = Convert.ToDouble(salary);
            var salarySave = salary2;
            salary2 += Math.Round((double)(salarySave * 22 / 100));
            salary2 += Math.Round((double)(salarySave * 5.1 / 100));
            salary2 += Math.Round((double)(salarySave * 2.9 / 100));
            return salary2;
        }
        
        private static void StatisticsYear()
        {
            var all = getSalaryANDOperations();
            var totalPrice = 0;
            var time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            foreach (var vari in all.products)
            {
                if (vari.data+60*60*24*30*12>time)
                {
                    totalPrice += vari.totalPrice;
                }
            }
            var all2 = getALLUser();
            var paymentAll = 0.0;
            foreach (var vari in all2)
            {
                var payment = 0;
                switch (vari["type_user"])
                {
                    case "2": payment = all.SalaryPersonnel;break;
                    case "3": payment = all.SalaryWarehouse;break;
                    case "4": payment = all.SalarySaller;break;
                }

                paymentAll += Payroll(payment*12);
            }
            var but = new Button(new []{"-Назад"});
            but.text = $"Продажи за год: {totalPrice}р.\nЗарплата: {paymentAll}\nИтог: {totalPrice-paymentAll}";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        private static void StatisticsFloorYear()
        {
            var all = getSalaryANDOperations();
            var totalPrice = 0;
            var time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            foreach (var vari in all.products)
            {
                if (vari.data+60*60*24*30*6>time)
                {
                    totalPrice += vari.totalPrice;
                }
            }
            var all2 = getALLUser();
            var paymentAll = 0.0;
            foreach (var vari in all2)
            {
                var payment = 0;
                switch (vari["type_user"])
                {
                    case "2": payment = all.SalaryPersonnel;break;
                    case "3": payment = all.SalaryWarehouse;break;
                    case "4": payment = all.SalarySaller;break;
                }

                paymentAll += Payroll(payment*6);
            }
            var but = new Button(new []{"-Назад"});
            but.text = $"Продажи за пол года: {totalPrice}р.\nЗарплата: {paymentAll}\nИтог: {totalPrice-paymentAll}";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        private static void StatisticsMonth()
        {
            var all = getSalaryANDOperations();
            var totalPrice = 0;
            var time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            foreach (var vari in all.products)
            {
                if (vari.data+60*60*24*30>time)
                {
                    totalPrice += vari.totalPrice;
                }
            }
            var all2 = getALLUser();
            var paymentAll = 0.0;
            foreach (var vari in all2)
            {
                var payment = 0;
                switch (vari["type_user"])
                {
                    case "2": payment = all.SalaryPersonnel;break;
                    case "3": payment = all.SalaryWarehouse;break;
                    case "4": payment = all.SalarySaller;break;
                }
                paymentAll += Payroll(payment);
            }
            var but = new Button(new []{"-Назад"});
            but.text = $"Продажи за месяц: {totalPrice}р.\nЗарплата: {paymentAll}\nИтог: {totalPrice-paymentAll}";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        private static void StatisticsDay()
        {
            var all = getSalaryANDOperations();
            var totalPrice = 0;
            var time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            foreach (var vari in all.products)
            {
                if (vari.data+60*60*24>time)
                {
                    totalPrice += vari.totalPrice;
                }
            }
            var but = new Button(new []{"-Назад"});
            but.text = $"Продажи за день: {totalPrice}р.";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        
        private static void updateSalary()
        {
            Console.Clear();
            Console.Write("Введите зарплату Кадров: ");
            var SalaryPersonnel =  Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите зарплату Кладовщиков: ");
            var SalarySaller =  Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите зарплату Прадовцов: ");
            var SalaryWarehouse = Convert.ToInt32(Console.ReadLine());
            DataBase.updateSalary(SalaryPersonnel,SalarySaller,SalaryWarehouse);
        }
        public static void accountant_menu() //Меню для Бухгалтера
        {
            var but = new Button(new[] {"Статистика за день","Статистика за месяц","Статистика за полгода","Статистика за год","Изменить зарплаты","Выйти"});
            but.text = "Бухгалтер";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:StatisticsDay(); break;
                        case 1: StatisticsMonth();break;
                        case 2: StatisticsFloorYear();break;
                        case 3: StatisticsYear();break;
                        case 4: updateSalary();break;
                        case 5: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
    }

}