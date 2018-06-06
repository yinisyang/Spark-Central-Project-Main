<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditTechnology.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="scripts/EditTech.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab is-active">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <br />

    <div class="mdl-grid" style="width: 50%">
        <div class="mdl-cell mdl-cell--6-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="techAssn" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="techAssn">ASSN</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="techName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="techName">Name</label>
            </div>
            <div class="mdl-dialog__actions">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="Submit_Click" CssClass="mdl-button mdl-js-button mdl-button" />
                <button id="btnDelete" type="button" onclick="deleteTech()" class="mdl-button mdl-js-button mdl-button">
                Delete
            </button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Cancel_Click" CssClass="mdl-button mdl-js-button mdl-button" />

            </div>

        </div>
        <div class="mdl-cell mdl-cell--6-col">
        </div>
    </div>

</asp:Content>

