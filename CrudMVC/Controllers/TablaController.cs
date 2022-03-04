using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudMVC.Models;
using CrudMVC.Models.ViewModels;


namespace CrudMVC.Controllers
{
    public class CrudController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;

            using (CrudEntities db = new CrudEntities())
            {
                lst = (from d in db.tabla
                       select new ListTablaViewModel
                       {
                           Id = d.Id,
                           Nombre = d.Nombre,
                           Correo = d.Correo,
                           fecha_nacimiento = d.fecha_nacimiento,

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
                if ( ModelState.IsValid )
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = new tabla();
                        oTabla.Nombre = model.Nombre;
                        oTabla.Correo = model.Correo;
                        oTabla.fecha_nacimiento = model.fecha_nacimiento;

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
            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.tabla.Find(Id);
                model.Id = oTabla.Id;
                model.Nombre = oTabla.Nombre;
                model.Correo = oTabla.Correo;
                model.fecha_nacimiento = oTabla.fecha_nacimiento;
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
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = db.tabla.Find(model.Id);
                        oTabla.Nombre = model.Nombre;
                        oTabla.Correo = model.Correo;
                        oTabla.fecha_nacimiento = model.fecha_nacimiento;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
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

    }
}



