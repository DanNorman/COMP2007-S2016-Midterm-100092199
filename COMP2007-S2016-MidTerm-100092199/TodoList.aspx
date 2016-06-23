<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_100092199.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-1 col-md-10">
                <h1>Todo List</h1>
                <a href="todoDetails.aspx" class="btn btn-success btn-sm"><i class='fa fa-plus-square'></i> Add Todo</a>
                <div>
                    <label for="PageSizeDropDownList">Records per Page: </label>
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                        AutoPostBack="true" CssClass="btn btn-default bt-sm dropdown-toggle"
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>
                <asp:GridView runat="server" ID="TodoGridView" CssClass="table table-bordered table-striped table-hover" 
                    AutoGenerateColumns="false" DataKeyNames="TodoID" OnRowDeleting="TodoGridView_RowDeleting" AllowPaging="true" PageSize="3"
                    OnPageIndexChanging="TodoGridView_PageIndexChanging" AllowSorting="true"
                    OnSorting="TodoGridView_Sorting" OnRowDataBound="TodoGridView_RowDataBound" 
                    PagerStyle-CssClass="pagination-ys" >
                    <Columns>
                        <asp:BoundField DataField="TodoName" HeaderText=" Todo " Visible="true" />
                        <asp:BoundField DataField="TodoNotes" HeaderText=" Notes " Visible="true" />
                        <asp:TemplateField><ItemTemplate><asp:CheckBox runat="server" AutoPostBack="true" ID="Completed" OnCheckedChanged="Completed_CheckedChanged" /></ItemTemplate></asp:TemplateField>          
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" NavigateUrl="~/TodoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server" DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" />
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash'></i> Delete" ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
