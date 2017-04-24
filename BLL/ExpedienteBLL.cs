﻿using DAL;
using DAL.Infrastructure;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ExpedienteBLL
    {
        Respuesta respuesta = new Respuesta();
        ApplicationDbContext db = new ApplicationDbContext();

        public Respuesta Insertar(ExpedienteDTO expediente)
        {
            using (db = new ApplicationDbContext())
            {
                try
                {
                    // preparar el cliente para guardar
                    Expediente c = Expediente.MapeoDTOToDAL(expediente);
                    c.Obligacion.Expediente = c;
                    PersonaDTO persona = new PersonaBLL().FindByIdentificacion(c.Obligacion.Persona.Identificacion);
                    if (persona != null) {//QUIERE DECIR QUE LA PERSONA YA EXISTE
                        c.Obligacion.PersonaId = persona.PersonaId;
                        c.Obligacion.Persona = null;
                    }
                    if (expediente.Documentos.Count > 0)
                    {
                        c.Documentos = Documento.ConvertList(expediente.Documentos);
                    }
                    db.Expedientes.Add(c);

                    // preparar la respuesta
                    respuesta.FilasAfectadas = db.SaveChanges();
                    respuesta.Mensaje = "Se realizó la operación satisfactoriamente";
                    respuesta.Error = false;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    respuesta.Mensaje = ex.Message;
                    respuesta.FilasAfectadas = 0;
                    respuesta.Error = true;
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = ex.Message;
                    respuesta.FilasAfectadas = 0;
                    respuesta.Error = true;
                }

                return respuesta;
            }
        }

        public List<ExpedienteDTO> GetRecords()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Expedientes
                    .Select(t =>
                        Expediente.MapeoDALToDTO(t)
                    ).ToList();
            }
        }

        public ExpedienteDTO FindById(int ExpedienteId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ExpedienteDTO expediente = Expediente.MapeoDALToDTO(db.Expedientes.FirstOrDefault(t => t.ExpedienteId.Equals(ExpedienteId))); // Busca por llave primaria
                return expediente;
            }
        }
    }
}