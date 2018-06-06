<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Circulations.aspx.cs" Inherits="Circulations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="scripts/Circulations.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab is-active">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">


    <div id="tableDiv" style="padding-left: 5%; overflow-x: auto; width: 75%">

        <br />
        <asp:Label ID="lblHelp" runat="server" Text=""></asp:Label>
        <h2>Check-Outs</h2>
        <button id="btnAddCo" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Check-Out">
            <i class="material-icons">add_shopping_cart</i>
        </button>
        <asp:Table ID="table" class="mdl-data-table" runat="server">
        </asp:Table>
        <br />
        <br />
        <br />
        <br />

        <hr />

        <br />
        <br />
        <br />
        <br />
        <h2>Fines</h2>
        <button id="btnAddFine" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Add Fine">
            <i class="material-icons">add</i>
        </button>
        <asp:Table ID="finetable" class="mdl-data-table" runat="server">
        </asp:Table>


    </div>


        <!-- Start Check Out Dialog -->
    <dialog class="mdl-dialog" id="coDialog" style="width: 60%">
        <h4 class="mdl-dialog__title">Check-Out</h4>

        <div class="mdl-grid" style="width: 90%">

            <div class="mdl-cell mdl-cell--6-col">

                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <asp:Textbox ID="txtmemid" runat="server" Cssclass="mdl-textfield__input" />
                    <label class="mdl-textfield__label" for="txtmemberid">Member ID</label>
                </div>

            </div>
            <div class="mdl-cell mdl-cell--6-col">
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <asp:Textbox ID="txtitemassn" runat="server" Cssclass="mdl-textfield__input" />
                    <label class="mdl-textfield__label" for="txtitemassn">Item Assn</label>
                </div>
            </div>

        </div>

        <div class="mdl-dialog__actions">
            <asp:Button ID="checkOutConfirm" runat="server" Text="Check-Out" OnClick="Submit_ClickCheckOut" CssClass="mdl-button mdl-js-button mdl-button" />
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>
    <!-- End Check Out Dialog -->



    <!-- Start Add Fine Dialog -->
    <dialog class="mdl-dialog" id="fineDialog" style="width: 30%">
        <h4 class="mdl-dialog__title">Add Fine</h4>
        <div class="mdl-dialog__content">

            <div class="mdl-grid" style="width: 90%">
                <div class="mdl-cell mdl-cell--6-col">


                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                        <asp:TextBox ID="txtMemberId" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="txtMemberId">Member ID</label>
                    </div>

                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="txtAmount">Amount</label>
                    </div>

                </div>
                <div class="mdl-cell mdl-cell--12-col">
                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label mdl-textfield--full-width">
                        <asp:TextBox ID="txtFineDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="txtFineDescription">Description</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="mdl-dialog__actions">
            <asp:Button ID="SubmitFine" runat="server" Text="Add Fine" OnClick="SubmitFine_Click" CssClass="mdl-button mdl-js-button mdl-button" />
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>
    <!-- End Fine Dialog -->

</asp:Content>

