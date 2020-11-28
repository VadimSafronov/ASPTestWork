using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafronovText.Models;


namespace SafronovText.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        { //Вывод всех пользователей 
            DataAccessModel model = new DataAccessModel();
            ViewBag.Items = model.SelectAllPeople();
            return View();
        }

        public ActionResult Companies()
        { // Вывод всех компаний
            DataAccessModel model = new DataAccessModel();
            ViewBag.Companies = model.SelectAllCompanies();
            return View();
        }

        #region редактирование 
        [HttpGet]
        public ActionResult EditPerson(int id )
        { // Получаем работника для редактирования
            DataAccessModel model = new DataAccessModel();
            ViewBag.Positions = new SelectList(model.DropDownPositions(), "Value", "Text");
            ViewBag.Companies = new SelectList(model.DropDowmCompanies(), "Value", "Text");

            return View(model.SelectPerson(id));
        }
        [HttpPost]
        public ActionResult EditPerson(PersonModel obj)
        { // редактируем работника
           
                DataAccessModel model = new DataAccessModel();
                model.UpdatePerson(obj);
                return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCompany(int id)
        {// Получаем компанию для редактирования
            DataAccessModel model = new DataAccessModel();
            return View(model.SelectCompany(id));
        }
        [HttpPost]
        public ActionResult EditCompany(CompanyModel obj)
        { //редактируем компанию
            if (ModelState.IsValid)
            {
                DataAccessModel model = new DataAccessModel();
                model.UpdateCompany(obj);
                return RedirectToAction("Companies");
            }
            else { return View(); }
        }
        #endregion

        #region Добавление 
        public ActionResult AddCompany()
        { //переход на страницу добавления пользователя
            return View();
        }

        [HttpPost]
        public ActionResult AddCompany(CompanyModel obj)
        {
            if(ModelState.IsValid)
            {
                //Добавляем компанию
                DataAccessModel model = new DataAccessModel();
                model.AddCompany(obj);
                return RedirectToAction("Companies");
            }
            else { return View(); }
        }

        public ActionResult AddPerson()
        {
            //Переходи на страницу добавления работника
            DataAccessModel model = new DataAccessModel();
            ViewBag.Positions = new SelectList(model.DropDownPositions(), "Value", "Text");
            ViewBag.Companies = new SelectList(model.DropDowmCompanies(), "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult AddPerson(PersonModel obj)
        {
            //Добавляем работника
            DataAccessModel model = new DataAccessModel();
            model.AddPerson(obj);
            return RedirectToAction("Index");
        }

        #endregion

        #region Удаление 
        public ActionResult DeleteCompany(int id)
        {  //Удаляем компанию
            DataAccessModel model = new DataAccessModel();
            model.DeleteCompany(id);
            return RedirectToAction("Companies");
        }

        public ActionResult DeletePerson(int id )
        { //Удаляем работника
            DataAccessModel model = new DataAccessModel();
            model.DeletePerson(id);
            return RedirectToAction("Index");
        }
        #endregion   

    }
}  