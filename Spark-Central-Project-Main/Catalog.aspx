<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Catalog.aspx.cs" Inherits="Catalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab is-active">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/Manage.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>





</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <br />
    <asp:Button ID="buttonBooks" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect" runat="server" Text="Books" OnClick="buttonBooks_Click" />
    <asp:Button ID="buttonDVD" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect" runat="server" Text="DVD" OnClick="buttonDVD_Click" />
    <asp:Button ID="buttonTech" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect" runat="server" Text="Technology" OnClick="buttonTech_Click" />
    <br />
    <br />

    <div>

        <asp:Table ID="table" class="mdl-data-table mdl-js-data-table" runat="server">

        </asp:Table>


    </div>



</asp:Content>

