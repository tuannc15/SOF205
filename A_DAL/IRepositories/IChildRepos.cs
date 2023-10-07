using A_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_DAL.IRepositories
{
    internal interface IChildRepos
    {
        // Interface là 1 bản thiết kế để làm khuôn mẫu được thực thi bởi các
        // thành phần kế thừa từ nó
        // Ví dụ: Tạo ra Interface ISinhvien có các hành động là:
        // Học, Làm quizz, Thi, đăng ký học lại,...
        // Tuy nhiên có nhiều đối tượng Sinh viên khác nhau nên khi triển khai 
        // sẽ khác nhau. VD sinh viên tạch sẽ học lười, thi cheat, làm quiz = ezquiz
        // sinh viên xuất sắc: học chăm, thi tự tin, làm quiz = nghiên cứu
        // các phương thức trong interface không có body mà chỉ có phần chữ ký 
        // chữ ký: access modifier - kiểu trả về - tên - tham số đầu vào
        // AC mặc định là public, đối với các cái khác là private
        // 1. Các phương thức Lấy dữ liệu từ DB
        IEnumerable<Child> GetAllChild(); // IEnumerable là kiểu dữ liệu dạng tập hợp
        List<Child> GetChildByName(string name);
        // 2. Phương thức tạo mới đối tượng - Luồng hoạt động như sau:
        /*
         * 1- Lấy dữ liệu từ form
         * 2- Tạo ra đối tượng từ data vừa lấy
         * 3- Thêm đối tượng đó vào danh sách 
         * 4- Sử dụng EF core để ánh xạ danh sách đó lên DB (lưu vào db)
         * 5- Thông báo
         */
        // Phương thức này sẽ thực hiện từ 3 đến 4 => trả về bool để xác định hành động
        // thêm đó có thành công hay không?
        bool CreateChild(Child child);
        // 3. Phương thức cập nhật đối tượng - Luồng hoạt động như sau:
        /*
         * 1- Xác định được đối tượng cần sửa (Lấy data từ form, Lấy đối tượng từ DB ra)
         * 2- Chỉnh sửa đối tượng theo data cần sửa
         * 3- Sử dụng EF core để ánh xạ danh sách đó lên DB (lưu vào db sau khi đã sửa)
         * 4- Thông báo
         */
        // Phương thức này sẽ là bước 2 và 3 trong luồng
        bool UpdateChild(Child child);
        // 4: Phương thức Xóa đối tượng - Luồng hoạt dộng như sau:
        /*
         * 1- Xác định được đối tượng cần xóa (Lấy data từ form, Lấy đối tượng từ DB ra)
         * 2- Xóa đối tượng đó
         * 3- Sử dụng EF core để ánh xạ danh sách đó lên DB (lưu vào db sau khi đã xóa)
         * 4- Thông báo
         */
        bool DeleteChild(int ID);    // Có thể truyền cả đối tượng vào 
        // ID chính là nằm trong đối tượng Child được truyền vào trong hàm
        // Đối tượng Child được được vào này lại được lấy từ Form
        // Form sẽ truyền qua BUS
    }
}
