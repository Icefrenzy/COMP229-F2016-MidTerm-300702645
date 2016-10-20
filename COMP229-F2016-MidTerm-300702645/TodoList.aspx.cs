using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Using Statements for Required Connection to DB Models
using COMP229_F2016_MidTerm_300702645.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP229_F2016_MidTerm_300702645
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // If the page is loaded for the first time then fill out the grid view
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoId"; // default sort column
                Session["SortDirection"] = "ASC";

                // Get the To Do List
                this.GetTodoList();
            }
        }


        /// Get all the Tasks from the Todo List Table

        private void GetTodoList()
        {
            // connect to EF DB
            using (TodoContext db = new TodoContext())
            {
                string SortString = Session["SortColumn"].ToString() + " " +
                    Session["SortDirection"].ToString();

                List<ToDoTable> tasklist = new List<ToDoTable>();

                // Store the todo list tables in a variable
                var tasks = (from alltasks in db.ToDoTables
                                select alltasks);


                // Bind result to the grid view
                tasklist.AddRange(tasks);
                TodoGridView.DataSource = tasks.AsQueryable().OrderBy(SortString).ToList();
                TodoGridView.DataBind();

            }
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            // Archive the row selected from the page
            int selectedRow = e.RowIndex;

            // Use the Grid View to archive the task ID
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoId"]);

            using (TodoContext db = new TodoContext())
            {
                // Query the tasks matching the selected one by id
                ToDoTable deletedTask = (from alltasks in db.ToDoTables
                                          where alltasks.TodoId == TodoID
                                          select alltasks).FirstOrDefault();

                // Remove the task from the database
                db.ToDoTables.Remove(deletedTask);

                // Commit Changes to the db
                db.SaveChanges();

                // Clean up the grid view to show current records
                this.GetTodoList();
            }


        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the new Page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the GridView
            this.GetTodoList();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // refresh the GridView
            this.GetTodoList();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < TodoGridView.Columns.Count - 1; index++)
                    {
                        if (TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the Gridview
            this.GetTodoList();
        }

    }
}