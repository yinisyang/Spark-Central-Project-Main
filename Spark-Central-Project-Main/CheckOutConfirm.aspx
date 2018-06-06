<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckOutConfirm.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <div class="mdl-grid" style="width: 50%">

        <div class="mdl-cell mdl-cell--6-col">
            <br /><br /><br /><br />
            <asp:Label ID="lblMemberNumber" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </div>
        <div class="mdl-cell mdl-cell--6-col">
            <br /><br /><br /><br />
            <asp:Label ID="lblItemNumber" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblItem" runat="server" Text=""></asp:Label>
        </div>
        <div class="mdl-cell mdl-cell--12-col">
            <br /><br /><br /><br />
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="TxtDate" runat="server" CssClass="mdl-textfield__input" />
                <label class="mdl-textfield__label" for="TxtDate">Due Date</label>
            </div>
            <br /><br /><br />
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="Submit_ClickCancel" CssClass="mdl-button mdl-js-button mdl-button" />
            <asp:Button ID="BtnConfirm" runat="server" Text="Confirm" OnClick="Submit_ClickConfirm" CssClass="mdl-button mdl-js-button mdl-button" />
        </div>

    </div>
</asp:Content>

