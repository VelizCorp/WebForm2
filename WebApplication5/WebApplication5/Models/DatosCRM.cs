using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication5.Models.ContactManager.Models;

namespace WebApplication5.Models
{
    

    public class DatosCRM
    {
        // Atributos
        
        private  CrmServiceClient crmSvc;
        private Oportunidad[] ArregloGente;
        private Lead[] ArregloLead;
        private Boolean conecto = false;
        String fetchXML;
        String fetchXML1;




        public DatosCRM(String Entidad, String Operador,String Nombre,Boolean Todos)
        {  
            // Conecta al CRM 
            crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["MyCRMServer2"].ConnectionString);

            ArregloGente = new Oportunidad[10];
            ArregloLead = new Lead[10];

            Consulta(Entidad,Operador,Nombre,Todos);
            ConsultaLead(Entidad, Operador, Nombre, Todos);
        }

        public DatosCRM()
        {
            crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["MyCRMServer2"].ConnectionString);

        }

        public Oportunidad[]  getColeccionOpor()
        {
            return ArregloGente;
        }


         public Lead[] getColeccionLead()
        {
            return ArregloLead;
        }
        public Boolean getConecto()
        {
            return conecto;
        }

        public void CrearLead(Lead lead)
        { 

        // Create an account record
        Dictionary<string, CrmDataTypeWrapper> inData = new Dictionary<string, CrmDataTypeWrapper>();

        inData.Add("subject", new CrmDataTypeWrapper(lead.Tema, CrmFieldType.String));

        inData.Add("firstname", new CrmDataTypeWrapper(lead.FirstName, CrmFieldType.String));

        inData.Add("lastname", new CrmDataTypeWrapper(lead.LastName, CrmFieldType.String));

        inData.Add("new_apellidomaterno", new CrmDataTypeWrapper(lead.Apellido_Materno, CrmFieldType.String));

        //inData.Add("new_fechadenacimiento", new CrmDataTypeWrapper(lead.Fecha_de_Nacimiento, CrmFieldType.CrmDateTime));

        inData.Add("mobilephone", new CrmDataTypeWrapper(lead.Celular, CrmFieldType.String));

        inData.Add("emailaddress1", new CrmDataTypeWrapper(lead.Email, CrmFieldType.String));

          Guid ResultLead= crmSvc.CreateNewRecord("lead", inData);

            if (ResultLead == null)
            {
            }
            else
            {
            }
        }

        public void ActualizaOportunidad(Oportunidad persona)
        {
            Guid guid = new Guid();

            Dictionary<string, CrmDataTypeWrapper> updateData = new Dictionary<string, CrmDataTypeWrapper>();

            //updateData.Add("new_name", new CrmDataTypeWrapper(persona.Tema, CrmFieldType.String));
            updateData.Add("new_firstname", new CrmDataTypeWrapper(persona.Nombre, CrmFieldType.String));
            updateData.Add("new_lastname", new CrmDataTypeWrapper(persona.ApellidoPaterno, CrmFieldType.String));
            updateData.Add("new_apellidomaterno", new CrmDataTypeWrapper(persona.ApellidoMaterno, CrmFieldType.String));
            updateData.Add("new_emailaddress1", new CrmDataTypeWrapper(persona.Email, CrmFieldType.String));
            updateData.Add("new_fechadenacimiento", new CrmDataTypeWrapper(persona.FechadeNacimiento, CrmFieldType.CrmDateTime));

            guid = persona.guid;
       
            bool updateAccountStatus = crmSvc.UpdateEntity("opportunity", "opportunityid", guid, updateData);

        }


        public void ActualizaLead(Lead persona)
        {
            Guid guid = new Guid();

            Dictionary<string, CrmDataTypeWrapper> updateData = new Dictionary<string, CrmDataTypeWrapper>();

            //updateData.Add("new_name", new CrmDataTypeWrapper(persona.Tema, CrmFieldType.String));
            updateData.Add("firstname", new CrmDataTypeWrapper(persona.FirstName, CrmFieldType.String));
            updateData.Add("lastname", new CrmDataTypeWrapper(persona.LastName, CrmFieldType.String));
            updateData.Add("new_apellidomaterno", new CrmDataTypeWrapper(persona.Apellido_Materno, CrmFieldType.String));
            updateData.Add("emailaddress1", new CrmDataTypeWrapper(persona.Email, CrmFieldType.String));

            guid = persona.guid;

            bool updateAccountStatus = crmSvc.UpdateEntity("lead", "leadid", guid, updateData);

        }

        public void Consulta(String Entidad, String Operador, String Nombre, Boolean Todos)
        {
            //String fetchXML;
            if (crmSvc != null && crmSvc.IsReady)
            {

                if (Todos)
                {
                    fetchXML =
                       @"<fetch version='1.0' output-format='xml-platform' 
                     mapping='logical' distinct='false' returntotalrecordcount='true' >
                    <entity name='" + Entidad + @"'>
                          <attribute name='opportunityid' /> <attribute name='name' /> <attribute name='new_firstname' /> 
                          <attribute name='new_lastname' />  <attribute name='new_emailaddress1' /> <attribute name='new_apellidomaterno' />
                          <attribute name='new_fechadenacimiento' />
                          <link-entity name='systemuser' to='owninguser'> 
                          <filter type='and'> 
                              <condition attribute='lastname' operator='eq' value='Veliz' /> 
                              </filter> 
                          </link-entity> 
                    </entity>
                  </fetch>";
                } else

                {
                     fetchXML =
                       @"<fetch version='1.0' output-format='xml-platform' 
                     mapping='logical' distinct='false' returntotalrecordcount='true' >
                    <entity name='" + Entidad + @"'>
                          <attribute name='opportunityid' /> <attribute name='name' /> <attribute name='new_firstname' /> 
                          <attribute name='new_lastname' />  <attribute name='new_emailaddress1' /> <attribute name='new_apellidomaterno' />
                          <attribute name='new_fechadenacimiento' />
                          <filter>
			                    <condition attribute='new_emailaddress1' operator='" + Operador + @"' value='" + Nombre + @" ' />
		                  </filter>
                    </entity>
                  </fetch>";
                }


                var queryResult = crmSvc.GetEntityDataByFetchSearchEC(fetchXML);


            if (queryResult != null)
            {

                int i = 0;
                ArregloGente = new Oportunidad[queryResult.TotalRecordCount];
                foreach (var c in queryResult.Entities)
                {
                    //guid = c.Id;
                    //msg = msg + "  " + c.Attributes["name"] + " " ;
                    Oportunidad gente      = new Oportunidad();
                    gente.OportunidadId    = i;
                    gente.guid             = c.Id;
                    gente.Tema             = "" + c.Attributes["name"];
                    gente.Nombre           = "" + c.Attributes["new_firstname"];
                    gente.ApellidoPaterno  = "" + c.Attributes["new_lastname"];
                    gente.ApellidoMaterno  = "" + c.Attributes["new_apellidomaterno"];
                    //gente.FechadeNacimiento = (DateTime)c.Attributes["new_fechadenacimiento"];
                    gente.Email            = "" +c.Attributes["new_emailaddress1"];
                    gente.id               = c.Attributes["opportunityid"].ToString();
                    ArregloGente[i]        = gente;
                    i = i + 1;
                }// ForEach
                    conecto = true;
            }// Query Result
            else
            {
                ArregloGente = new Oportunidad[1];
                Oportunidad gente = new Oportunidad();
                gente.OportunidadId = 0;
                gente.Tema = "";
                gente.Nombre = "";
                gente.ApellidoPaterno = "";
                gente.ApellidoMaterno = "";
                gente.Email = "";
                gente.id = "";
                ArregloGente[0] = gente;
                //msg = "No consigio ninguna cuenta.";
                // return View();
            }
         }
            else
            {
                //msg = "Un  Error ha ocurrido ";
                ArregloGente = new Oportunidad[1];
                Oportunidad gente = new Oportunidad();
                gente.OportunidadId = 0;
                gente.Tema = "";
                gente.Nombre = "";
                gente.ApellidoPaterno = "";
                gente.ApellidoMaterno = "";
                gente.Email = "";
                gente.id = "";
                ArregloGente[0] = gente;
            }// CRMSVC
    }//Consulta


        public void ConsultaLead(String Entidad, String Operador, String Nombre, Boolean Todos)
        {
            //String fetchXML1;
            if (crmSvc != null && crmSvc.IsReady)
            {

                if (Todos)
                {
                    fetchXML1 =
                         @"<fetch version='1.0' output-format='xml-platform' 
                         mapping='logical' distinct='false' returntotalrecordcount='true' >
                         <entity name='" + Entidad + @"'>
                             <attribute name='new_apellidomaterno' /> <attribute name='leadid'/> <attribute name='subject'/>   
                             <attribute name='firstname'/> <attribute name='lastname'/>  
                             <attribute name='emailaddress1'/>
                             <link-entity name='systemuser' to='owninguser'> 
                                 <filter type='and'> 
                                    <condition attribute='lastname' operator='eq' value='Veliz' /> 
                                 </filter> 
                             </link-entity>
                        </entity>
                        </fetch>";
                }
                else

                {
                    fetchXML1 =
                      @"<fetch version='1.0' output-format='xml-platform' 
                     mapping='logical' distinct='false' returntotalrecordcount='true' >
                    <entity name='" + Entidad + @"'>
                           <attribute name='new_apellidomaterno' /> <attribute name='leadid' /> <attribute name='subject' /> 
                           <attribute name='firstname' /> <attribute name='lastname' />
                           <attribute name='emailaddress1' />
                           <filter>
			                    <condition attribute='new_emailaddress1' operator='" + Operador + @"' value='" + Nombre + @" ' />
   		                   </filter>
                     </entity>
                  </fetch>";
                }



                EntityCollection queryResult1 = crmSvc.GetEntityDataByFetchSearchEC(fetchXML1);


                if (queryResult1 != null)
                {

                    int i = 0;
                    ArregloLead = new Lead[queryResult1.TotalRecordCount];
                    foreach (var c in queryResult1.Entities)
                    {
                        //guid = c.Id;
                        //msg = msg + "  " + c.Attributes["name"] + " " ;
                        Lead gente = new Lead();
                        gente.LeadId           = i;
                        gente.guid             = c.Id;
                        gente.Tema             = "" + c.Attributes["subject"];
                        gente.FirstName        = "" + c.Attributes["firstname"];
                        gente.LastName         = "" + c.Attributes["lastname"];
                        gente.Apellido_Materno = "" + c.Attributes["new_apellidomaterno"];
                        //gente.Apellido_Materno = "";
                        gente.Email            = "" + c.Attributes["emailaddress1"];
                        gente.id               = c.Attributes["leadid"].ToString();
                        ArregloLead[i]         = gente;
                        i = i + 1;
                    }// ForEach
                    conecto = true;
                }// Query Result
                else
                {
                    ArregloLead = new Lead[1];
                    Lead gente = new Lead();
                    gente.LeadId = 0;
                    gente.Tema = "";
                    gente.Tema = "";
                    gente.LastName = "";
                    gente.Apellido_Materno = "";
                    gente.Email = "";
                    gente.id = "";
                    ArregloLead[0] = gente;
                    //msg = "No consigio ninguna cuenta.";
                    // return View();
                }
            }
            else
            {
                //msg = "Un  Error ha ocurrido ";
                ArregloLead = new Lead[1];
                Lead gente = new Lead();
                gente.LeadId = 0;
                gente.Tema = "";
                gente.Tema = "";
                gente.LastName = "";
                gente.Apellido_Materno = "";
                gente.Email = "";
                gente.id = "";
                ArregloLead[0] = gente;
            }// CRMSVC
        }//Consulta Lead

    }// Class DatosCRM
}// Namespace

