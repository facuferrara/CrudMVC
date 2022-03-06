using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudMVC.Models;
using CrudMVC.Models.ViewModels;
using System.Data.Entity;

namespace CrudMVC.Controllers
{
    public class CrudController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;

            using (CrudEntities1 db = new CrudEntities1())
            {
                lst = (from d in db.tabla
                       select new ListTablaViewModel
                       {
                           Id = d.Id,
                           Nombre = d.Nombre,
                           Correo = d.Correo,
                           Fecha_nacimiento = d.Fecha_nacimiento,

                       }).ToList();

            }

            return View(lst);
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if ( ModelState.IsValid)
                {
                    using (CrudEntities1 db = new CrudEntities1())
                    {
                        var oTabla = new tabla
                        {
                            Nombre = model.Nombre,
                            Correo = model.Correo,
                            Fecha_nacimiento = model.Fecha_nacimiento
                        };

                        db.tabla.Add(oTabla);
                        db.SaveChanges();
                    }
                    return Redirect("~/Crud/");
                    //return Redirect("/");

                }
                return View(model);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ActionResult Editar(int Id)
        {
            TablaViewModel model = new TablaViewModel();
            using (CrudEntities1 db = new CrudEntities1())
            {
                var oTabla = db.tabla.Find(Id);
                model.Id = oTabla.Id;
                model.Nombre = oTabla.Nombre;
                model.Correo = oTabla.Correo;
                model.Fecha_nacimiento = oTabla.Fecha_nacimiento;
                model.Id = oTabla.Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities1 db = new CrudEntities1())
                    {
                        var oTabla = db.tabla.Find(model.Id);
                        oTabla.Nombre = model.Nombre;
                        oTabla.Correo = model.Correo;
                        oTabla.Fecha_nacimiento = model.Fecha_nacimiento;

                        //db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(oTabla).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Crud/");

                }
                return View(model);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            using (CrudEntities1 db = new CrudEntities1())
            {
                var oTabla = db.tabla.Find(Id);
                db.tabla.Remove(oTabla);
                //db.Entry(oTabla).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return Redirect("~/Crud/");
        }

    }
}



