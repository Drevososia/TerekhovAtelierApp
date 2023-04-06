using System;
using System.Collections.Generic;
using System.Text;

namespace TerekhovAtelierApp
{
    public class Client
    {
        public int idClient { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string phoneNumber { get; set; }
    }
    public class Employees
    {
        public int idEmployee { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public int idEmpType { get; set; }
    }
    public class EmployeeTypes
    {
        public int idEmpType { get; set; }
        public int idType { get; set; }
        public string employeeTypeName { get; set; }
    }


    public class Order
    {
        public int idOrder { get; set; }
        public int idClient { get; set; }
        public int idEmployee { get; set; }
        public int idService { get; set; }
        public DateTime orderDate { get; set; }
        public int idOrderStatus { get; set; }
        public string orderDescription { get; set; }
        public clientnavigation idClientNavigation { get; set; }
        public Idemployeenavigation idEmployeeNavigation { get; set; }
        public Idorderstatusnavigation idOrderStatusNavigation { get; set; }
        public Idservicenavigation idServiceNavigation { get; set; }
    }

    public class clientnavigation
    {
        public int idClient { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string phoneNumber { get; set; }
    }

    public class Idemployeenavigation
    {
        public int idEmployee { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public int idEmpType { get; set; }
        public Idemptypenavigation idEmpTypeNavigation { get; set; }
    }

    public class Idemptypenavigation
    {
        public int idEmpType { get; set; }
        public int idType { get; set; }
        public string employeeTypeName { get; set; }
        public Idtypenavigation idTypeNavigation { get; set; }
    }

    public class Idtypenavigation
    {
        public int idType { get; set; }
        public string type { get; set; }
    }

    public class Idorderstatusnavigation
    {
        public int idOrderStatus { get; set; }
        public string orderStatus1 { get; set; }
    }

    public class Idservicenavigation
    {
        public int idService { get; set; }
        public string nameService { get; set; }
        public string serviceDescription { get; set; }
        public decimal price { get; set; }
        public int idType { get; set; }
        public Idtypenavigation1 idTypeNavigation { get; set; }
    }

    public class Idtypenavigation1
    {
        public int idType { get; set; }
        public string type { get; set; }
    }

    public class OrderStatus
    {
        public int idOrderStatus { get; set; }
        public string orderStatus1 { get; set; }
    }
    public class Services
    {
        public int idService { get; set; }
        public string nameService { get; set; }
        public string serviceDescription { get; set; }
        public decimal price { get; set; }
        public int idType { get; set; }
        public Idtypenavigation1 idTypeNavigation;
    }

    public class User
    {
        public int idUser { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }

}

