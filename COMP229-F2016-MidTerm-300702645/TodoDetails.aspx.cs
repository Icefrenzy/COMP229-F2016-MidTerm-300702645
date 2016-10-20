using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Statements for DB Connection
using COMP229_F2016_MidTerm_300702645.Models;
using System.Web.ModelBinding;

namespace COMP229_F2016_MidTerm_300702645
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodoList();
            }
        }

        protected void GetTodoList()
        {
            // populate the form with existing data from db
            int TodoID = Convert.ToInt32(Request.QueryString["TodoId"]);

            // connect to the EF DB
            using (TodoContext db = new TodoContext())
            {
                // Query student matching the selected one
                ToDoTable updatedTask = (from tasks in db.ToDoTables
                                          where tasks.TodoId == TodoID
                                          select tasks).FirstOrDefault();

                // Assign the student data to the form
                if (updatedTask != null)
                {
                    TodoDescriptionTextBox.Text = updatedTask.TodoDescription;
                    TodoNotesTextBox.Text = updatedTask.TodoNotes;
                    if (updatedTask.Completed == true)
                        TodoCheckBox.Checked = true;
                    else
                        TodoCheckBox.Checked = false;
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to the Task List page
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to conect to the server
            using (TodoContext db = new TodoContext())
            {
                // Use the variable model for a task to save to dv

                ToDoTable newTask = new ToDoTable();

                int TodoID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoId"]);

                    // get the current task from EF db
                    newTask = (from tasks in db.ToDoTables
                                  where tasks.TodoId == TodoID
                                  select tasks).FirstOrDefault();
                }

                // Add the textbox data to the variable
                newTask.TodoDescription = TodoDescriptionTextBox.Text;
                newTask.TodoNotes = TodoNotesTextBox.Text;
                if(TodoCheckBox.Checked == true)
                {
                    newTask.Completed = true;
                }else
                {
                    newTask.Completed = false;
                }

                // use LINQ to ADO.NET to add/update the task in the database.

                if (TodoID == 0)
                {
                    db.ToDoTables.Add(newTask);
                }

                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated students page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}