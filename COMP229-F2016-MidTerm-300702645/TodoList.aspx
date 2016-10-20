﻿<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP229_F2016_MidTerm_300702645.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div>

            <h1>To do List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm">
                    <i class="fa fa-plus"></i> Add Task
                </a>
            <!-- Drop down menu with the amount of records to control the Grid View -->
            <label for="PageSizeDropDownList">Records per Page:</label>
            <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                AutoPostBack="true" CssClass="btn btn-default btn-sm dropdown-toggle"
                OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="All" Value="10000" />
            </asp:DropDownList>

            <!-- Grid View with the Todo List Data-->
            <asp:GridView ID="TodoGridView" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover" DataKeyNames="TodoId"
                    OnRowDeleting="TodoGridView_RowDeleting" AllowPaging="true" PageSize="3"
                    OnPageIndexChanging="TodoGridView_PageIndexChanging" AllowSorting="true"
                    OnSorting="TodoGridView_Sorting" OnRowDataBound="TodoGridView_RowDataBound"
                    PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="TodoDescription" HeaderText="To Do" Visible="true" SortExpression="TodoDescription" />
                        <asp:BoundField DataField="TodoNotes" HeaderText="Notes" Visible="true" SortExpression="TodoNotes" />
                        <asp:CheckBoxField DataField="Completed" HeaderText="Completed?" Visible="true" SortExpression="Completed" />

                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit"
                            NavigateUrl="~/TodoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm"
                            runat="server" DataNavigateUrlFields="TodoId"
                            DataNavigateUrlFormatString="TodoDetails.aspx?TodoId={0}" />

                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
        </div>
    </div>
</asp:Content>
