<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP229_F2016_MidTerm_300702645.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div>
            <label for="PageSizeDropDownList">Records per Page:</label>
            <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                AutoPostBack="true" CssClass="btn btn-default btn-sm dropdown-toggle">
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="All" Value="10000" />
            </asp:DropDownList>
        </div>
    </div>
</asp:Content>
