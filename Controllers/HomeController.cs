using BookRental.Dal;
using BookRental.Models;
using BookRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace BookRental.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBStuff dal = new DBStuff();
            List<Cars> objCars = dal.CarsLog.ToList<Cars>();
            CarsInStock mcar = new CarsInStock();

            mcar.car = new Cars();
            mcar.cars = objCars;
            return View(mcar);
        }

        public ActionResult getCarsByJson()
        {
            DBStuff dal = new DBStuff();
            Thread.Sleep(2000);
            List<Cars> objCars = dal.CarsLog.ToList<Cars>();
            return Json(objCars , JsonRequestBehavior.AllowGet);

        }

        public ActionResult LoginCustomer()
        {
            return View();
        }

        public ActionResult getOrdersByJson()
        {
            DBStuff dal = new DBStuff();
            Thread.Sleep(2000);
            List<Login> objOrder = dal.CustomersLog.ToList<Login>();
            return Json(objOrder, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewOrderUser()
        {
            return View();
        }
        public ActionResult CustomerOrder()
        {
            DBStuff dal = new DBStuff();
            List<Login> objCars = dal.CustomersLog.ToList<Login>();
            LoginUsers logu = new LoginUsers();
            logu.user = new Login();
            logu.users = objCars;
            return View(logu);
        }
        [HttpPost]
        public ActionResult Submit(MyOrder log)
        {

            if (ModelState.IsValid)
            {
                DBStuff dal = new DBStuff();
                List<MyOrder> objOrdersI = dal.OrdersLog.ToList<MyOrder>();
                List<Login> objCars = dal.CustomersLog.ToList<Login>();
                // check if the username and the password are in the DB.
                foreach (MyOrder ob1 in objOrdersI)
                {
                    // if they are in the Db go to OrdersPage
                    if (log.Username == ob1.Username && log.Password == ob1.Password)
                    {
                        foreach (Login ob2 in objCars)
                        {
                            if (log.Password == ob2.OrderID)
                            {
                                TempData["MyOrder2"] = log.Password;
                                return RedirectToAction("CustomerOrder", TempData["MyOrder2"]);
                            }
                        }
                    }
                }
            }
            return View("LoginCustomer");
        }

        public ActionResult SelectVehicle(Login ObjOrder)
        {
            DBStuff dal = new DBStuff();
            List<Cars> objCars = dal.CarsLog.ToList<Cars>();
            CarsInStock mcar = new CarsInStock();
            TempData["Student"] = ObjOrder;
            mcar.car = new Cars();
            mcar.cars = objCars;
            return View(mcar);
        }

        public ActionResult OrderComplete()
        {
            DBStuff dal = new DBStuff();
            List<Cars> objCars = dal.CarsLog.ToList<Cars>();
            CarsInStock mcar = new CarsInStock();
            Login ObjOrder = new Login();

            if (TempData.ContainsKey("Student"))
            {
                //If so access it here
                ObjOrder = TempData["Student"] as Login;

                //find Button clicked.
                foreach (Cars ob1 in objCars)
                {                  
                    if(Request.Form[ob1.CarID] != null)
                    {
                        Login np1 =
                            (from x in dal.CustomersLog
                             where x.ID == ObjOrder.ID
                             select x).ToList<Login>()[0];

                        ObjOrder.CarSelect = ob1.CarType;
                        np1.CarSelect = ob1.CarType;
                        dal.SaveChanges();
                    }
                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult AddUser()
        {
            DBStuff dal = new DBStuff();
            MyOrder myUser = new MyOrder();
            List<MyOrder> objOrdersI = dal.OrdersLog.ToList<MyOrder>();

            // Take the values from the TextBox of the user.
            myUser.Username = Request.Form["order.Username"].ToString();
            myUser.Password = Request.Form["order.Password"].ToString();

            if (ModelState.IsValid)
            {
                // check if the username and the password are in the DB.
                foreach (MyOrder ob1 in objOrdersI)
                {
                    // if they are in the Db go to OrdersPage
                    if (myUser.Password != ob1.Password)
                    {
                        // Save in the DB
                        dal.OrdersLog.Add(myUser);
                        dal.SaveChanges();
                        TempData["User"] = myUser;
                        return RedirectToAction("NewCarOrder", TempData["User"]);
                    }
                }
            }
            return View("NewOrderUser");
        }
        public ActionResult AddOrder()
        {
            DBStuff dal = new DBStuff();
            MyOrder myUser = new MyOrder();
            List<MyOrder> objOrdersI = dal.OrdersLog.ToList<MyOrder>();

            // Take the values from the TextBox of the user.
            myUser.Username = Request.Form["order.Username1"].ToString();
            myUser.Password = Request.Form["order.Password1"].ToString();

            if (ModelState.IsValid)
            {

                // check if the username and the password are in the DB.
                foreach (MyOrder ob1 in objOrdersI)
                {
                    // if they are in the Db go to OrdersPage
                    if (myUser.Password == ob1.Password || myUser.Username == ob1.Username)
                    {
                        TempData["User"] = myUser;
                        return RedirectToAction("NewCarOrder", TempData["User"]);
                    }
                }
            }
            return View("NewOrderUser");
        }
        public ActionResult NewCarOrder()
        {
            MyOrder myUser = new MyOrder();
            if (TempData.ContainsKey("User"))
            {
                //If so access it here
                myUser = TempData["User"] as MyOrder;
            }
            TempData["SaveId"] = myUser.Password;
            LoginUsers log = new LoginUsers();
            log.user = new Login();
            log.users = new List<Login>();
            return View(log);
        }

        [HttpPost]
        public ActionResult SubmitOrder()
        {
            LoginUsers log = new LoginUsers();
            Login ObjOrder = new Login();
            ObjOrder.Visa = Request.Form["user.Visa"].ToString();
            ObjOrder.ID = Request.Form["user.OrderID2"].ToString();
            ObjOrder.OrderID = Request.Form["user.OrderID"].ToString();
            ObjOrder.StartDate = Request.Form["startDate1"].ToString();
            ObjOrder.EndDate = Request.Form["endDate1"].ToString();
            ObjOrder.CarSelect = "none";

            if (ModelState.IsValid)
            {
                DBStuff dal = new DBStuff();

                // Save in the DB
                dal.CustomersLog.Add(ObjOrder);
                dal.SaveChanges();
                return RedirectToAction("SelectVehicle", ObjOrder);
            }
            return View("NewCarOrder");
        }

        [HttpPost]
        public ActionResult UpdateOrder()
        {
            LoginUsers log = new LoginUsers();
            Login ObjOrder = new Login();
            DBStuff dal = new DBStuff();

            ObjOrder.ID = Request.Form["GetIDtext"].ToString();
            ObjOrder.StartDate = Request.Form["startDate1"].ToString();
            ObjOrder.EndDate = Request.Form["endDate1"].ToString();
            ObjOrder.CarSelect = "none";

            Login np1 =
                (from x in dal.CustomersLog
                 where x.ID == ObjOrder.ID
                 select x).ToList<Login>()[0];

            if (ModelState.IsValid)
            {
                np1.StartDate = ObjOrder.StartDate;
                np1.EndDate = ObjOrder.EndDate;
                dal.SaveChanges();

            }
            ObjOrder = np1;
            return RedirectToAction("SelectVehicle" , ObjOrder);
        }

        [HttpPost]
        public ActionResult Delete()
        {
            // Create New Objects.
            DBStuff dal = new DBStuff();
            Login myUser = new Login();
            LoginUsers logU = new LoginUsers();

            // The list of Orders from Db. 
            List<Login> objCars = dal.CustomersLog.ToList<Login>();

            // Id Customer to delete the order.
            myUser.ID = Request.Form["GetIDtext2"].ToString();

            // find the product we are looking for by ProductId.
            foreach (Login ob in objCars)
            {
                if (myUser.ID == ob.ID)
                {
                    // remove the product we want form the list.
                    dal.CustomersLog.Remove(ob);
                    dal.SaveChanges();
                }
            }
            return View("DeleteOrder");
        }
    }
}
