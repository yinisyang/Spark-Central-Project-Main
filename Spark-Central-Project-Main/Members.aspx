<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="Members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="scripts/Members.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab is-active">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>


    <br />


    <div style="padding-left: 5%">

        <!-- New Member Button -->
        <button id="show-dialog" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Add Member">
            <i class="material-icons">person_add</i>
        </button>


        <br />


        <!-- Note Label
            *used for notifying user of search parameter and provides feedback after a new member is created.
            -->
        <asp:Label ID="NoteLabel" class="mdl-list__item-primary-content" Style="color: darkcyan" runat="server" Text=""></asp:Label>



        <!--
            Member Dialog
            -->
        <div id="member-dialog">


            <dialog class="mdl-dialog" style="width: 50%">
                <h4 class="mdl-dialog__title">Add Member</h4>
                <div class="mdl-dialog__content">

                    <div class="mdl-grid" style="width: 90%">
                        <div class="mdl-cell mdl-cell--6-col">


                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:TextBox ID="firstName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                <label class="mdl-textfield__label" for="firstName">First Name</label>
                            </div>

                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:TextBox ID="lastName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                <label class="mdl-textfield__label" for="lastName">Last Name</label>
                            </div>

                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label getmdl-select">
                                <input type="text" value="" class="mdl-textfield__input" id="memberGroup">
                                <input type="hidden" runat="server" id="memberGroupValue" value="" name="memberGroup">
                                <label for="memberGroup" class="mdl-textfield__label">Member Group</label>
                                <ul for="memberGroup" class="mdl-menu mdl-menu--bottom-left mdl-js-menu">
                                    <li class="mdl-menu__item" data-val="Youth">Youth</li>
                                    <li class="mdl-menu__item" data-val="Adult">Adult</li>
                                </ul>
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
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label getmdl-select">
                                <input type="text" value="" class="mdl-textfield__input" id="state">
                                <input type="hidden" runat="server" id="stateValue" value="" name="state">
                                <label for="state" class="mdl-textfield__label">State</label>
                                <ul for="state" class="mdl-menu mdl-menu--bottom-left mdl-js-menu">
                                    <li class="mdl-menu__item" data-val="WA">Washington</li>
                                    <li class="mdl-menu__item" data-val="OR">Oregon</li>
                                    <li class="mdl-menu__item" data-val="CA">California</li>
                                </ul>
                            </div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:TextBox ID="zipCode" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                <label class="mdl-textfield__label" for="zipCode">Zip</label>
                            </div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:TextBox ID="guardianName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                <label class="mdl-textfield__label" for="guardianName">Guardian</label>
                            </div>

                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label getmdl-select">
                                <input type="text" value="" class="mdl-textfield__input" id="ethnicity">
                                <input type="hidden" runat="server" id="ethnicityValue" value="" name="ethnicity">
                                <label for="ethnicity" class="mdl-textfield__label">Ethnicity</label>
                                <ul for="ethnicity" class="mdl-menu mdl-menu--bottom-left mdl-js-menu">
                                    <li class="mdl-menu__item" data-val="caucasian">Caucasian</li>
                                    <li class="mdl-menu__item" data-val="african-american">African American</li>
                                    <li class="mdl-menu__item" data-val="asian">Asian</li>
                                    <li class="mdl-menu__item" data-val="native-american">Native American</li>
                                    <li class="mdl-menu__item" data-val="middle-eastern">Middle Eastern</li>
                                    <li class="mdl-menu__item" data-val="other">Other</li>
                                </ul>
                            </div>
                            <asp:Label ID="lblrestrictedtotech_checkbox" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isRestrictedToTech">
                                <input type="checkbox" runat="server" id="isRestrictedToTech" class="mdl-checkbox__input" />
                                <span class="mdl-checkbox__label">Restricted to Tech?</span>
                            </asp:Label>
                            <asp:Label ID="lbl" runat="server" CssClass="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" AssociatedControlID="isWestCentralResident">
                                <input type="checkbox" runat="server" id="isWestCentralResident" class="mdl-checkbox__input" />
                                <span class="mdl-checkbox__label">West Central Resident?</span>
                            </asp:Label>
                        </div>

                    </div>
                </div>
                <div class="mdl-dialog__actions">
                    <asp:Button ID="Submit" runat="server" Text="Create New Member" OnClick="Submit_Click" CssClass="mdl-button mdl-js-button mdl-button" />
                    <button type="button" class="mdl-button close">Cancel</button>
                </div>
            </dialog>
        </div>
        <!--
            End New Member Dialog
            -->
    </div>


    <!-- Member Table -->



    <div id="tableDiv" style="padding-left: 5%; overflow-x: auto; width: 75%">

        <table class="mdl-data-table" id="table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Last Name</th>
                    <th>First Name</th>
                    <th>Phone</th>
                    <th>Email</th>
                    <th>Guardian Name</th>
                    <th>Tech Restricted</th>
                    <th>Edit</th>
                </tr>
            </thead>
        </table>

    </div>

    <br />


</asp:Content>


