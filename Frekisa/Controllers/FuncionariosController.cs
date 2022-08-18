using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Frekisa.Models;

namespace Frekisa.Controllers
{
    public class FuncionariosController : Controller
    {
        private dbFrekisaEntities db = new dbFrekisaEntities();

        // GET: Funcionarios
        public ActionResult Index()
        {
            if(Session.Count != 0)
            {
                if(Session["Perfil"].ToString().ToUpper().Trim() == "TRUE")
                {
                    return View(db.Funcionarios.ToList());
                }
                else
                {
                    return RedirectToAction("Service", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            if (funcionarios == null)
            {
                return HttpNotFound();
            }
            return View(funcionarios);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            if (Session.Count != 0)
            {
                if (Session["Perfil"].ToString().ToUpper().Trim() == "TRUE")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Service", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Funcionarios/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string nome,string cpf, string login, string senha, string isGerente)
        {
            Funcionarios funcionario = new Funcionarios();

            funcionario.nome_funcionario = nome.Trim();
            funcionario.cpf_funcionario = cpf.Trim();  
            funcionario.login = login.Trim();
            funcionario.senha = senha.Trim();
            if(isGerente == "on")
            {
                funcionario.is_gerente = Convert.ToBoolean(1);
            }
            else
            {
                funcionario.is_gerente = Convert.ToBoolean(0);
            }
            db.Funcionarios.Add(funcionario);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            if (funcionarios == null)
            {
                return HttpNotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_funcionario,nome_funcionario,cpf_funcionario,login,senha,is_gerente")] Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionarios);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Funcionarios funcionarios = db.Funcionarios.Find(id);

            if (funcionarios == null)
            {
                return HttpNotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Funcionarios funcionarios = db.Funcionarios.Find(id);
                db.Funcionarios.Remove(funcionarios);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.erroDelete = "Funcionario possui contrato ativo!";

                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
