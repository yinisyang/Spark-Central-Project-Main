<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Panel" ContentPlaceHolderID="PanelPlaceHolder" runat="server">
    <asp:Panel id="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
      <a href="/Dashboard.aspx" class="mdl-layout__tab is-active">Dashboard</a>
      <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
      <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
      <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
      <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
      <a href="/Manage.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div>
        Future Content

    </div>
</asp:Content>




