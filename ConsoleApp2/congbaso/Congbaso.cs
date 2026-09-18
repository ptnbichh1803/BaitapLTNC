using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleCAdvance.congbaso
{
    internal class Congbaso
    {
        public static void ThucHanh()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Hello, World!");

            int a, b, c;

            // Nhập số a (Sử dụng TryParse để chống lỗi nếu nhập chữ)
            while (true)
            {
                Console.Write("Nhập a: ");
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    break;
                }
                Console.WriteLine("Số bạn nhập không hợp lệ. Vui lòng nhập lại.");
            }

            // Nhập số b (Cách dùng Try-Catch theo ý bạn)
            while (true)
            {
                Console.Write("Nhập b: ");
                try
                {
                    b = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Số bạn nhập không hợp lệ. Vui lòng nhập lại.");
                }
            }

            // Nhập số c (Cách dùng TryParse theo ý bạn)
            while (true)
            {
                Console.Write("Nhập c: ");
                bool k = int.TryParse(Console.ReadLine(), out c);
                if (k)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Số bạn nhập không hợp lệ. Vui lòng nhập lại.");
                }
            }

            // In kết quả
            Console.WriteLine($"{a} + {b} + {c} = {a + b + c}");
        }
    }
}
