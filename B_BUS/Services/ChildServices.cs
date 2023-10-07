using A_DAL.Models;
using A_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_BUS.Services
{
    public class ChildServices
    {
        ChildRepos _repos = new ChildRepos();
        public ChildServices() { }
        public ChildServices(ChildRepos repos)
        {
            _repos = repos;
        }
        public List<Child> GetAllChild()
        {
            return _repos.GetAllChild().ToList();
        }
        public List<Child> GetChildByName(string name)
        {
            return _repos.GetChildByName(name).ToList();
        }
        public bool AddNewChild(string name, string age, string address, bool sex, int parentID) // truyền vào các thuộc tính
        {
            var child = new Child
            {
                Name = name,
                Age = Convert.ToInt32(age),
                Address = address,
                Sex = sex,
                ParentId = parentID

            };
            return _repos.CreateChild(child);
        }
        public bool DeleteChild(int Id) { 
            return _repos.DeleteChild(Id);  
        }
        public bool UpdateChild(int Id, string name, string age, string address,
            bool sex, int parentID)
        {
            Child child = new Child
            {
                ChildId = Id,
                Name = name,
                Age = Convert.ToInt32(age),
                Address = address,
                Sex = sex,
                ParentId = parentID
            };
            return _repos.UpdateChild(child);
        }
    }
}
