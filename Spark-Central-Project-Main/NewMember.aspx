<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewMember.aspx.cs" Inherits="NewMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <h1 style="text-align:center">New Member</h1>
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
            <!--
            <asp:Label ID="lbladult_checkbox" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isAdultCheckbox">
                <input type="checkbox" runat="server" id="isAdultCheckbox" class="mdl-checkbox__input"  />
                <span class="mdl-checkbox__label">Adult</span>
            </asp:Label>-->
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label getmdl-select">
                <input type="text" value="" class="mdl-textfield__input" id="memberGroup" readonly>
                <input type="hidden" runat="server" id="memberGroupValue" value="" name="memberGroup">
                <label for="memberGroup" class="mdl-textfield__label">Member Group</label>
                <ul for="memberGroup" class="mdl-menu mdl-menu--bottom-left mdl-js-menu">
                    <li class="mdl-menu__item" data-val="Youth">Youth</li>
                    <li class="mdl-menu__item" data-val="Adult">Adult</li>
                </ul>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="guardianName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="guardianName">Guardian</label>
            </div>
        </div>
        
    </div>
    <div class="mdl-grid">
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="streetAddress" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="streetAddress">Street Address</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="city" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="city">City</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--1-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label getmdl-select">
                <input type="text" value="" class="mdl-textfield__input" id="state" readonly>
                <input type="hidden" runat="server" id="stateValue" value="" name="state">
                <label for="state" class="mdl-textfield__label">State</label>
                <ul for="state" class="mdl-menu mdl-menu--bottom-left mdl-js-menu">
                    <li class="mdl-menu__item" data-val="WA">Washington</li>
                    <li class="mdl-menu__item" data-val="OR">Oregon</li>
                    <li class="mdl-menu__item" data-val="CA">California</li>
                </ul>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--1-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="zipCode" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="zipCode">Zip</label>
            </div>
        </div>
    </div>
    <div class="mdl-grid">
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="phone" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="phone">Phone Number</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="email" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="email">Email</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="dateOfBirth" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="dateOfBirth">Date of Birth</label>
            </div>
        </div>
    </div>
    <div class="mdl-grid">
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label getmdl-select">
                <input type="text" value="" class="mdl-textfield__input" id="ethnicity" readonly>
                <input type="hidden" runat="server" id="ethnicityValue" value="" name="ethnicity">
                <label for="ethnicity" class="mdl-textfield__label">State</label>
                <ul for="ethnicity" class="mdl-menu mdl-menu--bottom-left mdl-js-menu">
                    <li class="mdl-menu__item" data-val="caucasian">Caucasian</li>
                    <li class="mdl-menu__item" data-val="african-american">African American</li>
                    <li class="mdl-menu__item" data-val="asian">Asian</li>
                    <li class="mdl-menu__item" data-val="native-american">Native American</li>
                    <li class="mdl-menu__item" data-val="middle-eastern">Middle Eastern</li>
                    <li class="mdl-menu__item" data-val="other">Other</li>
                </ul>
            </div>
        </div>
    </div>
    <div class="mdl-grid">
        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="checkoutQuota" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="checkoutQuota">Checkout Quota</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <asp:Label ID="lblrestrictedtotech_checkbox" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isRestrictedToTech">
                <input type="checkbox" runat="server" id="isRestrictedToTech" class="mdl-checkbox__input"  />
                <span class="mdl-checkbox__label">Restricted to Tech?</span>
            </asp:Label>
        </div>
        <div class="mdl-cell mdl-cell--4-col">
            <asp:Label ID="lbl" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isWestCentralResident">
                <input type="checkbox" runat="server" id="isWestCentralResident" class="mdl-checkbox__input"  />
                <span class="mdl-checkbox__label">West Central Resident?</span>
            </asp:Label>
        </div>
    </div>

    <div class="mdl-grid">
        <div class="mdl-cell mdl-cell--4-col">
            <asp:Button ID="Submit" runat="server" Text="Create New Member" OnClick="Submit_Click" CssClass="mdl-button mdl-js-button mdl-button--raised"/>
        </div>
    </div>
    
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>

