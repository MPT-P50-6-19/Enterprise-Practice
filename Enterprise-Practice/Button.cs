using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enterprise_Practice
{
    public class Button
    {
        public List<string> button_array = new List<string>(); //Масив кнопок (string)
        public bool Click = false;
        public int ClickButton = 0;
        public string text;

        public Button(string[] mas) //Инициализация масива кнопок
        {
            foreach (var vremMas in mas)
            {
                button_array.Add(vremMas);
            }
        }

        public void up_Button(string[] mas)
        {
            button_array = new List<string>();
            foreach (var vremMas in mas)
            {
                button_array.Add(vremMas);
            }
        }
        
        public async void Read_keyAsync() //Метод для начала считывания нажатий клавишь
        {
            await Task.Run(()=>keys());
        }
        private int Check(int i) //Проверка на выход выброной кнопки за предел (когда 3 кнопки, чтобы не выбрал 4)
        {
            if (i == -1) { i = button_array.Count-1; }
            else if (i == button_array.Count) { i = 0; }
            return i;
        }

        public void WriteText()
        {
            Console.Clear();
            Console.WriteLine(text);
            for (int col = 0; col < button_array.Count; col++)
            {
                if (col==ClickButton)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(button_array[col]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine(button_array[col]);

                }
            }
        }
        private void Text(int i) //Присвоение выбранной кнопке '>' и удаление этого символа со старой
        {
            Console.Clear();
            Console.WriteLine(text);
            for (int col = 0; col < button_array.Count; col++)
            {
                if (col==i)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(button_array[col]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine(button_array[col]);

                }
            }
        }
        
        public void keys()//работа менюшки
        {
            Click = false;
            do
            {
                ConsoleKeyInfo keyPushed = Console.ReadKey();
                if (keyPushed.Key == ConsoleKey.DownArrow){ClickButton=Check(++ClickButton);}
                else if (keyPushed.Key == ConsoleKey.UpArrow){ClickButton=Check(--ClickButton);}
                else if (keyPushed.Key == ConsoleKey.Enter) { Click = true; return; }
            }while(true);
        }  
    }
}