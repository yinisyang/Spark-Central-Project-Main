<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <div>

        <asp:table ID="table" class="mdl-data-table mdl-js-data-table" runat="server">
    <asp:TableHeaderRow>
        <asp:TableCell>ID</asp:TableCell>
        <asp:TableCell cssclass="mdl-data-table__cell--non-numeric">Title</asp:TableCell>
        <asp:TableCell cssclass="mdl-data-table__cell--non-numeric">Author</asp:TableCell>
        <asp:TableCell>Pages</asp:TableCell>
    
    </asp:TableHeaderRow>
</asp:table>


    </div>
</asp:Content>

