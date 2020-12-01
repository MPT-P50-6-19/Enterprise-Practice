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
            Console.Write("Введите логин: ");
            var login = Console.ReadLine();
            while (!DataBase.checkLogin(login))
            {
                Console.Clear();
                Console.Write("Введите логин: ");
                login = Console.ReadLine();
            }
            Console.Write("Введите пароль: ");
            var password = Console.ReadLine();
            while (!DataBase.checkPassword(password))
            {
                Console.Clear();
                Console.WriteLine("Введите логин: "+login);
                Console.Write("Введите пароль: ");
                password = Console.ReadLine();
            }
            Console.Write("Введите ФИО: ");
            var fio = Console.ReadLine();
            Console.Write("Введите возраст: ");
            var age = Console.ReadLine();
            while (Convert.ToInt32(age)<1960 || Convert.ToInt32(age)>2002)
            {
                Console.Clear();
                Console.WriteLine("Введите логин: "+login);
                Console.WriteLine("Введите пароль: "+password);
                Console.WriteLine("Введите ФИО: "+fio);
                Console.Write("Введите возраст: ");
                age = Console.ReadLine();
            }
            var info = $"{fio}|{age}|null|null";
            DataBase.add_user(login, password, "0", info);
        }

        private static void updateUser(string login)
        {
            var but = new Button(new[] {"Пароль","Фио","Возраст","-Назад"});
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:RedInfo.upPassword(login); break;
                        case 1: RedInfo.upFio(login);break;
                        case 2: RedInfo.upAge(login);break;
                        case 3: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
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
        private static void updateUser(string login)
        {
            var inf = info_user(login);
            var button = (inf["type_user"] == "4") ?new[] {"Тип пользователя","ФИО","возраст","образование","опыт работы","-Назад","магазин"}:new[] {"Тип пользователя","ФИО","возраст","образование","опыт работы","-Назад"};
            var but = new Button(button);
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:RedInfo.upTypeUser(login); break;
                        case 1: RedInfo.upFio(login);break;
                        case 2: RedInfo.upAge(login);break;
                        case 3: RedInfo.upEducation(login);break;
                        case 4: RedInfo.upWorkExperience(login);break;
                        case 5: return;
                        case 6: RedInfo.upShop(login);break;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
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
            var button = new string[all.Count];
            var i2 = 0;
            foreach (var vari in all)
            {
                if (vari["type_user"] != "1" && vari["type_user"] != "6")
                {
                    button[i2] = (vari["type_user"]=="0")? $"{i2 + 1}. {vari["login"]} | Без работы":$"{i2 + 1}. {vari["login"]}";
                    ++i2;
                }
            }
            button[all.Count-1] = "-Назад";
            var but = new Button(button);
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    if (but.ClickButton == all.Count-1)
                    {
                        return;
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
            Console.Write("Срок годности в днях: ");
            var shelfLife = Convert.ToInt32(Console.ReadLine());
            var time = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + shelfLife*24*60*60;
            Console.Write("Количество: ");
            var count = Console.ReadLine();
            DataBase.addProduct(category,nameProduct,price,Convert.ToString(time),count);
        }
        
        private static void changeProduct(string category,string nameProduct)
        {
            var but = new Button(new[] {"Цена","Срок годности","Количество","-Назад"});
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:RedInfo.upPrice(category,nameProduct); break;
                        case 1: RedInfo.upShelfLife(category,nameProduct);break;
                        case 2: RedInfo.upCount(category,nameProduct);break;
                        case 3: return;
                    }

                    but.Read_keyAsync();
                }
                Thread.Sleep(500);
            }
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
                button[i2] = $"{i2 + 1}. {vari.name} | {vari.count}";
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
        private static string shope = "";
        private static List<CartClass> Getorders(string shop) //Возврашет заказы
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
                var arr =  JsonSerializer.Deserialize<List<CartClass>>(text);
                var out1 = new List<CartClass>();
                foreach (var vari in arr)
                {
                    if (vari.nameShop==shop)
                    {
                        out1.Add(vari);
                    }
                }

                return out1;
            }
            return new List<CartClass>();
            
        }

        private static void PushOrders(List<ProducCart> all, string email)
        {
            if (all.Count()>0)
            {
                var text = "Чек.\n";
                var totalPrice = 0;
                var TF = new Dictionary<string,int>();
                foreach (var vari in all)
                {
                    if (TF.ContainsKey($"{vari.name}|{vari.category}"))
                    {
                        TF[vari.name] += 1;
                    }
                    else
                    {
                        TF.Add($"{vari.name}|{vari.category}",1);
                    }
                }

                foreach (var vari in TF)
                {
                    var cla = vari.Key.Split('|');
                    if (Convert.ToInt32(infoProduct(cla[1],cla[0]).count)<vari.Value)
                    {
                        return;
                    }
                }

                foreach (var vari in all)
                {
                    text += $"---{vari.name} цена {vari.price} за 1шт.\n";
                    totalPrice += Convert.ToInt32(vari.price);
                    var prod = DataBase.infoProduct(vari.category,vari.name);
                    prod.count = Convert.ToString(Convert.ToInt32(prod.count)-1);
                    DataBase.updateProduct(vari.category,prod.name,prod.price,prod.shelfLife,prod.count);
                }
                text += $"\n Итоговая цена: {totalPrice}\nДата заказа:{DateTime.Now}";
                //SendMessage(email,"Чек на заказ в программе",text);
                DataBase.addOperation(totalPrice);
                //Удаление заказа
                string text2 = "";
                using (BinaryReader reader = new BinaryReader(File.Open(CartList, FileMode.Open)))
                {
                    if (reader.BaseStream.Length==0)
                    {
                        //ig
                    }
                    else
                    {
                        text2 += reader.ReadString();
                    }
                }
                var arr =  JsonSerializer.Deserialize<List<CartClass>>(text2);
                var arr2 = new List<CartClass>();
                foreach (var vari in arr)
                {
                    if (vari.email!=email)
                    {
                        arr2.Add(vari);
                    }
                }
                var json = JsonSerializer.Serialize(arr2,arr2.GetType());
                using (BinaryWriter writer = new BinaryWriter(File.Open(CartList, FileMode.OpenOrCreate)))
                {
                    writer.Write(json);
                }
            }
        }

        private static void ordersALL() // Заказы
        {
            var all = Getorders(shope);
            var button = new string[all.Count + 1];
            var doptext = "";
            if (all.Count()>0)
            {
                var i2 = 0;
                foreach (var vari in all)
                {
                    button[i2] = $"{i2 + 1}. {vari.email}";
                    ++i2;
                }
            }
            else
            {
                doptext = "\n Заказов нет";
            }
            button[all.Count] = "-Назад";
            var but = new Button(button);
            but.text = "Заказ: "+doptext;
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
                        orders(all[but.ClickButton].products,all[but.ClickButton].email);
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }

        private static void orders(List<ProducCart> all, string email) // Заказы
        {
            var button = new string[all.Count + 2];
            var doptext = "";
            if (all.Count()>0)
            {
                var i2 = 0;
                var totalPrice = 0;
                foreach (var vari in all)
                {
                    var info = DataBase.infoProduct(vari.category,vari.name);
                    button[i2] = $"{i2 + 1}. {vari.name} | Цена: {vari.price} | На складе {info.count}";
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
                        PushOrders(all, email);
                        return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        public static void saller_menu(string shopeIN) //Меню для Продовца
        {
            shope = shopeIN;
            var but = new Button(new[] {"Заказы", "Выйти"});
            but.text = $"Продовец Магазин:{shopeIN}";
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:
                            ordersALL();
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
            salary2 += Math.Round((double)(salarySave * 0.2 / 100));
            salary2 += Math.Round((double)(salarySave * 6 / 100));
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
            var but = new Button(new[] {"Кадров","Кладовщиков","Прадовцов","-Назад"});
            but.Read_keyAsync();
            while (true)
            {
                but.WriteText();
                if (but.Click)
                {
                    switch (but.ClickButton)
                    {
                        case 0:RedInfo.upSalaryPersonnel(); break;
                        case 1: RedInfo.upSalarySaller();break;
                        case 2: RedInfo.upSalaryWarehouse();break;
                        case 5: return;
                    }

                    but.Read_keyAsync();
                }

                Thread.Sleep(500);
            }
        }
        public static void accountant_menu() //Меню для Бухгалтера
        {
            var info = getSalaryANDOperations();
            if (DateTime.Now>info.LastSa)
            {
                var all2 = getALLUser();
                var paymentAll = 0.0;
                foreach (var vari in all2)
                {
                    var payment = 0;
                    switch (vari["type_user"])
                    {
                        case "2": payment = info.SalaryPersonnel;break;
                        case "3": payment = info.SalaryWarehouse;break;
                        case "4": payment = info.SalarySaller;break;
                    }
                    paymentAll += Payroll(payment);
                }
                info.LastSa = info.LastSa.AddMonths(1);
                info.budget = info.budget-(int)paymentAll;
                setALLAccounting(info);
            }
            var but = new Button(new[] {"Статистика за день","Статистика за месяц","Статистика за полгода","Статистика за год","Изменить зарплаты","Выйти"});
            but.text = $"Бухгалтер {info.budget}р.";
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