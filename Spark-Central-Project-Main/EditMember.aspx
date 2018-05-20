<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditMember.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="Server">

    <script src="scripts/EditMember.js"></script>

</asp:Content>
<asp:Content ID="Panel" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab is-active">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>



    <div class="mdl-grid" style="width: 50%">
        <div class="mdl-cell mdl-cell--6-col">


            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="firstName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="firstName">First Name</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="lastName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="lastName">Last Name</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="phone" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="phone">Phone Number</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="email" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="email">Email</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="dateOfBirth" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="dateOfBirth">Date of Birth</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="checkoutQuota" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="checkoutQuota">Checkout Quota</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--6-col">


            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="streetAddress" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="streetAddress">Street Address</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="city" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="city">City</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="state" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="state">State</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="zipCode" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="zipCode">Zip</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="guardianName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="guardianName">Guardian</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="ethnicity" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="guardianName">Ethnicity</label>
            </div>
            <asp:Label ID="lblrestrictedtotech_checkbox" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isRestrictedToTech">
                <input type="checkbox" runat="server" id="isRestrictedToTech" class="mdl-checkbox__input" />
                <span class="mdl-checkbox__label">Restricted to Tech?</span>
            </asp:Label>
            <asp:Label ID="lbl" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isWestCentralResident">
                <input type="checkbox" runat="server" id="isWestCentralResident" class="mdl-checkbox__input" />
                <span class="mdl-checkbox__label">West Central Resident?</span>
            </asp:Label>
            <asp:Label ID="lblAdult" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isAdult">
                <input type="checkbox" runat="server" id="isAdult" class="mdl-checkbox__input" />
                <span class="mdl-checkbox__label">Adult?</span>
            </asp:Label>

            <div class="mdl-dialog__actions">
                <asp:Button ID="Button1" runat="server" Text="Save Changes" OnClick="Submit_Click" CssClass="mdl-button mdl-js-button mdl-button" />
                <button id="btnDelete" type="button" onclick="deleteMember()" class="mdl-button mdl-js-button mdl-button">
                            Delete
                </button>
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Cancel_Click" CssClass="mdl-button mdl-js-button mdl-button" />

            </div>
        </div>

    </div>

</asp:Content>

