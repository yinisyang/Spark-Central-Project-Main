<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageAdmins.aspx.cs" Inherits="ManageAdmins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" Runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab is-active">Manage</a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <br />


    <div style="padding-left: 5%">

        <button id="show-dialog" type="button" class="mdl-button mdl-js-button mdl-button--raised">Add Admin</button>
        <asp:Label ID="newAdminLabel" class="mdl-list__item-primary-content" style="color:crimson" runat="server" Text="" ForeColor="#CC0000"></asp:Label>
        <dialog class="mdl-dialog" style="width: 75%">
            <h4 class="mdl-dialog__title">Add Admin</h4>
            <div class="mdl-dialog__content">

                <div class="mdl-grid">
                    <div class="mdl-cell mdl-cell--6-col">
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="userName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="userName">Username</label>
                        </div>
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="password" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="password">Password</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="mdl-dialog__actions">
                <asp:Button ID="Submit" runat="server" Text="Create New Admin" OnClick="Submit_Click" CssClass="mdl-button mdl-js-button mdl-button--raised" />
                <button type="button" class="mdl-button close">Cancel</button>
            </div>
        </dialog>


        <script>
            var dialog = document.querySelector('dialog');
            var showDialogButton = document.querySelector('#show-dialog');
            if (!dialog.showModal) {
                dialogPolyfill.registerDialog(dialog);
            }
            showDialogButton.addEventListener('click', function () {
                dialog.showModal();
            });
            dialog.querySelector('.close').addEventListener('click', function () {
                dialog.close();
            });
        </script>



    </div>
    <br />

    <div style="padding-left: 5%">

        <asp:Table ID="table" class="mdl-data-table mdl-js-data-table" runat="server">
        </asp:Table>


    </div>
</asp:Content>

