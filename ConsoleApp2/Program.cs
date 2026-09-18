using ExampleCAdvance.congbaso;
using ExampleCAdvance.Entities;
using ExampleCAdvance.Hinhh;
using System;

namespace ExampleCAdvance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== CHƯƠNG TRÌNH CONSOLE APP 2 ===\n");

            // 1. Chạy bài toán Cộng Ba Số
            Console.WriteLine("--- 1. Thực hành Cộng Ba Số ---");
            Congbaso.ThucHanh();

            // 2. Chạy tính toán Hình Tròn
            Console.WriteLine("\n--- 2. Thực hành Hình Tròn ---");
            Hinhtronn ht = new Hinhtronn(10);
            Console.WriteLine($"Bán kính  : {ht.BanKinh}");
            Console.WriteLine($"Chu vi    : {ht.GetChuVi():F2}");
            Console.WriteLine($"Diện tích : {ht.GetDienTich():F2}");

            // 3. Chạy khởi tạo Sinh Viên
            Console.WriteLine("\n--- 3. Thực hành Class Student ---");
            try
            {
                Student st = new Student("SV01", "Nguyễn Văn A", DateTime.Now);
                Console.WriteLine($"Tạo sinh viên thành công: {st.Name} (ID: {st.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
    }
}