using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models.ContactManager.Models;

namespace WebApplication5.Controllers
{

    /* EJEMPLO DE QUERY
     * <link-entity name='systemuser' to='owninguser'> 
         <filter type='and'> 
            <condition attribute='lastname' operator='ne' value='Cannon' /> 
          </filter> 
      </link-entity> 
     * 
     * 
     * 
     * 
     */
    public class CRMController : Controller
    {
        //Microsoft.Xrm.Sdk.EntityCollection queryResult;

        Contact[] ArregloGente;


        // GET: CRM
        public ActionResult Index(String cta)
        //public String Index()
        {
            String msg="";
            //Use the connection string named "MyCRMServer"
            //from the configuration file
            CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["MyCRMServer"].ConnectionString);


            // Verify that you are connected.
            if (crmSvc != null && crmSvc.IsReady)
            {

                string fetchXML =
                   @"<fetch version='1.0' output-format='xml-platform' 
                     mapping='logical' distinct='false' returntotalrecordcount='true' >
                    <entity name='account'>
                          <attribute name='accountid' />
                          <attribute name='name' />
                          <filter>
			                    <condition attribute='name' operator='eq' value='" + cta + @" ' />
		                  </filter>
                    </entity>
                  </fetch>";


                var queryResult = crmSvc.GetEntityDataByFetchSearchEC(fetchXML);

                //var queryResult = crmSvc.GetEntityDataByFetchSearchEC(fetchXML);
                if (queryResult != null)
                {
                    msg = "numero de cuentas: " + queryResult.TotalRecordCount + " ";
                    //gente=new Contact();
                    int i = 0;
                    ArregloGente = new Contact[queryResult.TotalRecordCount];
                    foreach (var c in queryResult.Entities)
                    {
                        
                        //msg = msg + "  " + c.Attributes["name"] + " " ;
                        Contact gente = new Contact();
                        gente.ContactId = i;
                        gente.Name = "" + c.Attributes["name"];
                        gente.Email = c.Attributes["name"] + "@gmail.com";
                        gente.Address = c.Attributes["accountid"].ToString();
                        gente.City = "Barquisimeto";
                        gente.State = "Lara";
                        gente.Zip = "3001";
                        ArregloGente[i] = gente;
                        i = i + 1;
                    }



                }
                else {
                    ArregloGente = new Contact[1];
                    Contact gente = new Contact();
                    gente.ContactId = 0;
                    gente.Name = "";
                    gente.Email = "";
                    gente.Address = "";
                    gente.City = "";
                    gente.State = "";
                    gente.Zip = "";
                    ArregloGente[0] = gente;


                    msg = "No consigio ninguna cuenta.";
                   // return View();
                }
            }
            else
            {
                msg = "Un  Error ha ocurrido ";
                ArregloGente = new Contact[1];
                Contact gente = new Contact();
                gente.ContactId = 0;
                gente.Name = "";
                gente.Email = "";
                gente.Address = "";
                gente.City = "";
                gente.State = "";
                gente.Zip = "";
                ArregloGente[0] = gente;

                //return View();
            }

            return View(ArregloGente.ToList());

        }
    } // Controlador




}// namespace
 
