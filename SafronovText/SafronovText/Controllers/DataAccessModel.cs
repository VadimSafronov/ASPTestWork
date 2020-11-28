using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SafronovText.Models;
using System.Web.Mvc;

namespace SafronovText.Controllers
{
    public class DataAccessModel
    {
        #region поля
        private DataSet DataModel;
        #endregion

        #region конструкторы
        public DataAccessModel()
        {
            //создаем датасет для представления данных из базы
            DataModel = new DataSet();
            //заполняем его с помощью адаптера
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SafronovTestString"].ConnectionString))
            {
                var da = new SqlDataAdapter("SELECT * FROM tb_Person; SELECT * FROM tb_Company; SELECT * FROM tb_Position", con);
                da.Fill(DataModel);
            }
        }

        #endregion

        #region методы
        //Методы для выбора элементов по ID

        //Выбор компании по ID
        public CompanyModel SelectCompany(int id)
        {
            CompanyModel result = new CompanyModel();

            foreach (var p in DataModel.Tables[1].AsEnumerable())
            {
                if (p.Field<int>("CompanyID") == id)
                {
                    result.CompanyID = id;
                    result.Title = p.Field<string>("Title");
                    result.CompType = p.Field<string>("CompType");

                }


            }
            return result;

        }
        //Выбор должности по ID
        public PositionModel SelectPosition(int id)
        {
            PositionModel result = new PositionModel();

            foreach (var p in DataModel.Tables[2].AsEnumerable())
            {
                if (p.Field<int>("PositionID") == id)
                {
                    result.PositionID = id;
                    result.Position = p.Field<string>("Position");
                }
            }
            return result;
        }

