using WebGestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// netsh http add urlacl url=http://192.168.1.14:29128/ user=Todos

namespace WebGestion.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, nameOption = "Bienes", imageClass = "fa fa-gift fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 2, nameOption = "Inventario", controller = "Hub", action = "Inventario", imageClass = "fa fa-gift fa-fw", status = true, isParent = false, parentId = 1 });
            menu.Add(new Navbar { Id = 3, nameOption = "Bienes", controller = "Hub", action = "Bienes", imageClass = "fa fa-gift fa-fw", status = true, isParent = false, parentId = 1 });
            menu.Add(new Navbar { Id = 4, nameOption = "Familias", controller = "Hub", action = "Familias", imageClass = "fa fa-tasks  fa-fw", status = true, isParent = false, parentId = 1 });
            menu.Add(new Navbar { Id = 5, nameOption = "Cualidades", controller = "Hub", action = "Cualidades", imageClass = "fa fa-tasks  fa-fw", status = true, isParent = false, parentId = 1 });
            menu.Add(new Navbar { Id = 6, nameOption = "Tipos de Cualidades", controller = "Hub", action = "TiposDeCualidades", imageClass = "fa fa-tasks  fa-fw", status = true, isParent = false, parentId = 1 });
            menu.Add(new Navbar { Id = 20, nameOption = "Clientes", imageClass = "fa fa-users fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 21, nameOption = "Clientes", controller = "Hub", action = "Clientes", imageClass = "fa fa-users fa-fw", status = true, isParent = false, parentId = 20 });
            menu.Add(new Navbar { Id = 22, nameOption = "Presupuestos", controller = "Hub", action = "PresupuestosClientes", imageClass = "fa fa-shopping-cart fa-fw", status = true, isParent = false, parentId = 20 });
            menu.Add(new Navbar { Id = 23, nameOption = "Pedidos", controller = "Hub", action = "PedidosClientes", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 20 });
            menu.Add(new Navbar { Id = 24, nameOption = "Facturas", controller = "Hub", action = "FacturasClientes", imageClass = "fa fa-money fa-fw", status = true, isParent = false, parentId = 20 });
            menu.Add(new Navbar { Id = 30, nameOption = "Proveedores", imageClass = "fa fa-user fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 31, nameOption = "Proveedores", controller = "Hub", action = "Proveedores", imageClass = "fa fa-user fa-fw", status = true, isParent = false, parentId = 30 });
            menu.Add(new Navbar { Id = 32, nameOption = "Pedidos", controller = "Hub", action = "PedidosProveedores", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 30 });
            menu.Add(new Navbar { Id = 33, nameOption = "Facturas", controller = "Hub", action = "FacturasProveedores", imageClass = "fa fa-money fa-fw", status = true, isParent = false, parentId = 30 });
            menu.Add(new Navbar { Id = 40, nameOption = "Empresa", imageClass = "fa fa-institution  fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 41, nameOption = "Empresas", controller = "Hub", action = "Empresas", imageClass = "fa fa-graduation-cap  fa-fw", status = true, isParent = false, parentId = 40 });
            menu.Add(new Navbar { Id = 42, nameOption = "Locales", controller = "Hub", action = "Locales", imageClass = "fa fa-home fa-fw", status = true, isParent = false, parentId = 40 });
            menu.Add(new Navbar { Id = 43, nameOption = "Empleados", controller = "Hub", action = "Empleados", imageClass = "fa fa-child fa-fw", status = true, isParent = false, parentId = 40 });
            menu.Add(new Navbar { Id = 44, nameOption = "Cargos", controller = "Hub", action = "Cargos", imageClass = "fa fa-graduation-cap  fa-fw", status = true, isParent = false, parentId = 40 });
            menu.Add(new Navbar { Id = 45, nameOption = "Recursos", controller = "Hub", action = "Recursos", imageClass = "fa fa-graduation-cap  fa-fw", status = true, isParent = false, parentId = 40 });
            //menu.Add(new Navbar { Id = 44, nameOption = "Permisos de usuario", controller = "Hub", action = "Permisos", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 40 });
            menu.Add(new Navbar { Id = 50, nameOption = "Mantenimiento", imageClass = "fa fa-gears fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 51, nameOption = "Formas de entrega", controller = "Hub", action = "FormasEntrega", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 52, nameOption = "Formas de pago", controller = "Hub", action = "FormasPago", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 53, nameOption = "Localidades", controller = "Hub", action = "Localidades", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 54, nameOption = "Provincias", controller = "Hub", action = "Provincias", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 55, nameOption = "Países", controller = "Hub", action = "Paises", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 56, nameOption = "Familias de Bienes", controller = "Hub", action = "Familias", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 57, nameOption = "Tipos de Factura", controller = "Hub", action = "TiposFacturas", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 58, nameOption = "Estados", controller = "Hub", action = "Estados", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 59, nameOption = "IVA", controller = "Hub", action = "IVAs", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 60, nameOption = "IRPF", controller = "Hub", action = "IRPFs", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 61, nameOption = "Usuarios de la aplicación", controller = "Hub", action = "AspNetUsers", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 62, nameOption = "Grupos de Usuario", controller = "Hub", action = "ApplicationGroups", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 63, nameOption = "Roles de Usuario", controller = "Hub", action = "AspNetRoles", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 64, nameOption = "Info de Usuario", controller = "Hub", action = "AspNetUserClaims", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 65, nameOption = "Idiomas", controller = "Hub", action = "Idiomas", imageClass = "fa fa-graduation-cap  fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 66, nameOption = "Personas", controller = "Hub", action = "Personas", imageClass = "fa fa-graduation-cap  fa-fw", status = true, isParent = false, parentId = 50 });
            menu.Add(new Navbar { Id = 67, nameOption = "Historicos", controller = "Hub", action = "Historicos", imageClass = "fa fa-graduation-cap  fa-fw", status = true, isParent = false, parentId = 50 });
            //menu.Add(new Navbar { Id = 60, nameOption = "IRPFs", controller = "Hub", action = "IRPFs", imageClass = "fa fa-ticket fa-fw", status = true, isParent = false, parentId = 50 });
            //menu.Add(new Navbar { Id = 160, nameOption = "Administración", imageClass = "fa fa-sitemap fa-fw", status = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 161, nameOption = "Dashboard", controller = "Home", action = "Dashboard", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 160 });

            //menu.Add(new Navbar { Id = 162, nameOption = "Charts", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = true, parentId = 160 });
            //menu.Add(new Navbar { Id = 163, nameOption = "Flot Charts", controller = "Home", action = "FlotCharts", status = true, isParent = false, parentId = 162 });
            //menu.Add(new Navbar { Id = 164, nameOption = "Morris.js Charts", controller = "Home", action = "MorrisCharts", status = true, isParent = false, parentId = 162 });
            //menu.Add(new Navbar { Id = 165, nameOption = "Tables", controller = "Home", action = "Tables", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 160 });
            //menu.Add(new Navbar { Id = 166, nameOption = "Forms", controller = "Home", action = "Forms", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 160 });

            //menu.Add(new Navbar { Id = 167, nameOption = "UI Elements", imageClass = "fa fa-wrench fa-fw", status = true, isParent = true, parentId = 160 });
            //menu.Add(new Navbar { Id = 168, nameOption = "Panels and Wells", controller = "Home", action = "Panels", status = true, isParent = false, parentId = 167 });
            //menu.Add(new Navbar { Id = 169, nameOption = "Buttons", controller = "Home", action = "Buttons", status = true, isParent = false, parentId = 167 });
            //menu.Add(new Navbar { Id = 170, nameOption = "Notifications", controller = "Home", action = "Notifications", status = true, isParent = false, parentId = 167 });
            //menu.Add(new Navbar { Id = 171, nameOption = "Typography", controller = "Home", action = "Typography", status = true, isParent = false, parentId = 167 });
            //menu.Add(new Navbar { Id = 172, nameOption = "Icons", controller = "Home", action = "Icons", status = true, isParent = false, parentId = 167 });
            //menu.Add(new Navbar { Id = 173, nameOption = "Grid", controller = "Home", action = "Grid", status = true, isParent = false, parentId = 167 });

            //menu.Add(new Navbar { Id = 174, nameOption = "Multi-Level Dropdown", imageClass = "fa fa-sitemap fa-fw", status = true, isParent = true, parentId = 160 });
            //menu.Add(new Navbar { Id = 175, nameOption = "Second Level Item", status = true, isParent = false, parentId = 174 });

            //menu.Add(new Navbar { Id = 176, nameOption = "Sample Pages", imageClass = "fa fa-files-o fa-fw", status = true, isParent = true, parentId = 160 });
            //menu.Add(new Navbar { Id = 177, nameOption = "Blank Page", controller = "Home", action = "Blank", status = true, isParent = false, parentId = 176 });
            //menu.Add(new Navbar { Id = 178, nameOption = "Login Page", controller = "Home", action = "Login", status = true, isParent = false, parentId = 176 });

            return menu.ToList();
        }
    }
}