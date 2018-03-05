<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewMember.aspx.cs" Inherits="NewMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <h1>New Member</h1>
    <div class="mdl-grid">
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="firstName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="firstName">First Name</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="lastName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="lastName">Last Name</label>
            </div>
        </div>
    </div>
    <div class="mdl-grid">
        <div class="mdl-cell mdl-cell--4-col">
            <asp:Label ID="lbladult_checkbox" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isAdultCheckbox">
                <input type="checkbox" runat="server" id="isAdultCheckbox" class="mdl-checkbox__input"  />
                <span class="mdl-checkbox__label">Adult</span>
            </asp:Label>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="guardianName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="guardianName">Guardian</label>
            </div>
        </div>
    </div>
    <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" CssClass="mdl-button mdl-js-button mdl-button--raised"/>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>