        //Выбор работника по Id
        public PersonModel SelectPerson(int id)
        {
            PersonModel result = new PersonModel();
            foreach (var p in DataModel.Tables[0].AsEnumerable())
            {
                if (p.Field<int>("PersonID") == id)
                {
                    result.PersonID = id;
                    result.Name = p.Field<string>("Name");
                    result.Surname = p.Field<string>("Surname");
                    result.Patronymic = p.Field<string>("Patronymic");
                    result.StartDate = p.Field<DateTime>("StartDate");
                    result.Position = SelectPosition(p.Field<int>("Position"));
                    result.Company = SelectCompany(p.Field<int>("Company"));
                   

                }
            }

            return result;

        }
        //Выбор всех компаний 
        public List<CompanyModel> SelectAllCompanies()
        {
            List<CompanyModel> result = new List<CompanyModel>();
            foreach(var p in DataModel.Tables[1].AsEnumerable())
            {
                result.Add(new CompanyModel
                {
                    CompanyID = p.Field<int>("CompanyID"),
                    Title = p.Field<string>("Title"),
                    CompType = p.Field<string>("CompType")
                });

            }
            return result;
        }
        //Выбор всех работников
        public List<PersonModel> SelectAllPeople()
        {
            List<PersonModel> result = new List<PersonModel>();
            foreach(var p in DataModel.Tables[0].AsEnumerable())
            {
                result.Add(new PersonModel { PersonID = p.Field<int>("PersonID"),
                    Name = p.Field<string>("Name"),
                    Surname = p.Field<string>("Surname"),
                    Patronymic = p.Field<string>("Patronymic"),
                    Position = SelectPosition(p.Field<int>("Position")),
                    Company = SelectCompany(p.Field<int>("Company")),
                    StartDate = p.Field<DateTime>("StartDate")

            });

            }
            return result;

        }
        //Выбор всех должностей в DropDownList
        public List<SelectListItem> DropDownPositions()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var p in DataModel.Tables[2].AsEnumerable())
            {
                result.Add(new SelectListItem()
                { Text = p.Field<string>("Position"),Value = p.Field<int>("PositionID").ToString() });
            }
            return result;
            
        }

        //Выбор всех компаний в  DropDownList

        public List<SelectListItem> DropDowmCompanies()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var p in DataModel.Tables[1].AsEnumerable())
            {
                result.Add(new SelectListItem()
                { Text = p.Field<string>("Title"), Value = p.Field<int>("CompanyID").ToString() });
            }
            return result;
        }

        //Методы для добавления новых элементов 

        //Добавить Работника

        public void AddPerson(PersonModel obj)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SafronovTestString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con; // передать команде соединение, которое уже содержит connectionstring
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pr_AddPerson";
                    cmd.Parameters.AddWithValue("@name", obj.Name);
                    cmd.Parameters.AddWithValue("@surname", obj.Surname);
                    cmd.Parameters.AddWithValue("@patronymic", obj.Patronymic);
                    cmd.Parameters.AddWithValue("@startdate", obj.StartDate);
                    cmd.Parameters.AddWithValue("@position", obj.Position.PositionID);
                    cmd.Parameters.AddWithValue("@company", obj.Company.CompanyID);
                    con.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    //catch (Exception E)
                    //{
                    //    //Response.Write(E.Message);
                    //}
                    finally
                    {
                        cmd.Connection.Close();
                    }
                }
            }

        }

        //Добавить компанию

        public void AddCompany(CompanyModel obj)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SafronovTestString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con; // передать команде соединение, которое уже содержит connectionstring
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pr_AddCompany";
                    cmd.Parameters.AddWithValue("@title", obj.Title);
                    cmd.Parameters.AddWithValue("@comptype", obj.CompType);

                    con.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    //catch (Exception E)
                    //{
                    //    //Response.Write(E.Message);
                    //}
                    finally
                    {
                        cmd.Connection.Close();
                    }

                }

            }
        }
        //Методы для удаления элементов
        //Удалить работника
        public void DeletePerson(int id )
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SafronovTestString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con; // передать команде соединение, которое уже содержит connectionstring
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pr_DeletePerson";
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    //catch (Exception E)
                    //{
                    //    //Response.Write(E.Message);
                    //}
                    finally
                    {
                        cmd.Connection.Close();
                    }

                }
            }
        }
        //Удалить компанию
        public void DeleteCompany(int id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SafronovTestString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con; // передать команде соединение, которое уже содержит connectionstring
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pr_DeleteCompany";
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    //catch (Exception E)
                    //{
                    //    //Response.Write(E.Message);
                    //}
                    finally
                    {
                        cmd.Connection.Close();
                    }
                }
            }
        }
        //Методы для обновления(редактирования ) состояния элементов

            //Обновить данные работника

             public void UpdatePerson(PersonModel obj)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SafronovTestString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "tb_UpdatePerson";
                        cmd.Parameters.AddWithValue("@id", obj.PersonID);
                        cmd.Parameters.AddWithValue("@name", obj.Name);
                        cmd.Parameters.AddWithValue("@surname", obj.Surname);
                        cmd.Parameters.AddWithValue("@patronymic", obj.Patronymic);
                        cmd.Parameters.AddWithValue("@startdate", obj.StartDate);
                        cmd.Parameters.AddWithValue("@position", obj.Position.PositionID);
                        cmd.Parameters.AddWithValue("@company", obj.Company.CompanyID);
                        con.Open();

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        //catch (Exception E)
                        //{
                        //    //Response.Write(E.Message);
                        //}
                        finally
                        {
                            cmd.Connection.Close();
                        }

                    }
                }
            }
               //Обновить данные компании
            public void UpdateCompany (CompanyModel obj)
          {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SafronovTestString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "tb_UpdateCompany";
                    cmd.Parameters.AddWithValue("@id", obj.CompanyID);
                    cmd.Parameters.AddWithValue("@title", obj.Title);
                    cmd.Parameters.AddWithValue("@comptype", obj.CompType);

                    con.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    //catch (Exception E)
                    //{
                    //    //Response.Write(E.Message);
                    //}
                    finally
                    {
                        cmd.Connection.Close();
                    }


                }

            }

           }

        #endregion

    }

}