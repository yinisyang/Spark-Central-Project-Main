<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Panel" ContentPlaceHolderID="PanelPlaceHolder" runat="server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/Manage.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <div>

        <asp:Table ID="table" class="mdl-data-table mdl-js-data-table" runat="server">
            <asp:TableHeaderRow>
                <asp:TableCell>ID</asp:TableCell>
                <asp:TableCell CssClass="mdl-data-table__cell--non-numeric">Title</asp:TableCell>
                <asp:TableCell CssClass="mdl-data-table__cell--non-numeric">Author</asp:TableCell>
                <asp:TableCell>Pages</asp:TableCell>

            </asp:TableHeaderRow>
        </asp:Table>


    </div>
</asp:Content>

