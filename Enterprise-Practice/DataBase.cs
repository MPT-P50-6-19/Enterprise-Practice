using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static Enterprise_Practice.Program;

namespace Enterprise_Practice
{
    public class Product
    {
        public string name { get; set; }

        public string price { get; set; }

        public string shelfLife { get; set; }

        public string count { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public List<Product> products { get; set; }
    }
    
    public class HistoryOperations
    {
        public int totalPrice { get; set; }
        public int data { get; set; }
    }
    public class Accounting
    {
        public DateTime LastSa { get; set; }
        public int budget { get; set; }
        public int SalaryPersonnel { get; set; }
        public int SalaryWarehouse { get; set; }
        public int SalarySaller { get; set; }
        public List<HistoryOperations> products { get; set; }
    }
    public static class DataBase //(1-Администратор, 2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия, 6-Покупатель)
        //Пользователь - тип пользователя;данные в строке раздилителем(|)
        //Категории
        //Название|продукты => Продукты: название, цена, срок годности, количество
    {
        public static List<Dictionary<string, string>> getALLUser()
        {
            string text = "";
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                text += reader.ReadString();
            }
            var json = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(text);
            return json;
        }
        
        public static void setALLUser(List<Dictionary<string, string>> users) //test
        {
            var json = JsonSerializer.Serialize(users,users.GetType());
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                writer.Write(json);
            }
        }
        
        public static List<Dictionary<string, string>> getALLDismissed()
        {
            string text = "";
            bool auth = false;
            using (BinaryReader reader = new BinaryReader(File.Open(dismissed, FileMode.Open)))
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

            if (!auth)
            {
                var json = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(text);
                return json;
            }
            else
            {
                return new List<Dictionary<string, string>>();
            }
        }
        public static void setALLDismissed(List<Dictionary<string, string>> dismissedLOG)
        {
            var json = JsonSerializer.Serialize(dismissedLOG,dismissedLOG.GetType());
            using (BinaryWriter writer = new BinaryWriter(File.Open(dismissed, FileMode.OpenOrCreate)))
            {
                writer.Write(json);
            }
        }
        public static List<Category> getALLCategory()
        {
            var json = new List<Category>();
            try
            {
                string text = "";
                using (BinaryReader reader = new BinaryReader(File.Open(path2, FileMode.Open)))
                {
                    text += reader.ReadString();
                }

                json = JsonSerializer.Deserialize<List<Category>>(text);
            }
            catch (Exception e)
            {

            }

            return json;
        }

        public static void setALLCategory(List<Category> categ)
            //Сохраняет весь журнал
        {
            var json = JsonSerializer.Serialize(categ, categ.GetType());
            using (BinaryWriter writer = new BinaryWriter(File.Open(path2, FileMode.OpenOrCreate)))
            {
                writer.Write(json);
            }
        }
        
        public static Accounting getALLAccounting()
        {
            string text = "";
            using (BinaryReader reader = new BinaryReader(File.Open(accounting, FileMode.Open)))
            {
                text += reader.ReadString();
            }
            var json = JsonSerializer.Deserialize<Accounting>(text);
            return json;
        }
        
        public static void setALLAccounting(Accounting account)
        {
            var json = JsonSerializer.Serialize(account,account.GetType());
            using (BinaryWriter writer = new BinaryWriter(File.Open(accounting, FileMode.OpenOrCreate)))
            {
                writer.Write(json);
            }
        }
        
        public static Dictionary<string, string> get_user(string login,string password) //Получить пользователя по логину и паролю
        {
            // var user = getALLUser();
            // foreach (var vari in user)
            // {
            //     if (vari["login"] ==login && vari["password"] ==password)
            //     {
            //         Dictionary<string, string> res = vari;
            //         res.Add("code","ok");
            //         return res;
            //     }
            // }
            // Dictionary<string, string> res2 = new Dictionary<string, string>();
            // res2.Add("code","error");
            // return res2;
            var res = new Dictionary<string, string>();
            res.Add("type_user","2");
            res.Add("code","ok");
            return res;
        }

        public static  Dictionary<string, string> info_user(string login) //Получить пользователя по логину
        {
            var user = getALLUser();
            foreach (var vari in user)
            {
                if (vari["login"] ==login)
                {
                    return vari;
                }
            }
            Dictionary<string, string> res2 = new Dictionary<string, string>();
            return res2;
        }

        public static List<Dictionary<string, string>> all_user(bool all=false) //Возврат всех пользователей
        {
            return getALLUser();
        }
        
        public static bool checkProductName(string category, string nameProduct)
        {
            var all = getALLCategory();
            foreach (var vari in all)
            {
                if (vari.name == category)
                {
                    foreach (var vari2 in vari.products)
                    {
                        if (vari2.name==nameProduct)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static bool checkLogin(string login)
        {
            var users = getALLUser();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i]["login"] == login)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool checkPassword(string password)
        {
            int countUpper = 0;
            int countCpec = 0;
            var pos = false;
            for (int i = 0; i < password.Length; i++)
            {
                if(char.IsUpper(password[i]) && !pos)
                {
                    countUpper++;
                    pos = true;
                }
                else if (!char.IsUpper(password[i]))
                {
                    pos = false;
                }
                if (password[i]=='!' || password[i]=='.' || password[i]=='_' || password[i]=='-' || password[i]=='(' || password[i]==')')
                {
                    countCpec++;
                }
            }
            var countINT = password.Count(n => n >= '0' && n <= '9');
            if (password.Length<8 || countUpper<3 || countCpec<2 || countINT<3)
            {
                return false;
            }

            return true;
        }
        
        public static void add_user(string login, string password,string type_user, string info) //Создать нового пользователя
        {
             if (!checkPassword(password) || !checkLogin(login))
             {
                 return;
             }
            var users = getALLUser();
            var user = new Dictionary<string, string>();
            user["type_user"] = type_user;
            user["login"] = login;
            user["password"] = password;
            user["info"] = info;
            users.Add(user);
            setALLUser(users);
        }
        
        public static void update_user(string login, string password,string type_user, string info,string shop="none") //Обновить пользовотеля
        {
            if (!checkPassword(password))
            {
                return;
            }
            var users = getALLUser();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i]["login"] == login)
                {
                    users[i]["type_user"] = type_user;
                    users[i]["password"] = password;
                    users[i]["info"] = info;
                    users[i]["shop"] = shop;
                }
            }
            setALLUser(users);
        }
        
