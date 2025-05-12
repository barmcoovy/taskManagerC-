using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace taskManager
{
    
    public partial class MainPage : ContentPage
    {
        int stepperValue = 0;
        int points = 0;
        public ObservableCollection<Zadanie> tasksCollection = new ObservableCollection<Zadanie>();
       
        public MainPage()
        {
            InitializeComponent();
            Zadania.ItemsSource = tasksCollection;
        }

        private void Add_Task(object sender, EventArgs e)
        {
            string taskName = taskNameEntry.Text;
            if (!string.IsNullOrEmpty(taskName))
            {
                Zadanie task = new Zadanie(taskName, taskDateTimePicker.Date, stepperValue);
                tasksCollection.Add(task);
                taskNameEntry.Text = "";
                pointsStepper.Value = 1;
                stepperValueLbl.Text = $"Aktualnie punktów: {stepperValue}";
                taskDateTimePicker.Date = DateTime.Now;
            }
            
        }

        private void Zadania_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }
        private void End_Task(object sender, EventArgs e)
        {

            var selectedItem = Zadania.SelectedItem as Zadanie;
            points += selectedItem.punkty;
            if (selectedItem != null && tasksCollection.Contains(selectedItem))
            {
                tasksCollection.Remove(selectedItem);
            }
            pointsLbl.Text = $"Zdobyte punkty: {points}";
        }
        private void Delete_Task(object sender, EventArgs e)
        {
            var selectedItem = Zadania.SelectedItem as Zadanie;
            if (selectedItem != null && tasksCollection.Contains(selectedItem))
            {
                tasksCollection.Remove(selectedItem);
            }
            pointsLbl.Text = $"Zdobyte punkty: {points}";
        }
        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            stepperValue = Convert.ToInt32(e.NewValue);
            stepperValueLbl.Text = $"Aktualnie punktów: {stepperValue}";
        }

        
    }


    public class Zadanie
    {
        public string tytul { get; set; }
        public DateTime termin { get; set; }
        public int punkty { get; set; }

        public Zadanie() { }

        public Zadanie(string _tytul, DateTime _termin, int _punkty = 0)
        {
            tytul = _tytul ;
            termin = _termin;
            punkty = _punkty;
        }

        public int GetPunkty()
        {
            return this.punkty;
        }

        public override string ToString()
        {
            return GetPunkty().ToString();
        }

        public bool CzyZadanieJestPoTerminie()
        {
            return DateTime.Now > termin;
        }

        public int DniDoTerminu()
        {
            return (termin - DateTime.Now).Days;
        }

        public void SetTermin(DateTime nowyTermin)
        {
            termin = nowyTermin;
        }

    }


}
