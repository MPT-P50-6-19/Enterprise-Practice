using System;
using  static Enterprise_Practice.DataBase;

namespace Enterprise_Practice
{
    public class RedInfo
    {
        private static string combine(string[] mas)
        {
            var text = "";
            foreach (var vari in mas)
            {
                text += $"{vari}|";
            }
            return text.Substring(0,text.Length-1);
        }
        
        
        public static void upSalaryPersonnel()
        {
            Console.Clear();
            Console.Write("Введите зарплату Кадров: ");
            var SalaryPersonnel =  Convert.ToInt32(Console.ReadLine());
            var inf = getSalaryANDOperations();
            DataBase.updateSalary(SalaryPersonnel,inf.SalarySaller,inf.SalaryWarehouse);
        }
        
        public static void upSalarySaller()
        {
            Console.Clear();
            Console.Write("Введите зарплату Кладовщиков: ");
            var SalarySaller =  Convert.ToInt32(Console.ReadLine());
            var inf = getSalaryANDOperations();
            DataBase.updateSalary(inf.SalaryPersonnel,SalarySaller,inf.SalaryWarehouse);
        }
        
        public static void upSalaryWarehouse()
        {
            Console.Clear();
            Console.Write("Введите зарплату Прадовцов: ");
            var SalaryWarehouse =  Convert.ToInt32(Console.ReadLine());
            var inf = getSalaryANDOperations();
            DataBase.updateSalary(inf.SalaryPersonnel,inf.SalarySaller,SalaryWarehouse);
        }

        public static void upPassword(string login)
        {
            Console.Clear();
            Console.Write("Введите пароль: ");
            var password = Console.ReadLine();
            while (!DataBase.checkPassword(password))
            {
                Console.Clear();
                Console.Write("Введите пароль: ");
                password = Console.ReadLine();
            }
            var inf = info_user(login);
            DataBase.update_user(login, password, inf["type_user"], inf["info"]);
        }

        public static void upTypeUser(string login)
        {
            Console.Write("Введите тип пользователя(2-Кадры, 3-Склад, 4-Кассир, 5-Бухгалтерия): ");
            var type_user = Console.ReadLine();
            var inf = info_user(login);
            DataBase.update_user(login, inf["password"], type_user, inf["info"]);
        }

        public static void upFio(string login)
        {
            Console.Write("Введите ФИО: ");
            var fio = Console.ReadLine();
            var inf = info_user(login);
            var info2 = inf["info"].Split('|');
            info2[0] = fio;
            DataBase.update_user(login, inf["password"], inf["type_user"], combine(info2));
        }
        
        public static void upAge(string login)
        {
            Console.Write("Введите возраст: ");
            var age = Console.ReadLine();
            while (Convert.ToInt32(age)<1960 || Convert.ToInt32(age)>2002)
            {
                Console.Clear();
                Console.Write("Введите возраст: ");
                age = Console.ReadLine();
            }
            var inf = info_user(login);
            var info2 = inf["info"].Split('|');
            info2[1] = age;
            DataBase.update_user(login, inf["password"], inf["type_user"], combine(info2));
        }
        
        public static void upEducation(string login)
        {
            Console.Write("Введите образование: ");
            var education = Console.ReadLine();
            var inf = info_user(login);
            var info2 = inf["info"].Split('|');
            info2[2] = education;
            DataBase.update_user(login, inf["password"], inf["type_user"], combine(info2));
        }
        
        public static void upWorkExperience(string login)
        {
            var inf = info_user(login);
            var info2 = inf["info"].Split('|');
            Console.Write("Введите опыт работы: ");
            var work_experience = Console.ReadLine();
            while (Convert.ToInt32(info2[1])-16<Convert.ToInt32(work_experience))
            {
                Console.Write("Введите опыт работы: ");
                work_experience = Console.ReadLine();
            }
            info2[3] = work_experience;
            DataBase.update_user(login, inf["password"], inf["type_user"], combine(info2));
        }
        
        public static void upShop(string login)
        {
            Console.Write("Введите магазин: ");
            var shop = Console.ReadLine();
            var inf = info_user(login);
            DataBase.update_user(login, inf["password"], inf["type_user"], inf["info"],shop);
        }

        public static void upPrice(string category,string nameProduct)
        {
            Console.Clear();
            Console.Write("Цена: ");
            var price = Console.ReadLine();
            var inf = infoProduct(category,nameProduct);
            DataBase.updateProduct(category,nameProduct,price,inf.shelfLife,inf.count);
        }
        
        public static void upShelfLife(string category,string nameProduct)
        {
            Console.Clear();
            Console.Write("Срок годности в днях: ");
            var shelfLife = Convert.ToInt32(Console.ReadLine());
            var time = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + shelfLife*24*60*60;
            var inf = infoProduct(category,nameProduct);
            DataBase.updateProduct(category,nameProduct,inf.price,Convert.ToString(time),inf.count);
        }

        public static void upCount(string category, string nameProduct)
        {
            Console.Clear();
            Console.Write("Количество: ");
            var count = Console.ReadLine();
            var inf = infoProduct(category,nameProduct);
            DataBase.updateProduct(category,nameProduct,inf.price,inf.shelfLife,count);
        }
    }
}