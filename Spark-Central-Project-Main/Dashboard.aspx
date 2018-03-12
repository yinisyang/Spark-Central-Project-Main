<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Panel" ContentPlaceHolderID="PanelPlaceHolder" runat="server">
    <asp:Panel id="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
      <a href="/Default.aspx" class="mdl-layout__tab">Dashboard</a>
      <a href="/Dashboard.aspx" class="mdl-layout__tab is-active">Members</a>
      <a href="#scroll-tab-3" class="mdl-layout__tab">Books</a>
      <a href="#scroll-tab-4" class="mdl-layout__tab">DVD</a>
      <a href="#scroll-tab-5" class="mdl-layout__tab">Technology</a>
      <a href="#scroll-tab-6" class="mdl-layout__tab">Tab 6</a>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div>
        Future Content

    </div>
</asp:Content>




