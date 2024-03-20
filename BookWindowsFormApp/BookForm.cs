using Business;
using Data.Model;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookWindowsFormApp
{
    public partial class BookForm : Form
    {
        private readonly BookBusiness bookBusiness;

        public BookForm()
        {
            InitializeComponent();
            bookBusiness = new BookBusiness();
            // Зареждане на данните в DataGridView
            RefreshDataGridView();
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.Columns["Id"].Width = 45; // Задаване на ширина от 50 пиксела за колоната "Id"
            dataGridViewBooks.Columns["Title"].Width = 280; // Задаване на ширина от 200 пиксела за колоната "Title"
            dataGridViewBooks.Columns["Author"].Width = 150; // Задаване на ширина от 150 пиксела за колоната "Author"
            dataGridViewBooks.Columns["Genre"].Width = 100; // Задаване на ширина от 100 пиксела за колоната "Genre"
            dataGridViewBooks.Columns["Publisher"].Width = 200; // Задаване на ширина от 200 пиксела за колоната "Publisher"
            dataGridViewBooks.Columns["Year"].Width = 60; // Задаване на ширина от 75 пиксела за колоната "Year"
            numericUpDownYear.Value = DateTime.Now.Year; // Задаване на стойността по подразбиране
            numericUpDownYear.Maximum = DateTime.Now.Year; // Задаване на максималната стойност
            textBoxId.ReadOnly = true;
        }

        private void RefreshDataGridView()
        {
            dataGridViewBooks.DataSource = bookBusiness.GetAll();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Book newBook = new Book()
            {
                Title = textBoxTitle.Text,
                Author = textBoxAuthor.Text,
                Genre = textBoxGenre.Text,
                Publisher = textBoxPublisher.Text,
                Year = (int)numericUpDownYear.Value
            };

            bookBusiness.Add(newBook);
            RefreshDataGridView();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // Проверка дали е избран ред в DataGridView
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Моля, изберете запис за редакция.");
                return;
            }

            // Получаваме ID на избрания запис
            int selectedBookId = (int)dataGridViewBooks.SelectedRows[0].Cells["Id"].Value;

            // Подготвяме обект с новите данни
            Book updatedBook = new Book
            {
                Id = selectedBookId,
                Title = textBoxTitle.Text,
                Author = textBoxAuthor.Text,
                Genre = textBoxGenre.Text,
                Publisher = textBoxPublisher.Text,
                Year = (int)numericUpDownYear.Value
            };

            // Обновяваме записа в базата данни
            bookBusiness.Update(updatedBook);

            // Обновяваме DataGridView с актуалните данни
            RefreshDataGridView();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Проверка дали е избран ред в DataGridView
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Моля, изберете запис за изтриване.");
                return;
            }

            // Получаваме ID на избрания запис
            int selectedBookId = (int)dataGridViewBooks.SelectedRows[0].Cells["Id"].Value;

            // Изтриваме записа от базата данни
            bookBusiness.Delete(selectedBookId);

            // Обновяваме DataGridView с актуалните данни
            RefreshDataGridView();
        }

        private void dataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewBooks.SelectedRows[0];
                textBoxId.Text = row.Cells["Id"].Value.ToString();
                textBoxTitle.Text = row.Cells["Title"].Value.ToString();
                textBoxAuthor.Text = row.Cells["Author"].Value.ToString();
                textBoxGenre.Text = row.Cells["Genre"].Value.ToString();
                textBoxPublisher.Text = row.Cells["Publisher"].Value.ToString();
                numericUpDownYear.Value = (int)row.Cells["Year"].Value;
            }
        }

    }
}
