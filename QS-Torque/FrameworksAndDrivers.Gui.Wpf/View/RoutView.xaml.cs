using FrameworksAndDrivers.Localization;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for RoutView.xaml
    /// </summary>
    public partial class RoutView : UserControl
    {
        public class ObjectTuple
        {
            public object Item1 { get; set; }
            public object Item2 { get; set; }
            public object Item3 { get; set; }
            public object Item4 { get; set; }
            public object Item5 { get; set; }
            public object Item6 { get; set; }
            public object Item7 { get; set; }
            public object Item8 { get; set; }
        }

        public ObservableCollection<ObjectTuple> List { get; set; } = new ObservableCollection<ObjectTuple>();

        public RoutView(LocalizationWrapper localization)
        {
            InitializeComponent();
            this.DataContext = this;
            DataGrid.LocalizationWrapper = localization;
            localization.Subscribe(DataGrid);

            List.Add(new ObjectTuple()
            {
                Item1 = 1,
                Item2 = "Stoßdämpfer Halterung oben",
                Item3 = 10223,
                Item4 = 489643945207317128,
                Item5 = "C",
                Item6 = 6,
                Item7 = "5 Nm",
                Item8 = "7 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 2,
                Item2 = "Motorblock Nr. 364",
                Item3 = 78979,
                Item4 = 653012432622727649,
                Item5 = "A",
                Item6 = 21,
                Item7 = "20 Nm",
                Item8 = "22 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 3,
                Item2 = "Anlasser oben links",
                Item3 = 98748,
                Item4 = 388918379132280530,
                Item5 = "A",
                Item6 = 42,
                Item7 = "41 Nm",
                Item8 = "43 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 4,
                Item2 = "Tank hinten rechts",
                Item3 = 19139,
                Item4 = 263910860595772513,
                Item5 = "C",
                Item6 = 6,
                Item7 = "5 Nm",
                Item8 = "7 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 5,
                Item2 = "Scheinwerfer 46",
                Item3 = 79675,
                Item4 = 592981401317446249,
                Item5 = "B",
                Item6 = 12,
                Item7 = "11 Nm",
                Item8 = "13 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 6,
                Item2 = "Vorderachsträget Schraube 9",
                Item3 = 78300,
                Item4 = 550581120395220033,
                Item5 = "B",
                Item6 = 8,
                Item7 = "7 Nm",
                Item8 = "9 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 7,
                Item2 = "Ölwanne 13",
                Item3 = 51535,
                Item4 = 541757246933310212,
                Item5 = "B",
                Item6 = 3,
                Item7 = "2 Nm",
                Item8 = "4 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 8,
                Item2 = "Batteriehalterung 3",
                Item3 = 48670,
                Item4 = 334791486987879159,
                Item5 = "A",
                Item6 = 35,
                Item7 = "34 Nm",
                Item8 = "36 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 9,
                Item2 = "Zylinderkopf 65",
                Item3 = 11595,
                Item4 = 514611384463798504,
                Item5 = "B",
                Item6 = 4,
                Item7 = "3 Nm",
                Item8 = "5 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 10,
                Item2 = "Seitenairbag Fahrerseite hinten B-Säule 2",
                Item3 = 33561,
                Item4 = 240042580673298032,
                Item5 = "C",
                Item6 = 9,
                Item7 = "8 Nm",
                Item8 = "10 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 11,
                Item2 = "Kofferraumdeckel Scharaube 36",
                Item3 = 40017,
                Item4 = 997725266469795581,
                Item5 = "C",
                Item6 = 32,
                Item7 = "31 Nm",
                Item8 = "33 Nm"
            });
            List.Add(new ObjectTuple()
            {
                Item1 = 12,
                Item2 = "Scheibenwischer links 4",
                Item3 = 46020,
                Item4 = 297775869710699472,
                Item5 = "B",
                Item6 = 16,
                Item7 = "15 Nm",
                Item8 = "17 Nm"
            });
        }
    }
}
