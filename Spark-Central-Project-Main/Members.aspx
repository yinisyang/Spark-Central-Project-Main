<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="Members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab is-active">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/Manage.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <br />


    <div style="padding-left: 10%">
        <asp:Button ID="buttonNewMember" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect" runat="server" Text="Add New Member" OnClick="buttonNewMember_Click" />

        <button id="show-dialog" type="button" class="mdl-button">Add Member</button>
        <dialog class="mdl-dialog">
            <h4 class="mdl-dialog__title">Add Member</h4>
            <div class="mdl-dialog__content">

                <div class="mdl-grid">
                    <div class="mdl-cell mdl-cell--6-col">

                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="fname">
                            <label class="mdl-textfield__label" for="sample1">First Name</label>
                        </div>
                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="lname">
                            <label class="mdl-textfield__label" for="sample1">Last Name</label>
                        </div>
                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="phone">
                            <label class="mdl-textfield__label" for="sample1">Phone Number</label>
                        </div>
                    </div>
                    <div class="mdl-cell mdl-cell--6-col">

                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="pname">
                            <label class="mdl-textfield__label" for="sample1">First Name</label>
                        </div>
                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="xname">
                            <label class="mdl-textfield__label" for="sample1">Last Name</label>
                        </div>
                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="chone">
                            <label class="mdl-textfield__label" for="sample1">Phone Number</label>
                        </div>
                    </div>

                </div>
            </div>
            <div class="mdl-dialog__actions">
                <button type="button" class="mdl-button">Add</button>
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

