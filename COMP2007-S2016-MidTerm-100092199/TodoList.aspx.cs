using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm_100092199.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm_100092199
{
    public partial class TodoList : System.Web.UI.Page
    {

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get Todo Data
                this.GetTodo();
            }
        }

    

        protected void GetTodo()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                //query the student table using EF and LINQ
                var Todo = (from allTodo in db.Todos select allTodo);

                // bind the result to the GridView
                TodoGridView.DataSource = Todo.ToList();
                TodoGridView.DataBind();
            }
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected TodoID using the Grid's DataKey collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected student in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the Student class and store the query string inside of it
                Todo deletedTodo = (from TodoRecord in db.Todos
                                    where TodoRecord.TodoID == TodoID
                                    select TodoRecord).FirstOrDefault();

                // remove the selected student from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetTodo();
            }
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetTodo();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sorty by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetTodo();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new Page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetTodo();
        }

        protected void Completed_CheckedChanged(object sender, EventArgs e)
        {
            /*using (TodoConnection db = new TodoConnection())
            {
                Todo newTodo = new Todo();
                CheckBox chk = sender as CheckBox;

                if (chk.Checked)
                {
                    newTodo.Completed = true;
                }
                else
                {
                    newTodo.Completed = false;
                }

            }

            // refresh the grid
            this.GetTodo();*/
        }
    }
}