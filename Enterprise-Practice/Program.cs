using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Enterprise_Practice.DataBase;

namespace Enterprise_Practice
{
    internal class Program
    {
        public static string path = "C:/Users/79096/RiderProjects/Enterprise-Practice/Debug/baseUser.dat";
        public static string path2 = "C:/Users/79096/RiderProjects/Enterprise-Practice/Debug/baseCategory.dat";
        public static string pathBuyer = "C:/Users/79096/RiderProjects/Enterprise-Practice/Debug/Buyer.dat";
        public static string pathBuyerSave = "C:/Users/79096/RiderProjects/Enterprise-Practice/Debug/BuyerSave.dat";
        public static string CartList = "C:/Users/79096/RiderProjects/Enterprise-Practice/Debug/CartList.dat";
        public static string accounting = "C:/Users/79096/RiderProjects/Enterprise-Practice/Debug/accounting.dat";
        public static string dismissed = "C:/Users/79096/RiderProjects/Enterprise-Practice/Debug/dismissed.dat";
        
        public static void SendMessage(string to,string title,string text)
        {
            WebRequest req = WebRequest.Create($"http://cn61693.tmweb.ru/mpt/mail?to={to}&title={title}&text={text}");
            req.GetResponse();
        }
        public static void Main(string[] args)
        {
            //Админ: admin | 12345RtR!!R
            //Кадры: pers | 12345RtR!!R
            //Склад: wareh | 12345RtR!!R
            //Склад: accoun | 12345RtR!!R
            //DataBase.add_user("accoun", "12345RtR!!R", "5", "Роман|17|25");
            // var test = new Accounting();
            // test.SalaryPersonnel = 10000;
            // test.SalarySaller = 23000;
            // test.SalaryWarehouse = 5000;
            // test.budget = 100000;
            // test.LastSa = new DateTime(2020,12,3);
            // test.products = new List<HistoryOperations>();
            // test.products.Add(new HistoryOperations());
            // test.products[0].totalPrice = 300;
            // test.products[0].data = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            // DataBase.setALLAccounting(test);
            if (File.Exists(pathBuyer))
            {
                //Buyer.buyer_menu("roman.m2003@yandex.ru");
                Buyer.POLIS();
            }
            else
            {
                start();
            }
        }

        public static void start()
        {
            Console.Write("Введите логин: ");
            var login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            var key = 'g';
            var password = "";
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
                Console.WriteLine("Введите логин: "+login);
                Console.Write("Введите пароль: ");
                foreach (var VARIABLE in password)
                {
                    Console.Write("*");
                }
            }
            var user = get_user(login,password);
            if (user["code"]=="ok")
            {
                switch (Convert.ToInt32(user["type_user"]))
                {
                    case 1: Admin.admin_menu();return;
                    case 2: Personnel.personnel_menu();return;
                    case 3: Warehouse.warehouse_menu();return;
                    case 4: Seller.saller_menu(user["shope"]);return;
                    case 5: Accountant.accountant_menu();return;
                }
            }

        }
    }
}