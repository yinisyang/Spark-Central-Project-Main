<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="scripts/jquery.easy-autocomplete.min.js"></script>
    <script src="scripts/Dashboard.js"></script>
    <link href="styles/easy-autocomplete.themes.min.css" rel="stylesheet" />
    <style>
        .mdl-card {
            width: 330px;
            height: 330px;
        }
    </style>


</asp:Content>

<asp:Content ID="Panel" ContentPlaceHolderID="PanelPlaceHolder" runat="server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab is-active">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <div class="mdl-grid" style="width: 70%">

        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <h2 class="mdl-card__title-text">Members</h2>
                </div>
                <div class="mdl-card__actions mdl-card--border">
                    <a id="addMemButton" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" title="Add New Member">Add
                    </a>
                    <a id="viewMem" href="/Members.aspx" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" title="View All Members">View All
                    </a>
                </div>
            </div>
        </div>

        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <h2 class="mdl-card__title-text">Library</h2>
                </div>
                <div class="mdl-card__actions mdl-card--border">

                    <button id="btnSmart" type="button" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" title="Smart Add">
                        Smart Add
                    </button>
                    <a href="Catalog.aspx" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" title="View Catalog">Catalog
                    </a>
                </div>
            </div>
        </div>

        <div class="mdl-cell mdl-cell--4-col">
            <div class="mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <h2 class="mdl-card__title-text">Checkouts</h2>
                </div>
                <div class="mdl-card__actions mdl-card--border">

                    <button id="checkOutButton" type="button" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" title="Check-Out">
                        Check-Out
                    </button>

                    <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" title="Check-In">Check-In
                    </a>
                </div>
            </div>
        </div>



        <div class="mdl-cell mdl-cell--4-col">


            <div class="mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <h2 class="mdl-card__title-text">Overdues</h2>
                </div>
                <div class="mdl-card__actions mdl-card--border">
                    <a href="Circulations.aspx" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" title="View Circulation">View All
                    </a>
                </div>
            </div>

        </div>

    </div>




    <!-- Check Out Dialog -->
    <dialog class="mdl-dialog" id="checkOutDialog" style="width: 60%">
        <h4 class="mdl-dialog__title">Check-Out</h4>

        <div class="mdl-grid" style="width: 90%">

            <div class="mdl-cell mdl-cell--6-col">

                <input id="txtmemberid" />

                <button id="btnFindMember" type="button" onclick="findMember()" class="mdl-button mdl-js-button mdl-button" title="Find">
                    Find
                </button>

                <div id="memberNameField"></div>
                <div id="memberhidden" class="hidden"></div>
                <label id="memName"></label>
            </div>
            <div class="mdl-cell mdl-cell--6-col">
                <input id="txtitemid" />

                <button id="btnFindItem" type="button" onclick="findItem()" class="mdl-button mdl-js-button mdl-button" title="Find">
                    Find
                </button>

                <div id="itemNameField"></div>
                <div id="itemhidden" class="hidden"></div>
            </div>
        </div>

        <div class="mdl-dialog__actions">
            <button id="btnCheckOut" type="button" class="mdl-button mdl-js-button mdl-button" title="Confirm">
                Confirm
            </button>
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>






    <!-- Smart Add Dialog -->
    <dialog class="mdl-dialog" id="smartDialog" style="width: 30%; top: 2%">
        <h4 class="mdl-dialog__title">Smart Add</h4>
        <div class="mdl-dialog__content">
            <div class="mdl-grid" id="smartGrid" style="width: 90%">
                <div class="mdl-cell mdl-cell--6-col">
                    <form action="#">
                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="isbn">
                            <label class="mdl-textfield__label" for="isbn">Isbn</label>
                        </div>
                    </form>
                </div>
                <div class="mdl-cell mdl-cell--6-col">
                </div>

                <div class="mdl-cell mdl-cell--4-col"></div>
                <div class="mdl-cell mdl-cell--4-col">
                    <img id="bookPic" src="images/empty.png" />
                </div>
                <div class="mdl-cell mdl-cell--4-col"></div>

                <div class="mdl-cell mdl-cell--6-col">
                    <div id="bookTitle"></div>
                    <div id="bookAuthor"></div>
                    <div id="bookPublisher"></div>
                    <div id="bookYear"></div>

                </div>
                <div class="mdl-cell mdl-cell--6-col">
                    <div id="bookPages"></div>
                    <div id="bookCategory"></div>
                    <div id="bookIsbn10"></div>
                    <div id="bookIsbn13"></div>

                </div>
                <div class="mdl-cell mdl-cell--12-col">
                    <div id="bookDescription"></div>
                </div>
            </div>
        </div>
        <div class="mdl-dialog__actions">
            <button id="btnAdd" type="button" onclick="addBook()" class="mdl-button mdl-js-button mdl-button" title="Add">
                Add
            </button>
            <button id="btnSearch" type="button" onclick="findGoogleBook()" class="mdl-button mdl-js-button mdl-button" title="Search">
                Search
            </button>
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>
    <!---End Smart Add Dialog-->

    <div>

        <!---Add Member Smart Dialog-->
        <dialog id="member-dialog" class="mdl-dialog" style="width: 50%">
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
                <asp:Button ID="Submit" runat="server" Text="Create New Member" OnClick="Submit_ClickMember" CssClass="mdl-button mdl-js-button mdl-button" />
                <button type="button" class="mdl-button close">Cancel</button>
            </div>
        </dialog>
    </div>
    <!---End Add Member Smart Dialog-->



</asp:Content>
