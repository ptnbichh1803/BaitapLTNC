using System;
using System.Collections.Generic;
using System.Text;

// Sửa từ ConsoleApp1.Hinhh thành ExampleCAdvance.Hinhh
namespace ExampleCAdvance.Hinhh
{
    internal class Hinhtronn
    {
        private double banKinh;

        public double BanKinh
        {
            get
            {
                return banKinh;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Bán kính không được âm");
                }
                banKinh = value;
            }
        }

        public Hinhtronn(double banKinh)
        {
            // Kiểm tra bán kính hợp lệ qua Property
            this.BanKinh = banKinh;
        }

        public double GetDienTich()
        {
            return Math.PI * banKinh * banKinh;
        }

        public double GetChuVi()
        {
            return 2 * Math.PI * banKinh;
        }
    }
}