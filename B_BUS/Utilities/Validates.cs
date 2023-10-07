using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_BUS.Utilities
{
    // Class này được dùng để validate dữ liệu
    public class Validates
    {
        // Từ khóa static là từ khóa để xác định 1 thành phần là tĩnh
        // nó có thể được gọi trực tiếp mà không cần khởi tạo đối tượng
        // Khi gọi chúng ta sử dụng trược tiếp Tên_Class.Tên_Phương_thức
        public static bool ValidateString(string str)
        {
            // Điều kiện là string không rỗng và độ dài thực tế >=6
            return str.Trim().Length >= 6;
        }
        public static bool ValidateInt(string number) {
            return Convert.ToInt32(number) > 0;
        }
    }
}
