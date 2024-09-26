<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ejemplo.aspx.cs" Inherits="proyecto_practica.ejemplo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestión de Artículos</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <a class="navbar-brand" href="#">gestion de articulos</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" href="#">Agregar Artículo</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Eliminar Artículo</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Ingresar Código de Descuento</a>
                        </li>
                    </ul>
                </div>
            </nav>

            <div class="row mt-4">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlMarcas">Filtrar por Marca</label>
                        <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">-- Seleccionar Marca --</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="filtroNombre">Filtrar por Nombre</label>
                        <asp:TextBox ID="filtroNombre" runat="server" CssClass="form-control" placeholder="Nombre del Artículo" />
                    </div>
                </div>
                <div class="col-md-4">
                    <button type="button" class="btn btn-primary mt-4" runat="server" onserverclick="FiltrarArticulos">Buscar</button>
                </div>
            </div>

            <table class="table table-bordered">
    <thead>
        <tr>
            <th>Código</th>
            <th>Nombre</th>
            <th>Marca</th>
            <th>Categoría</th>
            <th>Precio</th>
            <th>Imagen</th>
        </tr>
    </thead>
    <tbody id="tablaArticulos" runat="server">
       
    </tbody>
</table>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>