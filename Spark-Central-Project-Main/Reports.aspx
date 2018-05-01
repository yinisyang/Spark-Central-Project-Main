<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab is-active">Reports</a>
        <a href="/Manage.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <br />
    <div  Style="padding-left: 5%">
        <div class="mdl-grid">
            <div class="mdl-cell mdl-cell--2-col">
                <asp:Button ID="westCentralButton" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect" runat="server" Text="West Central Residents" OnClick="westCentralButton_Click" />
            </div>
        </div>
        <div class="mdl-grid">
            <div class="mdl-cell mdl-cell--2-col">
                <button id="show-dialog" type="button" class="mdl-button mdl-js-button mdl-button--raised">Item Checkouts</button>
            </div>
        </div>
    </div>
    <dialog class="mdl-dialog" style="width: 75%">
                <div class="mdl-dialog__content">
                    <div class="mdl-cell mdl-cell--6-col">
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="startDate" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="startDate">Start Date (yyyy-mm-dd)</label>
                        </div>
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="endDate" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="endDate">End Date (yyyy-mm-dd)</label>
                        </div>
                    </div>
                    <div class="mdl-cell mdl-cell--4-col">
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label getmdl-select">
                            <input type="text" value="" class="mdl-textfield__input" id="itemType" readonly>
                            <input type="hidden" runat="server" id="itemTypeValue" value="" name="itemType">
                            <label for="itemType" class="mdl-textfield__label">Item Type</label>
                            <ul for="itemType" class="mdl-menu mdl-menu--bottom-left mdl-js-menu">
                                <li class="mdl-menu__item" data-val="book">Books</li>
                                <li class="mdl-menu__item" data-val="technology">Technology</li>
                                <li class="mdl-menu__item" data-val="dvd">DVDs</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="mdl-dialog__actions">
                    <asp:Button ID="Submit" runat="server" Text="Submit Report"  CssClass="mdl-button mdl-js-button mdl-button--raised" OnClick="Submit_Click" />
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
    <br />
    <br />
            <div style="padding-left: 5%; padding-bottom:1%">

                <asp:Table ID="numTable" class="mdl-data-table mdl-js-data-table" runat="server">
                </asp:Table>


            </div>

            <div style="padding-left: 5%; padding-bottom:1%">

                <asp:Table ID="table" class="mdl-data-table mdl-js-data-table" runat="server">
                </asp:Table>


            </div>
</asp:Content>

