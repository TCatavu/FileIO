using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TKC.FileIO.UI
{
    public partial class frmFileIO : Form
    {
        // Global Variables
        string filename;
        String path = String.Empty;

        public frmFileIO()
        {
            InitializeComponent();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            try
            {
                // Change status label to blue
                lblStatus.ForeColor = Color.Blue;

                // Empty status tet
                lblStatus.Text = string.Empty;

                // Declare
                OpenFileDialog openFileDialog;
                openFileDialog = new OpenFileDialog();

                // Set text and location
                openFileDialog.Title = "Pick a file";
                openFileDialog.InitialDirectory = @"c:\Users\public";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All Files (*.*)|*.*";

                // Show the dialog to user
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                    StreamReader streamReader = File.OpenText(filename);
                    txtMain.Text = streamReader.ReadToEnd();
                    lblStatus.Text = openFileDialog.FileName;
                    filename = openFileDialog.FileName;
                    streamReader.Close();
                    streamReader = null;
                }
                else
                {
                    // Show error to user
                    throw new Exception("No file selected.");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;      // Exception message
                lblStatus.ForeColor = Color.Red; // Change label color to red
            }
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                // Change status label to blue
                lblStatus.ForeColor = Color.Blue;

                // Empty status tet
                lblStatus.Text = string.Empty;

                // Declare
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                StreamWriter streamWriter;

                // Set text and location
                saveFileDialog.Title = "Please pick a file";
                saveFileDialog.InitialDirectory = @"c:\Users\public";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // write all the text
                    File.WriteAllText(path = saveFileDialog.FileName, txtMain.Text);

                    // Show dialog to user
                    filename = saveFileDialog.FileName;
                    streamWriter = File.CreateText(filename);
                    streamWriter.WriteLine(txtMain.Text);
                    streamWriter.Close();
                    streamWriter = null;
                    lblStatus.Text = $"{filename} written...";
                }
                else
                {
                    // Show error to user
                    throw new Exception("No file selected...");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;      // Exception message
                lblStatus.ForeColor = Color.Red; // Change label color to red
            }
        }

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtMain.Text))
            {
                exitPrompt(); // Show exit prompt

                if (DialogResult == DialogResult.Yes) // If user selects "Yes" then save
                {
                    // Save as
                    mnuFileSaveAs_Click(sender, e);
                    txtMain.Text = String.Empty;
                    path = String.Empty; ;
                }
                else if (DialogResult == DialogResult.No) // If user  selects "No" do not save
                {
                    // Dont save
                    txtMain.Text = String.Empty; ;
                    path = String.Empty; ;
                }

            }
        }

        private void exitPrompt() // Exit prompt
        {
            // Show dialog
            DialogResult = MessageBox.Show("Do you want to save current file?",
                "Notepad",
            // Show buttons and question icon
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            // If file is not empty
            if (!String.IsNullOrWhiteSpace(filename))
            {
                // Save file
                File.WriteAllText(filename, txtMain.Text);
                lblStatus.Text = $"{filename} Saved...";
            }
            
            // If file is empty
            else
            {
                // Save as
                mnuFileSaveAs_Click(sender, e);
            }
        }

        private void frmFileIO_FormClosing(object sender, FormClosingEventArgs e) // When form is closing
        {
            // If file is not empty
            if (!string.IsNullOrWhiteSpace(txtMain.Text))
            {
                // Show exit promp
                exitPrompt();

                if (DialogResult == DialogResult.Yes) // If "Yes"
                {
                    // Save as
                    mnuFileSaveAs_Click(sender, e);
                }
                else if (DialogResult == DialogResult.Cancel) // If "No"
                {
                    // Close application
                    e.Cancel = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // Timer function
        {
            // Change status label to show time 
            lblTimerStatus.Text = DateTime.Now.ToString();
        }
    }
}