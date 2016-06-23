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
                //Get Game Data
                this.GetToDo();
            }
        }

    

        protected void GetToDo()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                //query the student table using EF and LINQ
                var ToDo = (from allToDo in db.Todos select allToDo);

                // bind the result to the GridView
                ToDoGridView.DataSource = ToDo.ToList();
                ToDoGridView.DataBind();
            }
        }

        protected void ToDoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected GameID using the Grid's DataKey collection
            int TodoID = Convert.ToInt32(ToDoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected student in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the Student class and store the query string inside of it
                Todo deletedGame = (from GameRecord in db.Todos
                                    where GameRecord.TodoID == TodoID
                                    select GameRecord).FirstOrDefault();

                // remove the selected student from the db
                db.Todos.Remove(deletedGame);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetToDo();
            }
        }

        protected void ToDoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            ToDoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetToDo();
        }

        protected void ToDoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sorty by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetToDo();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void ToDoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new Page size
            ToDoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetToDo();
        }
    }
}