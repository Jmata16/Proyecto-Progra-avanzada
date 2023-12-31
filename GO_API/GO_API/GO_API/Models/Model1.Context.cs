﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GO_API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GO_ProyectoEntities : DbContext
    {
        public GO_ProyectoEntities()
            : base("name=GO_ProyectoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
    
        public virtual ObjectResult<IniciarSesion_Result> IniciarSesion(string correoElectronico, string contrasenna)
        {
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<IniciarSesion_Result>("IniciarSesion", correoElectronicoParameter, contrasennaParameter);
        }
    
        public virtual int Registrarse(string correoElectronico, string contrasenna, string identificacion, string nombre, Nullable<bool> estado, Nullable<byte> idRol)
        {
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Registrarse", correoElectronicoParameter, contrasennaParameter, identificacionParameter, nombreParameter, estadoParameter, idRolParameter);
        }
    
        public virtual int RegistrarBitacora(string origen, string mensaje, Nullable<long> idUsuario)
        {
            var origenParameter = origen != null ?
                new ObjectParameter("Origen", origen) :
                new ObjectParameter("Origen", typeof(string));
    
            var mensajeParameter = mensaje != null ?
                new ObjectParameter("Mensaje", mensaje) :
                new ObjectParameter("Mensaje", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarBitacora", origenParameter, mensajeParameter, idUsuarioParameter);
        }
    
        public virtual ObjectResult<Nullable<long>> Registrarse1(string correoElectronico, string contrasenna, string identificacion, string nombre, Nullable<bool> estado, Nullable<byte> idRol)
        {
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("Registrarse1", correoElectronicoParameter, contrasennaParameter, identificacionParameter, nombreParameter, estadoParameter, idRolParameter);
        }
    
        public virtual int REGISTRAR_ERROR(string origen, string mensaje, Nullable<long> idUsuario, string direccionIP)
        {
            var origenParameter = origen != null ?
                new ObjectParameter("Origen", origen) :
                new ObjectParameter("Origen", typeof(string));
    
            var mensajeParameter = mensaje != null ?
                new ObjectParameter("Mensaje", mensaje) :
                new ObjectParameter("Mensaje", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(long));
    
            var direccionIPParameter = direccionIP != null ?
                new ObjectParameter("DireccionIP", direccionIP) :
                new ObjectParameter("DireccionIP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("REGISTRAR_ERROR", origenParameter, mensajeParameter, idUsuarioParameter, direccionIPParameter);
        }
    }
}
