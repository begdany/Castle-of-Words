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
            int cursorPositionY = 0; // Координата Y
            int cursorPositionZ = 0; // Координата каретки в строке
            int lastCharPositionX = 0;
            int lastCharPositionY = 0;
            
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
                        // Сохраняем текущие координаты каретки
                        cursorPositionX = Console.CursorLeft;
                        cursorPositionY = Console.CursorTop;
                        if (cursorPositionZ != filePath.Length)
                        {


                            //ПРОДУМАТЬ КАК ОБОЙТИ ПОСТОЯННЫЙ РАСЧЕТ
                            // Сохраняем текущие координаты каретки
                            lastCharPositionX = filePath.Length % Console.BufferWidth;
                            lastCharPositionY = filePath.Length / Console.BufferWidth + 1; ////// Объяснить 1!!!!!!!!!!!!!!!!!!


                            Console.SetCursorPosition(lastCharPositionX, lastCharPositionY); // Перенос каретки
                        }
                        if (sceneHelp == true) // Если отображение справки включили
                        {
                            Console.WriteLine(sceneHelpText); // Вывод текста справки текущей сцены
                        }
                        else // Если отображение справки отключили
                        {
                            for (int i = 0; i < 5; i++) Console.WriteLine(new string(' ', Console.BufferWidth)); // ПОЧЕМУ 5???? Как найти число строк в справке????
                        }
                        // Перенос каретки по координатам X и Y
                        Console.SetCursorPosition(cursorPositionX, cursorPositionY); // Перенос каретки
                        continue; // Переходим к считыванию следующей клавиши
                    case ConsoleKey.UpArrow:
                        // Если каретка находится не на первой строке
                        if (Console.CursorTop != 1)
                        {
                            // Переносим каретку на одну строку выше
                            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                            // Сохраняем новое положение каретки в строке
                            cursorPositionZ -= Console.BufferWidth;
                        }
                        continue;
                    case ConsoleKey.DownArrow:
                        // Если каретка находится не на последней строке
                        if (Console.CursorTop != filePath.Length / Console.BufferWidth + 1) // Убрать деление в будущем, могут быть проблемы, если заголовок состоит из более чем 1 строки 
                        {
                            // Если каретка находится не на предпоследней строке
                            if (Console.CursorTop != filePath.Length / Console.BufferWidth)
                            {
                                // Переносим каретку на одну строку ниже
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
                                // Сохраняем новое положение каретки в строке
                                cursorPositionZ += Console.BufferWidth;
                            }
                            // Если каретка находится на предпоследней строке
                            else
                            {
                                // Запоминаем координату последнего символа по оси X 
                                lastCharPositionX = filePath.Length % Console.BufferWidth; // Убрать деление в будущем
                                // Если каретка находится дальше последнего символа по оси X
                                if (Console.CursorLeft > lastCharPositionX)
                                {
                                    // Переносим каретку на одну строку ниже
                                    Console.SetCursorPosition(lastCharPositionX, Console.CursorTop + 1);
                                    // Сохраняем новое положение каретки в строке
                                    cursorPositionZ = filePath.Length;
                                }
                                // Если каретка находится ближе последнего символа по оси X
                                else
                                {
                                    // Переносим каретку на одну строку ниже
                                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
                                    // Сохраняем новое положение каретки в строке
                                    cursorPositionZ += Console.BufferWidth;
                                }
                            }
                        }
                        continue;
                    case ConsoleKey.LeftArrow:
                        if (cursorPositionZ > 0)
                        {
                            if (Console.CursorTop > 0 && Console.CursorLeft == 0) /////////////////////// Нужно обосновать первый 0
                            {
                                Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
                            }
                            else
                            {
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }
                            cursorPositionZ--; // Уменьшаем позицию каретки в строке на 1
                        }
                        continue;
                    case ConsoleKey.RightArrow:
                        if (cursorPositionZ < filePath.Length)
                        {
                            if (Console.CursorLeft == Console.BufferWidth - 1)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop + 1);
                            }
                            else
                            {
                                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                            }
                            cursorPositionZ++; // Увеличиваем позицию каретки в строке на 1
                        }
                        continue;
                    case ConsoleKey.Backspace:
                        // Если длина строки больше 0
                        if (cursorPositionZ != 0)
                        {
                            // Если каретка находится внутри строки
                            if (cursorPositionZ != filePath.Length)
                            {
                                // Уменьшаем позицию каретки в строке на 1
                                cursorPositionZ--;
                                // Удаляем из строки символ перед кареткой
                                filePath = filePath.Substring(0, cursorPositionZ) + filePath.Substring(cursorPositionZ + 1);
                                // Если каретка находится не у левой границы окна
                                if (Console.CursorLeft == 0)
                                {
                                    // Запоминаем координату предыдущего символа
                                    cursorPositionX = Console.BufferWidth - 1; // Координата X
                                    cursorPositionY = Console.CursorTop - 1; // Координата Y
                                }
                                // Если каретка находится внутри окна
                                else
                                {
                                    // Запоминаем координату предыдущего символа
                                    cursorPositionX = Console.CursorLeft - 1; // Координата X
                                    cursorPositionY = Console.CursorTop; // Координата Y
                                }
                                // Переносим каретку
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                // Выводим правую часть строки
                                Console.Write(filePath[^(filePath.Length - (cursorPositionZ))..]);
                                // 
                                Console.Write(' ');
                                // Если отображение справки включено
                                if (sceneHelp == true)
                                {
                                    // Если последний символ строки перешел на предыдущую строчку окна
                                    if (Console.CursorLeft == 1)
                                    {
                                        // Смещаем положение справки вверх на одну строчку:
                                        // Запоминаем номер строки окна на которой находится курсор
                                        lastCharPositionY = Console.CursorTop;
                                        // Переносим каретку к началу справки
                                        Console.SetCursorPosition(0, lastCharPositionY + 2);
                                        // Выводим пустые строки, чтобы стереть текст справки
                                        for (int i = 0; i < 4; i++) Console.WriteLine(new string(' ', Console.BufferWidth)); // ПОЧЕМУ 4???? Как найти число строк в справке???? Можно будет заменить на foreach
                                        // Переносим каретку к последней строке
                                        Console.SetCursorPosition(0, lastCharPositionY - 1);
                                        // Выводим текст справки
                                        Console.Write(sceneHelpText);
                                    }
                                }
                                // Возвращаем каретку
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                            }
                            // Если каретка находится в конце строки
                            else
                            {
                                // Если каретка находится внутри окна
                                if (Console.CursorLeft != 0)
                                {
                                    // Удаляем последний символ:
                                    // Запоминаем новые координаты каретки
                                    cursorPositionX = Console.CursorLeft - 1; // Координата X
                                    cursorPositionY = Console.CursorTop; // Координата Y
                                    // Переносим каретку по новым координатам
                                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                    // Выводим пробел
                                    Console.Write(" ");
                                }
                                // Если каретка находится у правой границы окна
                                else
                                {
                                    // Удаляем последний символ и переносим каретку в конец предыдущей строки:
                                    // Запоминаем новые координаты каретки
                                    cursorPositionX = Console.BufferWidth - 1; // Координата X
                                    cursorPositionY = Console.CursorTop - 1; // Координата Y
                                    // Переносим каретку по новым координатам
                                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                    // Выводим пробел
                                    Console.WriteLine(" ");
                                    // Если отображение справки включено
                                    if (sceneHelp == true)
                                    {
                                        Console.SetCursorPosition(0, cursorPositionY + 3);
                                        // Выводим пустые строки, чтобы стереть текст справки
                                        for (int i = 0; i < 4; i++) Console.WriteLine(new string(' ', Console.BufferWidth)); // ПОЧЕМУ 4???? Как найти число строк в справке????
                                        // Возвращаем положение каретки
                                        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                        // Выводим текст справки
                                        Console.Write(sceneHelpText);
                                    }
                                }
                                // Удаляем символ из пути к файлу
                                filePath = filePath.Substring(0, filePath.Length - 1);
                                // Уменьшаем позицию каретки в строке на 1
                                cursorPositionZ--;
                                // Переносим каретку по новым координатам
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                            }
                        }
                        continue; // Переходим к считыванию следующей клавиши
                    default: // Если нажата другая клавиша
                        // Если каретка находится в конце строки
                        if (cursorPositionZ == filePath.Length)
                        {
                            // Добавляем к строке символ нажатой клавиши
                            filePath += key.KeyChar;
                            // Если каретка находится у правой границы окна
                            if (Console.CursorLeft == Console.BufferWidth - 1)
                            {
                                // Выводим символ нажатой клавиши и переносим каретку на следующую строку
                                Console.WriteLine(key.KeyChar);
                                // Если отображение справки включено
                                if (sceneHelp == true)
                                {
                                    // Смещаем положение справки вниз на одну строчку:
                                    // Запоминаем текущие координаты каретки
                                    cursorPositionX = 0; // Координата X
                                    cursorPositionY = Console.CursorTop; // Координата Y
                                    // Выводим пустые строки, чтобы стереть текст справки
                                    for (int i = 1; i < 5; i++) Console.WriteLine(new string(' ', Console.BufferWidth)); // ПОЧЕМУ 5???? Как найти число строк в справке????
                                    // Возвращаем положение каретки
                                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                    // Выводим текст справки
                                    Console.Write(sceneHelpText);
                                    // Возвращаем положение каретки
                                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                }
                            }
                            // Если каретка находится внутри окна
                            else
                            {
                                // Выводим символ нажатой клавиши
                                Console.Write(key.KeyChar);
                            }
                        }
                        // Если каретка находится внутри строки
                        else
                        {
                            // Вставляем в строку символ нажатой клавиши (по текущей позиции каретки)
                            filePath = filePath.Insert(cursorPositionZ, key.KeyChar.ToString());
                            // Если каретка находится у правой границы окна
                            if (Console.CursorLeft == Console.BufferWidth - 1)
                            {
                                // Выводим символ нажатой клавиши и переносим каретку на следующую строку
                                Console.WriteLine(key.KeyChar);
                                // Запоминаем текущую координату X каретки
                                cursorPositionX = 0;
                            }
                            // Если каретка находится внутри окна
                            else
                            {
                                // Выводим символ нажатой клавиши
                                Console.Write(key.KeyChar);
                                // Запоминаем текущую координату X каретки
                                cursorPositionX = Console.CursorLeft;
                            }
                            // Запоминаем текущую координату Y каретки
                            cursorPositionY = Console.CursorTop;
                            // Смещаем положение правой части строки
                            Console.Write(filePath[^(filePath.Length - (cursorPositionZ + 1))..]);
                            // Если отображение справки включено
                            if (sceneHelp == true)
                            {
                                // Если последний символ строки перешел на новую строку
                                if (filePath.Length % Console.BufferWidth == 1)
                                {
                                    // Смещаем положение справки вниз на одну строчку:
                                    // Запоминаем в какой по счету строке расположен последний символ строки
                                    lastCharPositionY = filePath.Length / Console.BufferWidth + 1; ////// Объяснить 1!!!!!!!!!!!!!!!!!!
                                    // Переносим каретку на следующую строку от последнего символа
                                    Console.SetCursorPosition(0, lastCharPositionY + 1);
                                    // Выводим пустые строки, чтобы стереть текст справки
                                    for (int i = 0; i < 4; i++) Console.WriteLine(new string(' ', Console.BufferWidth)); // ПОЧЕМУ 4???? Как найти число строк в справке????
                                    // Переносим каретку к последнему символу строки
                                    Console.SetCursorPosition(1, lastCharPositionY);
                                    // Выводим текст справки
                                    Console.Write(sceneHelpText);
                                }
                            }
                            // Возвращаем положение каретки в исходное состояние
                            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                        }
                        // Увеличиваем положение каретки на 1
                        cursorPositionZ++;
                        // Переходим к считыванию следующей клавиши
                        continue; 
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