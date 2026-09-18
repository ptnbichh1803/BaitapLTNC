using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleCAdvance.Entities
{
    public class Student
    {
        // Thuộc tính private
        private string id;
        private string name;
        private DateTime birthDay;

        // Property cho Id (viết theo chuẩn C#)
        public string Id
        {
            get { return id; }
        }

        // Hoặc nếu bạn muốn giữ kiểu method get thì viết liền: getId()
        public string GetId()
        {
            return id;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime BirthDay
        {
            get { return birthDay; }
            set { birthDay = value; }
        }

        // Bổ sung 'public' cho Constructor
        public Student(string id, string name, DateTime birthDay)
        {
            // Kiểm tra cả null lẫn chuỗi rỗng/khoảng trắng
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("Id không được null hoặc rỗng");
            }

            // Gán đầy đủ các thuộc tính
            this.id = id;
            this.name = name;
            this.birthDay = birthDay;
        }
    }
}