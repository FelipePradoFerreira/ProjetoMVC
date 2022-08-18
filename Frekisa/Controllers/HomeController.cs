using Frekisa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frekisa.Controllers
{
    public class HomeController : Controller
    {

        private dbFrekisaEntities db = new dbFrekisaEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id_cliente,nome_cliente,cpf_cliente,telefone_cliente,email_cliente,renda_mensal,endereco")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientes);
        }

        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Service()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(string login, string senha)
        {
            if(login == null || login == "")
            {
                ViewBag.erroLogin = "Erro no Login";
            }
            if(senha == null || senha == "")
            {
                ViewBag.erroSenha = "Erro na Senha";
            }

            var vLogin = db.Funcionarios.Where(p => p.login.Trim().Equals(login.Trim())).FirstOrDefault();
            if(vLogin != null)
            {
                if(Equals(vLogin.senha.Trim(), senha.Trim()))
                {
                    Session["Nome"] = vLogin.nome_funcionario;
                    Session["Perfil"] = vLogin.is_gerente;
                    Session["ID"] = vLogin.id_funcionario;

                    return RedirectToAction("Service", "Home");

                }
                else
                {
                    ViewBag.erroSenha = "Senha inválida";

                    return View();
                }
            }
            else
            {
                ViewBag.erroLogin = "Nome de usuário inválido!";

                return View();
            }
        }
    }
}