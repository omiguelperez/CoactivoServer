﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TipoDocumento{
        public TipoDocumento()
        {
            Documentos = new List<Documento>();
        }
        /// <summary>
        /// Este metodo convierte un DTO a DAL
        /// </summary>
        /// <param name="DTO">Parametro DTO</param>
        /// <returns>Objeto tipo DAL</returns>
        public static TipoDocumento Mapeo(TipoDocumentoDTO DTO)
        {
            TipoDocumento c = new TipoDocumento();
            c.CreatedAt = DTO.CreatedAt;
            c.Nombre = DTO.Nombre;
            c.UpdateAt = DTO.UpdateAt;
            c.TipoDocumentoId = DTO.TipoDocumentoId;
            return c;
        }
        public int TipoDocumentoId { get; set; }
        public string Nombre { get; set; }
        public virtual List<Documento> Documentos { get; set; }
        private DateTime _updateAt;
        public DateTime UpdateAt { get { return _updateAt; } set { _updateAt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second); } }
        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second); } }
    }
}
