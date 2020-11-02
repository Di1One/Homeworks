using System;
using System.Text.RegularExpressions;

namespace Project
{
    class Program
    {
        static double Factorial(int value)
		{
			if (value > 0)
			{
				var factorial = 1.0;
				for (int i = 1; i <= value; i++)
					factorial *= i;
				return factorial;
			}
			else
				return 0;
		}
		static bool Validator(string str)
        {
			//  регулярное выражение для целых и дробных чисел (так же экспоненциальная запись):
			string pattern = @"^[-+]?[0-9]*[.,]?[0-9]+(?:[eE][-+]?[0-9]+)?$";
			return Regex.IsMatch(str, pattern);
		}
		public static char OperatorValidator(char oper)
		{
			char[] op_arr = new char[6] { '+', '-', '*', '/', '^', '!' };

			foreach (var chr in op_arr)
            {
				if (oper == chr)
                {
					return oper;
                }
            }
			throw new Exception("Invalid operator input!");
		}
		static void MassageException()
        {
			Console.WriteLine("Wrong input! Try again!");
		}
		static void Main(string[] args)
        {
			char select; 
			bool temp = true;

			do
			{
				Console.Write("Enter first number : ");
				var str1 = Console.ReadLine();

				if (!Validator(str1))
				{
					MassageException();
				}
				else
				{
					str1 = str1.Replace(".", ","); // это делается только для метода .Parse()

					var num1 = double.Parse(str1);

				link1:
					char op;

					try
					{
						Console.Write("Enter operation (tip: { +, -, *, /, ^, ! }) : ");
						op = char.Parse(Console.ReadLine());
						OperatorValidator(op);
					}
					catch (Exception)
                    {
						Console.WriteLine("Invalid operator input!");
						goto link1;
						/* 
						 * ДА, да, знаю что goto не вписывается в рамки хорошей практики объектно-ориентированного программирования.
						  но так же беспонятия как обойтись здесь без него (не переписывая почти весь код)... Зато работает =)
						*/
					}

					if (op != '!')
					{
						Console.Write("Enter second number : ");
						var str2 = Console.ReadLine();

						if (!Validator(str2))
						{
							MassageException();
						}
						else
						{
							str2 = str2.Replace(".", ",");

							var num2 = double.Parse(str2);

							if (op == '+') Console.WriteLine("Sum = {0}", num1 + num2);
							else if (op == '-') Console.WriteLine("Dif = {0}", num1 - num2);
							else if (op == '/')
							{
								if (num2 == 0)
								{
									Console.WriteLine("You cannot divide by zero!");
								}
								else
                                {
									Console.WriteLine("Div = {0}", num1 / num2);
								}
							}
							else if (op == '*') Console.WriteLine("Mul = {0}", num1 * num2);
							else if (op == '^') Console.WriteLine("Pow = {0}", Math.Pow(num1, num2));
							else MassageException();
						}
					}
					else
					{
						if (Factorial(Convert.ToInt32(num1)) == 0) MassageException();
						else Console.WriteLine("{0}! = {1}", Convert.ToInt32(num1), Factorial(Convert.ToInt32(num1)));
					}
				}

				// Менюшка при вводе символа Y || y прогонит всю программу занаво
				do
				{
					Console.WriteLine("\nWant to do something else ?! [Press Y (yes) | N (no)] : ");
					select = char.Parse(Console.ReadLine());

					if (select == 'n' || select == 'N') { temp = false; }
					else if (select == 'y' || select == 'Y') 
					{ 
						temp = true;
						Console.Clear();
					}
					else 
					{
						Console.Clear();
						MassageException();
					}
				} while (select != 'n' && select != 'y' && select != 'N' && select != 'Y');
			} while (temp);
		}
    }
}