using static System.Net.Mime.MediaTypeNames;

namespace Castle_of_Words
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Изменение названия вкладки программы
            Console.Title = "Castle of Words"; 
            
            // Вывод текста текущей сцены
            string sceneText = "Введите путь к файлу:"; // Текст сцены
            Console.WriteLine(sceneText); // Вывод текста

            // Вывод текста справки текущей сцены
            string sceneHelpText = "\u001b[33m\n\nF1 - показать, скрыть справку;\nСтрелка влево, Backscape, Escape - вернуться назад;\nСтрелка вверх, Стрелка вниз - выбор предыдущего пути;\nEnter - подтвердить выбор.\u001b[0m"; // Текст справки
            bool sceneHelp = false; // Отоброжение справки - выключено

            string filePath = ""; // Переменная для хранения пути к файлу
            
            // Объявление координат положения каретки
            int cursorPositionX = 0; // Координата X
            int cursorPositionY = 0; // Координата
            
            
            int numHistoryPath = 0; // Номер текущего пути в истории поиска
            int maxNumHistoryPath = 10; // Максимальное число запоминаемых путей
            string[] searchHistory = new string[maxNumHistoryPath]; // Массив с путями истории поиска

            // Ожидание ввода пути к файлу
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); // Считывание нажатия клавиш
                switch (key.Key) // Определение нажатой клавиши
                {
                    case ConsoleKey.Enter: // Нажата клавиша Enter
                        break; // Завершение считывания нажатия клавиш
                    case ConsoleKey.Escape: // Нажата клавиша Escape
                        break; // Завершение считывания нажатия клавиш
                    case ConsoleKey.F1: // Нажата клавиша F1 (отображение справки)
                        sceneHelp = !sceneHelp; // Изменение состояния отображения справки
                        if (sceneHelp == true) // Если отображение справки включили
                        {
                            Console.WriteLine(sceneHelpText); // Вывод текста справки текущей сцены
                            // Перенос каретки по координатам X и Y
                            cursorPositionX = filePath.Length % Console.BufferWidth; // Расчет координаты X (остаток от деления длины пути файла к ширине консоли)
                            cursorPositionY = filePath.Length / Console.BufferWidth + 1; // Расчет координаты Y (отношение длины пути файла к ширине консоли без остатка (сколько строк в консоли занимает путь к файлу) + 1 строка)
                            Console.SetCursorPosition(cursorPositionX, cursorPositionY); // Перенос каретки
                        }
                        else // Если отображение справки отключили
                        {
                            Console.Clear(); // Очищаем консоль
                            Console.WriteLine(sceneText); // Выводим основной текст сцены
                            Console.Write(filePath); // Выводим путь к файлу (без переноса каретки)
                        }
                        continue; // Переходим к считыванию следующей клавиши
                    case ConsoleKey.UpArrow:
                        numHistoryPath--;
                        continue;
                    case ConsoleKey.DownArrow:
                        numHistoryPath++;
                        continue;
                    case ConsoleKey.LeftArrow:
                        if (filePath.Length > 0)
                        {
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                        continue;
                    case ConsoleKey.RightArrow:
                        
                        continue;
                    case ConsoleKey.Backspace:
                        // Если длина строки больше 0
                        if (filePath.Length > 0)
                        {
                            // Если позиции каретки по оси Y больше 0 и по оси X равна 0
                            if (Console.CursorTop > 0 && Console.CursorLeft == 0)
                            {
                                // Сохраняем координаты каретки
                                cursorPositionX = Console.BufferWidth - 1;
                                cursorPositionY = Console.CursorTop - 1;
                                Console.Clear(); // Очищаем консоль
                                Console.WriteLine(sceneText); // Выводим основной текст сцены
                                Console.Write(filePath); // Выводим путь к файлу (без переноса строки)
                                // Если справка включена
                                if (sceneHelp == true)
                                {
                                    Console.WriteLine(sceneHelpText); // Выводим текст справки
                                }
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY); // Перемещаем координаты каретки
                                Console.Write(" "); // Заменяем символ на пробел
                            }
                            else
                            {
                                // Перемещаем каретку на одну позицию влево
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                // Заменяем символ на пробел
                                Console.Write(" ");
                                // Перемещаем каретку на одну позицию влево
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }
                            // Удаляем символ из пути к файлу
                            filePath = filePath.Substring(0, filePath.Length - 1);
                        }
                        continue; // Переходим к считыванию следующей клавиши
                    default: // Если нажата другая клавиша
                        filePath += key.KeyChar; // Запоминаем символ нажатой клавиши
                        Console.Write(key.KeyChar); // Выводим символ нажатой клавиши
                        // Проверка на заполнение строки консоли при отображении справки
                        if (filePath.Length % Console.BufferWidth == Console.BufferWidth - 1 && sceneHelp == true)
                        {
                            Console.Clear(); // Очищаем консоль
                            Console.WriteLine(sceneText); // Выводим основной текст сцены
                            Console.WriteLine(filePath); // Выводим путь к файлу
                            Console.WriteLine(sceneHelpText); // Выводим текст справки текущей страницы
                            // Перенос каретки по координатам X и Y
                            cursorPositionX = filePath.Length % Console.BufferWidth; // Расчет координаты X (остаток от деления длины пути файла к ширине консоли)
                            cursorPositionY = filePath.Length / Console.BufferWidth + 1; // Расчет координаты Y (отношение длины пути файла к ширине консоли без остатка (сколько строк в консоли занимает путь к файлу) + 1 строка)
                            Console.SetCursorPosition(cursorPositionX, cursorPositionY); // Перенос каретки
                        }                        
                        continue; // Переходим к считыванию следующей клавиши
                }

                // Перенос положения каретки по координатам X и Y
                if (sceneHelp == true) // Если отображение справки включено
                {
                    cursorPositionY = filePath.Length / Console.BufferWidth + 7; // Расчет координаты Y (7 - число строк текста сцены и текста справки вместе)
                }
                else // Если отображение справки отключено
                {
                    cursorPositionY = filePath.Length / Console.BufferWidth + 2; // Расчет координаты Y (2 - число строк текста сцены и первой строки пути к файлу)
                }
                cursorPositionX = 0; // Координата X
                Console.SetCursorPosition(cursorPositionX, cursorPositionY); // Перенос положения каретки

                break; // Завершение считывания нажатия клавиш
            }
        }
    }
}