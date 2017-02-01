using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Models.ContactManager.Models;

namespace WebApplication5.Controllers
{
    public class CRMLEADController : Controller
    {
        DatosCRM UtilCRM = new DatosCRM("lead", "eq", "Todos Lead", true);
        Lead[] ArregloLead = new Lead[10];

        // GET: CRMLEAD

        /*
         * 
         * //Microsoft.Xrm.Sdk.EntityCollection queryResult;

         DatosCRM UtilCRM = new DatosCRM("opportunity","eq","Solicitud Wilmer",true);
         Contact[] ArregloGente = new Contact[10];

    // GET: CRM
    public ActionResult Index()
    {

        Oportunidad[] valores=new Oportunidad[UtilCRM.getColeccion().Length];
        valores = UtilCRM.getColeccion();
        return View(valores.ToList());

    }
         * 
         * 
         */


        public ActionResult Index()
        {
            Lead[] valores = new Lead[UtilCRM.getColeccionLead().Length];
            valores = UtilCRM.getColeccionLead();
            return View(valores.ToList());
            //return View();
        }

        // GET: Contacts/Create
        public ActionResult Crear()
        {
            return View();
        }


        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Tema,FirstName,LastName,Apellido_Materno,Celular,Email")] Lead lead)
        {

            /*
             * 
             *  public int LeadId { get; set; }

            public Guid guid { get; set; }

            public string Tema { get; set; }// subject
            public string FirstName { get; set; } // fistname
            public string LastName { get; set; }// lastname
            
            public string Apellido_Materno { get; set; }//new_apellidomaterno
            public string Celular { get; set; } //mobilephone
            public DateTime Fecha_de_Nacimiento { get; set; } // Fec
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
             * 
             * 
             * 
             */
            DatosCRM UtilCRM = new DatosCRM();

            if (ModelState.IsValid)
            {
                //db.Contacts.Add(contact);
                //db.SaveChanges();
                UtilCRM.CrearLead(lead);

                return RedirectToAction("Index");
            }

            return View(lead);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit1(int? id)
        {
            int item = Convert.ToInt32(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lead[] valores = new Lead[UtilCRM.getColeccionLead().Length];
            valores = UtilCRM.getColeccionLead();
            Lead contact = valores[item]; //db.Contacts.Find(id);
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
        public ActionResult Edit1([Bind(Include = "id,Tema,guid,FirstName,LastName,Apellido_Materno,Email")] Lead contact)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(contact).State = EntityState.Modified;
                //db.SaveChanges();

                // Update the account record

                if (UtilCRM.getConecto())
                {
                    UtilCRM.ActualizaLead(contact);
                }


                return RedirectToAction("Index");
            }
            return View(contact);
        }

    } // Controlador LEADS
}// Namespace de Controladores