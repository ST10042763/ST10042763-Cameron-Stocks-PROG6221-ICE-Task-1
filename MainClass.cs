using System;

namespace ICE_TASK_1 {
    internal class MainClass {
        public static void Main() {
            int nLec, nScripts, nQuestions;

            bool validScripts = false;
            do {
                nScripts = GetInt("Enter Number of Scripts");
                if (nScripts == 0) {
                    Console.WriteLine("Can't have 0 scripts");
                } else {
                    validScripts = true;
                }
            } while (!validScripts);

            bool validQuestions;
            do {
                nQuestions = GetInt("Enter number of questions in this paper");
                validQuestions = true;
                if (nQuestions == 0 || nQuestions > 10) {
                    Console.WriteLine("Please enter a valid number of questions!");
                    validQuestions = false;
                }
            } while (!validQuestions);

            int[] subQuestions = new int[nQuestions];
            for (int i = 0;  i < nQuestions; i++) {
                subQuestions[i] = GetInt($"Enter sub total for question {i + 1}");
            }

            bool validAmount = false;
            bool usingLast = false;
            int nDiv = default, leftover = default;
            do {
                nLec = GetInt("Enter number of Lecturers");
                if (nLec == 0) {
                    Console.WriteLine("Can't have 0 Lecturers");
                    continue;
                }

                nDiv = Convert.ToInt32(Math.Floor((double)(nScripts / nLec)));

                if (nDiv * nLec != nScripts) {
                    leftover = nDiv + (nScripts - (nDiv * nLec));
                    if (leftover > 50) {
                        Console.WriteLine("There were not enough lectures allocated. Please allocate more lecturers");
                        continue;
                    }
                    usingLast = true;
                }


                if (nScripts / nLec - 1 > 50 || nScripts / nLec > 50) {
                    Console.WriteLine("There were not enough lectures allocated. Please allocate more lecturers");
                    validAmount = false;
                } else {
                    validAmount = true;
                }

            } while (!validAmount);

            if (usingLast) {
                Console.WriteLine($"The first {nLec - 1} Lecturers will mark {nDiv} Scripts");
                Console.WriteLine($"The last Lecturer will mark {leftover} Scripts");
            } else {
                Console.WriteLine($"Each Lecturer will mark {nDiv} scripts");
            }

            int hours = 0, minutes = 0, seconds = 0;
            foreach (int i in subQuestions) {
                for (int j = 0; j < i; j++) {
                    seconds += 2;
                    if (seconds == 60) {
                        seconds = 0;
                        minutes++;
                    }
                    if (minutes == 60) {
                        minutes = 0;
                        hours++;
                    }
                }
            }
            if (seconds >= 30) {
                minutes++;
            }

            int totalMinutes = minutes * nDiv;

            bool minutes_less_60 = false;
            do {
                if (totalMinutes > 60) {
                    hours++;
                    totalMinutes -= 60;
                } else {
                    minutes_less_60 = true;
                }
            } while (!minutes_less_60);

            if (!usingLast) {
                Console.WriteLine($"Each lecturer will take {hours} hours and {totalMinutes} minutes to mark all {nDiv} Scripts");
            } else {
                Console.WriteLine($"The first {nLec - 1} lecturers will take {hours} hours and {totalMinutes} minutes to mark {nDiv} scripts");
                minutes_less_60 = false;
                totalMinutes = minutes * leftover;
                hours = 0;
                do {
                    if (totalMinutes > 60) {
                        hours++;
                        totalMinutes -= 60;
                    } else {
                        minutes_less_60 = true;
                    }
                } while (!minutes_less_60);
                Console.WriteLine($"It will take the last lecturer {hours} hours and {totalMinutes} minutes to mark {leftover} scripts");
            }

            Console.ReadKey();
        }
        
        static int GetInt(string prompt) {
            int n = default;
            bool valid = false;
            do {
                Console.Write($"{prompt}: ");
                try {
                    n = Convert.ToInt32(Console.ReadLine());
                    valid = true;
                } catch (FormatException) {
                    Console.WriteLine("Enter a Valid Number");
                } catch (OverflowException) {
                    Console.WriteLine("Please enter a realistic number");
                }
            } while (!valid);
            return n;
        }
    }
}