        public static void del_user(string login) //Удалить пользователя по логину
        {
            var users = getALLUser();
            List<Dictionary<string, string>> users2 = new List<Dictionary<string, string>>();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i]["login"] != login)
                {
                    users2.Add(users[i]);
                    var des = getALLDismissed();
                    des.Add(users[i]);
                    setALLDismissed(des);
                }
            }
            setALLUser(users2);
        }

        public static List<string> getAllShop()
        {
            var all = getALLUser();
            var out1 = new List<string>();
            foreach (var vari in all)
            {
                if (vari["type_user"]=="4")
                {
                    var shop = vari["shop"];
                    if (!out1.Contains(shop))
                    {
                        out1.Add(shop);
                    }
                }
            }
            return out1;
        }
        
        
        public static List<Category> all_category() //Возврат всех категорий
        {
            return getALLCategory();
        }

        public static void addCategory(string name) //Создать категорию
        {
            var category = getALLCategory();
            var vrem = new Category();
            vrem.name = name;
            vrem.products = new List<Product>();
            category.Add(vrem);
            setALLCategory(category);
        }

        public static Category infoCategory(string name) //Получить категорию по названию
        {
            var category = getALLCategory();
            foreach (var vari in category)
            {
                if (vari.name==name)
                {
                    return vari;
                }
            }
            return new Category();
        }
        
        public static void del_category(string name) //Удалить категорию по названию
        {
            var category = getALLCategory();
            var category2 = new List<Category>();
            for (int i = 0; i < category.Count; i++)
            {
                if (category[i].name != name)
                {
                    category2.Add(category[i]);
                }
            }
            setALLCategory(category2);
        }
        public static void addProduct(string nameCategory,string nameProduct,string price,string shelfLife,string count) //Добавить товар
        {
            if (!checkProductName(nameCategory,nameProduct))
            {
                return;
            }
            var category = getALLCategory();
            for (int i = 0; i < category.Count; i++)
            {
                if (category[i].name == nameCategory)
                {
                    var product = new Product();
                    product.name = nameProduct;
                    product.price = price;
                    product.shelfLife = shelfLife;
                    product.count = count;
                    category[i].products.Add(product);
                }
            }
            setALLCategory(category);
        }
        
        public static void updateProduct(string nameCategory,string nameProduct,string price,string shelfLife,string count) //Изменить товар по категории и названию
        {
            var category = getALLCategory();
            for (int i = 0; i < category.Count; i++)
            {
                if (category[i].name == nameCategory)
                {
                    foreach (var vari in category[i].products)
                    {
                        if (vari.name==nameProduct)
                        {
                            vari.price = price;
                            vari.shelfLife = shelfLife;
                            vari.count = count;
                        }
                    }
                }
            }
            setALLCategory(category);
        }
        
        public static void delProduct(string nameCategory,string nameProduct) //Удалить товар по категории и названию
        {
            var category = getALLCategory();
            var category2= new List<Category>();
            for (int i = 0; i < category.Count; i++)
            {
                if (category[i].name != nameCategory)
                {
                    category2.Add(category[i]);
                }
                else
                {
                    var vrem = category[i];
                    var prod = new List<Product>();
                    foreach (var vari in vrem.products)
                    {
                        if (vari.name!=nameProduct)
                        {
                            prod.Add(vari);
                        }
                    }
                    vrem.products = prod;
                    category2.Add(vrem);
                }
            }
            setALLCategory(category2);
        }
        
        public static Product infoProduct(string nameCategory,string nameProduct) //Добавить товар
        {
            var category = getALLCategory();
            for (int i = 0; i < category.Count; i++)
            {
                if (category[i].name == nameCategory)
                {
                    foreach (var vari in category[i].products)
                    {
                        if (vari.name == nameProduct)
                        {
                            return vari;
                        }
                    }
                }
            }

            return new Product();
        }

        public static void addOperation(int totalPrice) //Добавить операцию
        {
            var all = getALLAccounting();
            var operation = new HistoryOperations();
            operation.totalPrice = totalPrice;
            all.budget = all.budget + totalPrice;
            operation.data = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            all.products.Add(operation);
            setALLAccounting(all);
        }
        
        public static void updateSalary(int SalaryPersonnel, int SalarySaller, int SalaryWarehouse) //Добавить операцию
        {
            var all = getALLAccounting();
            all.SalaryPersonnel = SalaryPersonnel;
            all.SalarySaller = SalarySaller;
            all.SalaryWarehouse = SalaryWarehouse;
            setALLAccounting(all);
        }

        public static Accounting getSalaryANDOperations()
        {
            return getALLAccounting();
        }
        
        
    }
}