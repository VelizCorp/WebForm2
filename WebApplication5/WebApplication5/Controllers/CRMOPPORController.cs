using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Models.ContactManager.Models;

namespace WebApplication5.Controllers
{

    public class CRMOPPORController : Controller
    {
        //Microsoft.Xrm.Sdk.EntityCollection queryResult;

        DatosCRM UtilCRM = new DatosCRM("opportunity","eq","Solicitud Wilmer",true);
        Contact[] ArregloGente = new Contact[10];

        // GET: CRM

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Index()
        {
            
            Oportunidad[] valores=new Oportunidad[UtilCRM.getColeccionOpor().Length];
            valores = UtilCRM.getColeccionOpor();
            return View(valores.ToList());

        }

        ////////////////////////////
        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            Oportunidad[] valores;
            valores = UtilCRM.getColeccionOpor();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oportunidad contact = valores[1];//db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactId,Name,Address,City,State,Zip,Email")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                //db.Contacts.Add(contact);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit1(int? id)
        {
            int item = Convert.ToInt32(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Oportunidad[] valores = new Oportunidad[UtilCRM.getColeccionOpor().Length];
            valores = UtilCRM.getColeccionOpor();
            Oportunidad contact = valores[item]; //db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "id,Tema,guid,Nombre,ApellidoPaterno,ApellidoMaterno,FechadeNacimiento,Email")] Oportunidad contact)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(contact).State = EntityState.Modified;
                //db.SaveChanges();

                // Update the account record

                if (UtilCRM.getConecto())
                {
                    UtilCRM.ActualizaOportunidad(contact);
                }
                

                return RedirectToAction("Index");
            }
            return View(contact);
        }


        // GET: Contacts/Edit/5
        public ActionResult Edit(String item)
        {
            DatosCRM UtilCRM = new DatosCRM("opportunity", "eq",item,false);
            Oportunidad[] valores = new Oportunidad[UtilCRM.getColeccionOpor().Length];
            valores = UtilCRM.getColeccionOpor();
            Oportunidad contact = valores[0]; //db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Tema,guid,Nombre,ApellidoPaterno,ApellidoMaterno,Email")] Oportunidad contact)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(contact).State = EntityState.Modified;
                //db.SaveChanges();

                // Update the account record

                if (UtilCRM.getConecto())
                {
                    UtilCRM.ActualizaOportunidad(contact);
                }
                
                return RedirectToAction("Thanks");
            }
            return View(contact);
        }

        

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = ArregloGente[1];//db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // Contact contact = db.Contacts.Find(id);
            //db.Contacts.Remove(contact);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        ////////////////////////////

        public ActionResult Thanks()
        {
            ViewBag.Message = "Actualización Exitosa!";

            return View();
        }


    } // Controlador
 }// namespace

